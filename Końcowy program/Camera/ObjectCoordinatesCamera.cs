using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompleteProgram
{
    static class ObjectCoordinatesCamera
    {
        private static double pi = Math.PI;
        public static Tuple<double[][], int[]> LUPDecomposition(double[][] A)
        {
            int n = A.Length - 1;
            /*
            * pi represents the permutation matrix.  We implement it as an array
            * whose value indicates which column the 1 would appear.  We use it to avoid 
            * dividing by zero or small numbers.
            * */
            int[] pi = new int[n + 1];
            double p = 0;
            int kp = 0;
            int pik = 0;
            int pikp = 0;
            double aki = 0;
            double akpi = 0;

            //Initialize the permutation matrix, will be the identity matrix
            for (int j = 0; j <= n; j++)
            {
                pi[j] = j;
            }

            for (int k = 0; k <= n; k++)
            {
                /*
                * In finding the permutation matrix p that avoids dividing by zero
                * we take a slightly different approach.  For numerical stability
                * We find the element with the largest 
                * absolute value of those in the current first column (column k).  If all elements in
                * the current first column are zero then the matrix is singluar and throw an
                * error.
                * */
                p = 0;
                for (int i = k; i <= n; i++)
                {
                    if (Math.Abs(A[i][k]) > p)
                    {
                        p = Math.Abs(A[i][k]);
                        kp = i;
                    }
                }
                if (p == 0)
                {
                    throw new Exception("singular matrix");
                }
                /*
                * These lines update the pivot array (which represents the pivot matrix)
                * by exchanging pi[k] and pi[kp].
                * */
                pik = pi[k];
                pikp = pi[kp];
                pi[k] = pikp;
                pi[kp] = pik;

                /*
                * Exchange rows k and kpi as determined by the pivot
                * */
                for (int i = 0; i <= n; i++)
                {
                    aki = A[k][i];
                    akpi = A[kp][i];
                    A[k][i] = akpi;
                    A[kp][i] = aki;
                }

                /*
                    * Compute the Schur complement
                    * */
                for (int i = k + 1; i <= n; i++)
                {
                    A[i][k] = A[i][k] / A[k][k];
                    for (int j = k + 1; j <= n; j++)
                    {
                        A[i][j] = A[i][j] - (A[i][k] * A[k][j]);
                    }
                }
            }
            return Tuple.Create(A, pi);
        }

        public static double[] LUPSolve(double[][] LU, int[] pi, double[] b)
        {
            int n = LU.Length - 1;
            double[] x = new double[n + 1];
            double[] y = new double[n + 1];
            double suml = 0;
            double sumu = 0;
            double lij = 0;

            /*
            * Solve for y using formward substitution
            * */
            for (int i = 0; i <= n; i++)
            {
                suml = 0;
                for (int j = 0; j <= i - 1; j++)
                {
                    /*
                    * Since we've taken L and U as a singular matrix as an input
                    * the value for L at index i and j will be 1 when i equals j, not LU[i][j], since
                    * the diagonal values are all 1 for L.
                    * */
                    if (i == j)
                    {
                        lij = 1;
                    }
                    else
                    {
                        lij = LU[i][j];
                    }
                    suml = suml + (lij * y[j]);
                }
                y[i] = b[pi[i]] - suml;
            }
            //Solve for x by using back substitution
            for (int i = n; i >= 0; i--)
            {
                sumu = 0;
                for (int j = i + 1; j <= n; j++)
                {
                    sumu = sumu + (LU[i][j] * x[j]);
                }
                x[i] = (y[i] - sumu) / LU[i][i];
            }
            return x;
        }

        public static double[][] InvertMatrix(double[][] A)
        {
            int n = A.Length;
            //e will represent each column in the identity matrix
            double[] e;
            //x will hold the inverse matrix to be returned
            double[][] x = new double[n][];
            for (int i = 0; i < n; i++)
            {
                x[i] = new double[A[i].Length];
            }
            /*
            * solve will contain the vector solution for the LUP decomposition as we solve
            * for each vector of x.  We will combine the solutions into the double[][] array x.
            * */
            double[] solve;

            //Get the LU matrix and P matrix (as an array)
            Tuple<double[][], int[]> results = LUPDecomposition(A);

            double[][] LU = results.Item1;
            int[] P = results.Item2;

            /*
            * Solve AX = e for each column ei of the identity matrix using LUP decomposition
            * */
            for (int i = 0; i < n; i++)
            {
                e = new double[A[i].Length];
                e[i] = 1;
                solve = LUPSolve(LU, P, e);
                for (int j = 0; j < solve.Length; j++)
                {
                    x[j][i] = solve[j];
                }
            }
            return x;
        }

        public static void macierze(double alfa, double beta, double gamma, double x, double y,
                                     double z, double zak, double zbk, double zck, double xakk, double yakk, double xbkk,
                                     double ybkk, double xckk, double yckk, double xa, double ya, double za, double xb,
                                     double yb, double zb, double xc, double yc, double zc, double fk,
                                     double[][] J, double[] F)
        {
            double w = pi / 180,
                    sa = Math.Sin(alfa * w),
                    ca = Math.Cos(alfa * w),
                    sb = Math.Sin(beta * w),
                    cb = Math.Cos(beta * w),
                    sg = Math.Sin(gamma * w),
                    cg = Math.Cos(gamma * w),
                    xak = -xakk * (zak / fk - 1),
                    xbk = -xbkk * (zbk / fk - 1),
                    xck = -xckk * (zck / fk - 1),
                    yak = -yakk * (zak / fk - 1),
                    ybk = -ybkk * (zbk / fk - 1),
                    yck = -yckk * (zck / fk - 1);
            // elementy pierwszej kolumny- pochodne po alfa
            J[0][0] = yak * cg * sb * ca + zak * (sg * ca - cg * cb * sa);
            J[1][0] = yak * (-cg * sa + sg * sb * ca) - zak * (cg * ca + sg * sb * sa);
            J[2][0] = yak * cb * ca - zak * cb * sa;
            J[3][0] = ybk * cg * sb * ca + zbk * (sg * ca - cg * cb * sa);
            J[4][0] = ybk * (-cg * sa + sg * sb * ca) - zbk * (cg * ca + sg * sb * sa);
            J[5][0] = ybk * cb * ca - zbk * cb * sa;
            J[6][0] = yck * cg * sb * ca + zck * (sg * ca - cg * cb * sa);
            J[7][0] = yck * (-cg * sa + sg * sb * ca) - zck * (cg * ca + sg * sb * sa);
            J[8][0] = yck * cb * ca - zck * cb * sa;
            // elementy drugiej kolumny- pochodne po beta
            J[0][1] = -xak * cg * sb + yak * cg * cb * sa + zak * cg * cb * ca;
            J[1][1] = -xak * sg * sb + yak * sg * cb * sa + zak * sg * cb * ca;
            J[2][1] = -xak * cb - yak * sb * sa - zak * sb * ca;
            J[3][1] = -xbk * cg * sb + ybk * cg * cb * sa + zbk * cg * cb * ca;
            J[4][1] = -xbk * sg * sb + ybk * sg * cb * sa + zbk * sg * cb * ca;
            J[5][1] = -xbk * cb - ybk * sb * sa - zbk * sb * ca;
            J[6][1] = -xck * cg * sb + yck * cg * cb * sa + zck * cg * cb * ca;
            J[7][1] = -xck * sg * sb + yck * sg * cb * sa + zck * sg * cb * ca;
            J[8][1] = -xck * cb - yck * sb * sa - zck * sb * ca;
            // elementy trzeciej kolumny- pochodne po gamma
            J[0][2] = -xak * sg * cb - yak * (cg * ca + sg * sb * sa) + zak * (cg * sa - sg * sb * ca);
            J[1][2] = xak * cg * cb + yak * (-sg * ca + cg * sb * sa) + zak * (sg * sa + cg * sb * ca);
            J[2][2] = 0;
            J[3][2] = -xbk * sg * cb - ybk * (cg * ca + sg * sb * sa) + zbk * (cg * sa - sg * sb * ca);
            J[4][2] = xbk * cg * cb + ybk * (-sg * ca + cg * sb * sa) + zbk * (sg * sa + cg * sb * ca);
            J[5][2] = 0;
            J[6][2] = -xck * sg * cb - yck * (cg * ca + sg * sb * sa) + zck * (cg * sa - sg * sb * ca);
            J[7][2] = xck * cg * cb + yck * (-sg * ca + cg * sb * sa) + zck * (sg * sa + cg * sb * ca);
            J[8][2] = 0;
            // elementy czwartej kolumny- pochodne po x
            J[0][3] = 1;
            J[1][3] = 0;
            J[2][3] = 0;
            J[3][3] = 1;
            J[4][3] = 0;
            J[5][3] = 0;
            J[6][3] = 1;
            J[7][3] = 0;
            J[8][3] = 0;
            // elementy piątej kolumny- pochodne po y
            J[0][4] = 0;
            J[1][4] = 1;
            J[2][4] = 0;
            J[3][4] = 0;
            J[4][4] = 1;
            J[5][4] = 0;
            J[6][4] = 0;
            J[7][4] = 1;
            J[8][4] = 0;
            // elementy szóstej kolumny- pochodne po z
            J[0][5] = 0;
            J[1][5] = 0;
            J[2][5] = 1;
            J[3][5] = 0;
            J[4][5] = 0;
            J[5][5] = 1;
            J[6][5] = 0;
            J[7][5] = 0;
            J[8][5] = 1;
            // elementy siódmej kolumny- pochodne po zak
            J[0][6] = -(xakk * cg * cb) / fk - (yakk * (-sg * ca + cg * sb * sa)) / fk + (sg * sa + cg * sb * ca);
            J[1][6] = -(xakk * sg * cb) / fk - (yakk * (cg * ca * sg * sb * sa)) / fk + (-cg * sa + sg * sb * ca);
            J[2][6] = (xakk * sb) / fk - (yakk * cb * sa) / fk + cb * ca;
            J[3][6] = 0;
            J[4][6] = 0;
            J[5][6] = 0;
            J[6][6] = 0;
            J[7][6] = 0;
            J[8][6] = 0;
            // elementy ósmej kolumny- pochodne po zbk
            J[0][7] = 0;
            J[1][7] = 0;
            J[2][7] = 0;
            J[3][7] = -(xbkk * cg * cb) / fk - (ybkk * (-sg * ca + cg * sb * sa)) / fk + (sg * sa + cg * sb * ca);
            J[4][7] = -(xbkk * sg * cb) / fk - (ybkk * (cg * ca * sg * sb * sa)) / fk + (-cg * sa + sg * sb * ca);
            J[5][7] = (xbkk * sb) / fk - (ybkk * cb * sa) / fk + cb * ca;
            J[6][7] = 0;
            J[7][7] = 0;
            J[8][7] = 0;
            // elementy dziewiątej kolumny- pochodne po zck
            J[0][8] = 0;
            J[1][8] = 0;
            J[2][8] = 0;
            J[3][8] = 0;
            J[4][8] = 0;
            J[5][8] = 0;
            J[6][8] = -(xckk * cg * cb) / fk - (yckk * (-sg * ca + cg * sb * sa)) / fk + (sg * sa + cg * sb * ca);
            J[7][8] = -(xckk * sg * cb) / fk - (yckk * (cg * ca * sg * sb * sa)) / fk + (-cg * sa + sg * sb * ca);
            J[8][8] = (xckk * sb) / fk - (yckk * cb * sa) / fk + cb * ca;
            // populacja F
            F[0] = cg * cb * xak + (-sg * ca + cg * sb * sa) * yak + (sg * sa + cg * sb * ca) * zak + x - xa;
            F[1] = sg * cb * xak + (cg * ca + sg * sb * sa) * yak + (-cg * sa + sg * sb * ca) * zak + y - ya;
            F[2] = -sb * xak + cb * sa * yak + cb * ca * zak + z - za;
            F[3] = cg * cb * xbk + (-sg * ca + cg * sb * sa) * ybk + (sg * sa + cg * sb * ca) * zbk + x - xb;
            F[4] = sg * cb * xbk + (cg * ca + sg * sb * sa) * ybk + (-cg * sa + sg * sb * ca) * zbk + y - yb;
            F[5] = -sb * xbk + cb * sa * ybk + cb * ca * zbk + z - zb;
            F[6] = cg * cb * xck + (-sg * ca + cg * sb * sa) * yck + (sg * sa + cg * sb * ca) * zck + x - xc;
            F[7] = sg * cb * xck + (cg * ca + sg * sb * sa) * yck + (-cg * sa + sg * sb * ca) * zck + y - yc;
            F[8] = -sb * xck + cb * sa * yck + cb * ca * zck + z - zc;
        }

        public static void uklk(double alfa, double beta, double gamma, double x, double y, double z, double[,] T)
        {
            double w = pi / 180,
                    sa = Math.Sin(alfa * w),
                    ca = Math.Cos(alfa * w),
                    sb = Math.Sin(beta * w),
                    cb = Math.Cos(beta * w),
                    sg = Math.Sin(gamma * w),
                    cg = Math.Cos(gamma * w);
            T[0, 0] = cg * cb;
            T[0, 1] = -sg * ca + cg * sb * sa;
            T[0, 2] = sg * sa + cg * sb * ca;
            T[0, 3] = x;
            T[1, 0] = sg * cb;
            T[1, 1] = cg * ca + sg * sb * sa;
            T[1, 2] = -cg * sa + sg * sb * ca;
            T[1, 3] = y;
            T[2, 0] = -sb;
            T[2, 1] = cb * sa;
            T[2, 2] = cb * ca;
            T[2, 3] = z;
            T[3, 0] = 0;
            T[3, 1] = 0;
            T[3, 2] = 0;
            T[3, 3] = 1;
        }

        public static void kamera(double alfa, double beta, double gamma, double x, double y,
                                   double z, double zak, double zbk, double zck, double xakk, double yakk, double xbkk,
                                   double ybkk, double xckk, double yckk, double xa, double ya, double za, double xb,
                                   double yb, double zb, double xc, double yc, double zc, double fk, double delta,
                                   double[,] T, double[] xk)
        {
            double w = pi / 180;
            double[][] J = new double[9][];
            for (int k = 0; k < 9; k++)
            {
                J[k] = new double[9];
            }
            double[][] InvertedJ;
            double[] F = new double[9];
            double[] dx = new double[9];
            double temp;
            macierze(alfa, beta, gamma, x, y, z, zak, zbk, zck, xakk, yakk, xbkk, ybkk, xckk, yckk, xa, ya, za, xb, yb, zb, xc, yc, zc, fk, J, F);
            int i = 1;
            double dok = 1;
            while (dok > 0)
            {
                InvertedJ = InvertMatrix(J);
                for (int k = 0; k < 9; k++)
                {
                    temp = 0;
                    for (int j = 0; j < 9; j++)
                    {
                        temp = temp + (InvertedJ[k][j] * F[j]);
                    }
                    dx[k] = -1 * temp;
                }
                alfa = alfa + dx[0] / w;
                beta = beta + dx[1] / w;
                gamma = gamma + dx[2] / w;
                x = x + dx[3];
                y = y + dx[4];
                z = z + dx[5];
                zak = zak + dx[6];
                zbk = zbk + dx[7];
                zck = zck + dx[8];
                macierze(alfa, beta, gamma, x, y, z, zak, zbk, zck, xakk, yakk, xbkk, ybkk, xckk, yckk, xa, ya, za, xb, yb, zb, xc, yc, zc, fk, J, F);
                xk[0] = alfa;
                xk[1] = beta;
                xk[2] = gamma;
                xk[3] = x;
                xk[4] = y;
                xk[5] = z;
                xk[6] = zak;
                xk[7] = zbk;
                xk[8] = zck;
                double[] d = new double[9];
                for (int j = 0; j < 9; j++)
                {
                    d[j] = Math.Abs(F[j]);
                }
                double d1 = d.Max();
                double dokl = d1 - delta;
                if (dokl < 0)
                {
                    double[] u = new double[3];
                    u[0] = i;
                    u[1] = d1;
                    u[2] = dokl;
                }
                i = i + 1;
                dok = dokl;
                // w głównym programie wrzucić ładnego message boxa z opcjami ... a na razie uproszczone if przerywające pętlę
                if (i > 1000)
                    break;
                //if i>1000
                //    disp('i>1000');
                //    disp('xk=');
                //    disp(xk);
                //    kx=input('jeśli kontunuować obliczenia podaj liczbę >0 , w przeciwnym przypadku <=0 kx=');
                //    if kx>0
                //       i=1;
                //       alfa=input('podaj nową wartość alfa w [stopniach]');
                //       beta=input('podaj nową wartość beta w [stopniach]');
                //       gamma=input('podaj nową wartość gamma w [stopniach]');
                //       x=input('podaj nową wartość x w [mm]');
                //       y=input('podaj nową wartość y w [mm]');
                //       z=input('podaj nową wartość z w [mm]');
                //       zak=input('podaj nową wartość zak w [mm]');
                //       zbk=input('podaj nową wartość zbk w [mm]');
                //       zck=input('podaj nową wartość zck w [mm]');
                //       [J,F]=macierze(alfa,beta,gamma,x,y,z,zak,zbk,zck,xakk,yakk,xbkk,ybkk,xckk,yckk,xa,ya,za,xb,yb,zb,xc,yc,zc,fk);
                //    end
                //    if kx<=0
                //        disp('przerwanie obliczeń')
                //        stop
                //    end
                // end
            }
            uklk(xk[0], xk[1], xk[2], xk[3], xk[4], xk[5], T);
        }

        public static void kalibracja(double x, double y, double z, double dx, double dy, double[,] T, double[] xk, double fk, double pxx, double pxy,
                                           double nx0, double ny0, double[, ,] C2B, double[,] xkko, double[,] ykko, double[,] xkki, double[,] ykki, double[,] xod, double[,] yod,
                                           double[,] xkkos, double[,] ykkos, double[,] xods, double[,] yods, double[][] error, double [,] K)
        {
            //Program obliczania parametrów geometrycznych kamery1B:  alfa,beta,gamma,x,y,z,zak,zbk,zck.
            //alfa, beta, gamma-kąty w stopniach ustalone x-y-z  układu kamery w układzie odniesienia ;
            //  x,y,z-współrzędne połozenia w mm początku układu kamery w układzie odniesienia ;
            //  zak, zbk, zck- współrzędne (z)w mm  punktów A,B,C w układzie kamery;
            //
            //  Wyniki pojawiają się w macierzy
            // Tk=Trans(x,y,z)Rot(z,gamma)Rot(y,beta)Rot(x,alfa), 
            // oraz w macierzy xk=[alfa,beta,gamma,x,y,z,zak,zbk,zck] zawierajcej obliczone współrzędne kamery.
            //

            double[,] Todwr = new double[4, 4];
            double d = Math.Sqrt(Math.Pow(z, 2) + Math.Pow(x, 2)),
                    fi = Math.Atan(x / z) * 180 / pi,
                    alfa = 180 + fi,
                    beta = 0,
                    gamma = 90;
            double delta = 1e-6;
            delta = delta * 1;

            double[,] P = new double[24, 4];
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            // Tworzenie zbioru punktów P1-P24 według numeracji na kartce ICMMI-2015 na str.1. -opisanych w układzie współrzędnych odniesienia xyz

            // punkt P1
            double x1 = -dx,
                    y1 = -dy,
                    z1 = 0,
                    z1k = d - x1 * Math.Sin(fi * pi / 180);
            P[0, 0] = x1;
            P[0, 1] = y1;
            P[0, 2] = z1;
            P[0, 3] = z1k;

            // punkt P2
            double x2 = -dx,
                    y2 = 0,
                    z2 = 0,
                    z2k = d - x2 * Math.Sin(fi * pi / 180);
            P[1, 0] = x2;
            P[1, 1] = y2;
            P[1, 2] = z2;
            P[1, 3] = z2k;

            // punkt P3
            double x3 = -dx,
                    y3 = dy,
                    z3 = 0,
                    z3k = d - x3 * Math.Sin(fi * pi / 180);
            P[2, 0] = x3;
            P[2, 1] = y3;
            P[2, 2] = z3;
            P[2, 3] = z3k;

            // punkt P4
            double x4 = 0,
                    y4 = dy,
                    z4 = 0,
                    z4k = d - x4 * Math.Sin(fi * pi / 180);
            P[3, 0] = x4;
            P[3, 1] = y4;
            P[3, 2] = z4;
            P[3, 3] = z4k;

            // punkt P5
            double x5 = dx,
                    y5 = dy,
                    z5 = 0,
                    z5k = d - x5 * Math.Sin(fi * pi / 180);
            P[4, 0] = x5;
            P[4, 1] = y5;
            P[4, 2] = z5;
            P[4, 3] = z5k;

            // punkt P6
            double x6 = dx,
                    y6 = 0,
                    z6 = 0,
                    z6k = d - x6 * Math.Sin(fi * pi / 180);
            P[5, 0] = x6;
            P[5, 1] = y6;
            P[5, 2] = z6;
            P[5, 3] = z6k;

            // punkt P7
            double x7 = dx,
                    y7 = -dy,
                    z7 = 0,
                    z7k = d - x7 * Math.Sin(fi * pi / 180);
            P[6, 0] = x7;
            P[6, 1] = y7;
            P[6, 2] = z7;
            P[6, 3] = z7k;

            // punkt P8
            double x8 = 0,
                    y8 = -dy,
                    z8 = 0,
                    z8k = d - x8 * Math.Sin(fi * pi / 180);
            P[7, 0] = x8;
            P[7, 1] = y8;
            P[7, 2] = z8;
            P[7, 3] = z8k;

            // punkt P9
            double x9 = -2 * dx,
                    y9 = -2 * dy,
                    z9 = 0,
                    z9k = d - x9 * Math.Sin(fi * pi / 180);
            P[8, 0] = x9;
            P[8, 1] = y9;
            P[8, 2] = z9;
            P[8, 3] = z9k;

            // punkt P10
            double x10 = -2 * dx,
                    y10 = -dy,
                    z10 = 0,
                    z10k = d - x10 * Math.Sin(fi * pi / 180);
            P[9, 0] = x10;
            P[9, 1] = y10;
            P[9, 2] = z10;
            P[9, 3] = z10k;

            // punkt P11
            double x11 = -2 * dx,
                    y11 = 0,
                    z11 = 0,
                    z11k = d - x11 * Math.Sin(fi * pi / 180);
            P[10, 0] = x11;
            P[10, 1] = y11;
            P[10, 2] = z11;
            P[10, 3] = z11k;

            // punkt P12
            double x12 = -2 * dx,
                    y12 = dy,
                    z12 = 0,
                    z12k = d - x12 * Math.Sin(fi * pi / 180);
            P[11, 0] = x12;
            P[11, 1] = y12;
            P[11, 2] = z12;
            P[11, 3] = z12k;

            // punkt P13
            double x13 = -2 * dx,
                    y13 = 2 * dy,
                    z13 = 0,
                    z13k = d - x13 * Math.Sin(fi * pi / 180);
            P[12, 0] = x13;
            P[12, 1] = y13;
            P[12, 2] = z13;
            P[12, 3] = z13k;

            // punkt P14
            double x14 = -dx,
                    y14 = 2 * dy,
                    z14 = 0,
                    z14k = d - x14 * Math.Sin(fi * pi / 180);
            P[13, 0] = x14;
            P[13, 1] = y14;
            P[13, 2] = z14;
            P[13, 3] = z14k;

            // punkt P15
            double x15 = 0,
                    y15 = 2 * dy,
                    z15 = 0,
                    z15k = d - x15 * Math.Sin(fi * pi / 180);
            P[14, 0] = x15;
            P[14, 1] = y15;
            P[14, 2] = z15;
            P[14, 3] = z15k;

            // punkt P16
            double x16 = dx,
                    y16 = 2 * dy,
                    z16 = 0,
                    z16k = d - x16 * Math.Sin(fi * pi / 180);
            P[15, 0] = x16;
            P[15, 1] = y16;
            P[15, 2] = z16;
            P[15, 3] = z16k;

            // punkt P17
            double x17 = 2 * dx,
                    y17 = 2 * dy,
                    z17 = 0,
                    z17k = d - x17 * Math.Sin(fi * pi / 180);
            P[16, 0] = x17;
            P[16, 1] = y17;
            P[16, 2] = z17;
            P[16, 3] = z17k;

            // punkt P18
            double x18 = 2 * dx,
                    y18 = dy,
                    z18 = 0,
                    z18k = d - x18 * Math.Sin(fi * pi / 180);
            P[17, 0] = x18;
            P[17, 1] = y18;
            P[17, 2] = z18;
            P[17, 3] = z18k;

            // punkt P19
            double x19 = 2 * dx,
                    y19 = 0,
                    z19 = 0,
                    z19k = d - x19 * Math.Sin(fi * pi / 180);
            P[18, 0] = x19;
            P[18, 1] = y19;
            P[18, 2] = z19;
            P[18, 3] = z19k;

            // punkt P20
            double x20 = 2 * dx,
                    y20 = -dy,
                    z20 = 0,
                    z20k = d - x20 * Math.Sin(fi * pi / 180);
            P[19, 0] = x20;
            P[19, 1] = y20;
            P[19, 2] = z20;
            P[19, 3] = z20k;

            // punkt P21
            double x21 = 2 * dx,
                    y21 = -2 * dy,
                    z21 = 0,
                    z21k = d - x21 * Math.Sin(fi * pi / 180);
            P[20, 0] = x21;
            P[20, 1] = y21;
            P[20, 2] = z21;
            P[20, 3] = z21k;

            // punkt P22
            double x22 = dx,
                    y22 = -2 * dy,
                    z22 = 0,
                    z22k = d - x22 * Math.Sin(fi * pi / 180);
            P[21, 0] = x22;
            P[21, 1] = y22;
            P[21, 2] = z22;
            P[21, 3] = z22k;

            // punkt P23
            double x23 = 0,
                    y23 = -2 * dy,
                    z23 = 0,
                    z23k = d - x23 * Math.Sin(fi * pi / 180);
            P[22, 0] = x23;
            P[22, 1] = y23;
            P[22, 2] = z23;
            P[22, 3] = z23k;

            // punkt P24
            double x24 = -dx,
                    y24 = -2 * dy,
                    z24 = 0,
                    z24k = d - x24 * Math.Sin(fi * pi / 180);
            P[23, 0] = x24;
            P[23, 1] = y24;
            P[23, 2] = z24;
            P[23, 3] = z24k;

            // Utworzenie zbioru punktów P1-P24 według numeracji na kartce ICMMI-2015 na str.1. -opisanych w układzie współrzędnych odniesienia xyz
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            // Tworzenie zbioru punktów PKK1-PKK24 stanowiacych opis obrazów odpowiednich punktów P1-P24 na matrycy kamery w układzie współrzędnych kamery xkyk

            double[,] PKK = new double[24, 2];

            // punkt PKK1
            double x1kk = -(C2B[5, 7, 0] - nx0) * pxx,
                    y1kk = -(C2B[5, 7, 1] - ny0) * pxy;
            PKK[0, 0] = x1kk;
            PKK[0, 1] = y1kk;

            // punkt PKK2
            double x2kk = -(C2B[5, 8, 0] - nx0) * pxx,
                    y2kk = -(C2B[5, 8, 1] - ny0) * pxy;
            PKK[1, 0] = x2kk;
            PKK[1, 1] = y2kk;

            // punkt PKK3
            double x3kk = -(C2B[5, 9, 0] - nx0) * pxx,
                    y3kk = -(C2B[5, 9, 1] - ny0) * pxy;
            PKK[2, 0] = x3kk;
            PKK[2, 1] = y3kk;

            // punkt PKK4
            double x4kk = -(C2B[6, 9, 0] - nx0) * pxx,
                    y4kk = -(C2B[6, 9, 1] - ny0) * pxy;
            PKK[3, 0] = x4kk;
            PKK[3, 1] = y4kk;

            // punkt PKK5
            double x5kk = -(C2B[7, 9, 0] - nx0) * pxx,
                    y5kk = -(C2B[7, 9, 1] - ny0) * pxy;
            PKK[4, 0] = x5kk;
            PKK[4, 1] = y5kk;

            // punkt PKK6
            double x6kk = -(C2B[7, 8, 0] - nx0) * pxx,
                    y6kk = -(C2B[7, 8, 1] - ny0) * pxy;
            PKK[5, 0] = x6kk;
            PKK[5, 1] = y6kk;

            // punkt PKK7
            double x7kk = -(C2B[7, 7, 0] - nx0) * pxx,
                    y7kk = -(C2B[7, 7, 1] - ny0) * pxy;
            PKK[6, 0] = x7kk;
            PKK[6, 1] = y7kk;

            // punkt PKK8
            double x8kk = -(C2B[6, 7, 0] - nx0) * pxx,
                    y8kk = -(C2B[6, 7, 1] - ny0) * pxy;
            PKK[7, 0] = x8kk;
            PKK[7, 1] = y8kk;

            // punkt PKK9
            double x9kk = -(C2B[4, 6, 0] - nx0) * pxx,
                    y9kk = -(C2B[4, 6, 1] - ny0) * pxy;
            PKK[8, 0] = x9kk;
            PKK[8, 1] = y9kk;

            // punkt PKK10
            double x10kk = -(C2B[4, 7, 0] - nx0) * pxx,
                    y10kk = -(C2B[4, 7, 1] - ny0) * pxy;
            PKK[9, 0] = x10kk;
            PKK[9, 1] = y10kk;

            // punkt PKK11
            double x11kk = -(C2B[4, 8, 0] - nx0) * pxx,
                    y11kk = -(C2B[4, 8, 1] - ny0) * pxy;
            PKK[10, 0] = x11kk;
            PKK[10, 1] = y11kk;

            // punkt PKK12
            double x12kk = -(C2B[4, 9, 0] - nx0) * pxx,
                    y12kk = -(C2B[4, 9, 1] - ny0) * pxy;
            PKK[11, 0] = x12kk;
            PKK[11, 1] = y12kk;

            // punkt PKK13
            double x13kk = -(C2B[4, 10, 0] - nx0) * pxx,
                    y13kk = -(C2B[4, 10, 1] - ny0) * pxy;
            PKK[12, 0] = x13kk;
            PKK[12, 1] = y13kk;

            // punkt PKK14
            double x14kk = -(C2B[5, 10, 0] - nx0) * pxx,
                    y14kk = -(C2B[5, 10, 1] - ny0) * pxy;
            PKK[13, 0] = x14kk;
            PKK[13, 1] = y14kk;

            // punkt PKK15
            double x15kk = -(C2B[6, 10, 0] - nx0) * pxx,
                    y15kk = -(C2B[6, 10, 1] - ny0) * pxy;
            PKK[14, 0] = x15kk;
            PKK[14, 1] = y15kk;

            // punkt PKK16
            double x16kk = -(C2B[7, 10, 0] - nx0) * pxx,
                    y16kk = -(C2B[7, 10, 1] - ny0) * pxy;
            PKK[15, 0] = x16kk;
            PKK[15, 1] = y16kk;

            // punkt PKK17
            double x17kk = -(C2B[8, 10, 0] - nx0) * pxx,
                    y17kk = -(C2B[8, 10, 1] - ny0) * pxy;
            PKK[16, 0] = x17kk;
            PKK[16, 1] = y17kk;

            // punkt PKK18
            double x18kk = -(C2B[8, 9, 0] - nx0) * pxx,
                    y18kk = -(C2B[8, 9, 1] - ny0) * pxy;
            PKK[17, 0] = x18kk;
            PKK[17, 1] = y18kk;

            // punkt PKK19
            double x19kk = -(C2B[8, 8, 0] - nx0) * pxx,
                    y19kk = -(C2B[8, 8, 1] - ny0) * pxy;
            PKK[18, 0] = x19kk;
            PKK[18, 1] = y19kk;

            // punkt PKK20
            double x20kk = -(C2B[8, 7, 0] - nx0) * pxx,
                    y20kk = -(C2B[8, 7, 1] - ny0) * pxy;
            PKK[19, 0] = x20kk;
            PKK[19, 1] = y20kk;

            // punkt PKK21
            double x21kk = -(C2B[8, 6, 0] - nx0) * pxx,
                    y21kk = -(C2B[8, 6, 1] - ny0) * pxy;
            PKK[20, 0] = x21kk;
            PKK[20, 1] = y21kk;

            // punkt PKK22
            double x22kk = -(C2B[7, 6, 0] - nx0) * pxx,
                    y22kk = -(C2B[7, 6, 1] - ny0) * pxy;
            PKK[21, 0] = x22kk;
            PKK[21, 1] = y22kk;

            // punkt PKK23
            double x23kk = -(C2B[6, 6, 0] - nx0) * pxx,
                    y23kk = -(C2B[6, 6, 1] - ny0) * pxy;
            PKK[22, 0] = x23kk;
            PKK[22, 1] = y23kk;

            // punkt PKK24
            double x24kk = -(C2B[5, 6, 0] - nx0) * pxx,
                    y24kk = -(C2B[5, 6, 1] - ny0) * pxy;
            PKK[23, 0] = x24kk;
            PKK[23, 1] = y24kk;

            //Utworzenie zbioru punktów PKK1-PKK24 stanowiacych opis obrazów odpowiednich punktów P1-P24 na matrycy kamery w układzie współrzędnych kamery xkyk
            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

            //&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
            // Szukanie trójek punktów A, B, C których współrzędne stanowią dane wejściowe do podprogramu kamera

            int l4 = 0;
            double[] s = new double[9];
            for (int i = 0; i < 9; i++)
            {
                s[i] = 0;
            }
            double fiw0 = Math.Atan(0.25) * 0.8;
            double dw = 1.5 * dx;
            double[] r1 = new double[3];
            double[] r2 = new double[3];
            double[] r3 = new double[3];
            double[] iwCross = new double[3];
            double iw;
            double _is;
            double fiw;
            double dr1;
            double dr2;
            double dr3;
            double xa,
                    ya,
                    za,
                    zak,
                    xb,
                    yb,
                    zb,
                    zbk,
                    xc,
                    yc,
                    zc,
                    zck,
                    xakk,
                    yakk,
                    xbkk,
                    ybkk,
                    xckk,
                    yckk;

            for (int l1 = 0; l1 < 24; l1++)
                for (int l2 = 0; l2 < 24; l2++)
                {
                    if (l1 == l2)
                    {
                        r1[0] = 0;
                        r1[1] = 0;
                        r1[2] = 0;
                    }
                    else
                    {
                        r1[0] = P[l2, 0] - P[l1, 0];
                        r1[1] = P[l2, 1] - P[l1, 1];
                        r1[2] = P[l2, 2] - P[l1, 2];
                    }

                    for (int l3 = 0; l3 < 24; l3++)
                        if (!((l1 == l2) || (l1 == l3) || (l2 == l3)))
                        {
                            r2[0] = P[l3, 0] - P[l2, 0];
                            r2[1] = P[l3, 1] - P[l2, 1];
                            r2[2] = P[l3, 2] - P[l2, 2];
                            r3[0] = P[l3, 0] - P[l1, 0];
                            r3[1] = P[l3, 1] - P[l1, 1];
                            r3[2] = P[l3, 2] - P[l1, 2];
                            //l1
                            //l2
                            //l3
                            dr1 = Math.Sqrt(Math.Pow(r1[0], 2) + Math.Pow(r1[1], 2) + Math.Pow(r1[2], 2));
                            dr2 = Math.Sqrt(Math.Pow(r2[0], 2) + Math.Pow(r2[1], 2) + Math.Pow(r2[2], 2));
                            dr3 = Math.Sqrt(Math.Pow(r3[0], 2) + Math.Pow(r3[1], 2) + Math.Pow(r3[2], 2));
                            iwCross[0] = r1[1] * r2[2] - r1[2] * r2[1];
                            iwCross[1] = r1[2] * r2[0] - r1[0] * r2[2];
                            iwCross[2] = r1[0] * r2[1] - r1[1] * r2[0];
                            iw = Math.Sqrt(Math.Pow(iwCross[0], 2) + Math.Pow(iwCross[1], 2) + Math.Pow(iwCross[2], 2));
                            _is = r1[0] * r2[0] + r1[1] * r2[1] + r1[2] * r2[2];
                            fiw = Math.Abs(Math.Atan(iw / _is));
                            if ((dr1 > dw) && (dr2 > dw) && (dr3 > dw) && (fiw > fiw0))
                            {
                                xa = P[l1, 0];
                                ya = P[l1, 1];
                                za = P[l1, 2];
                                zak = P[l1, 3];
                                xb = P[l2, 0];
                                yb = P[l2, 1];
                                zb = P[l2, 2];
                                zbk = P[l2, 3];
                                xc = P[l3, 0];
                                yc = P[l3, 1];
                                zc = P[l3, 2];
                                zck = P[l3, 3];
                                xakk = PKK[l1, 0];
                                yakk = PKK[l1, 1];
                                xbkk = PKK[l2, 0];
                                ybkk = PKK[l2, 1];
                                xckk = PKK[l3, 0];
                                yckk = PKK[l3, 1];
                                kamera(alfa, beta, gamma, x, y, z, zak, zbk, zck, xakk, yakk, xbkk, ybkk, xckk, yckk, xa, ya, za, xb, yb, zb, xc, yc, zc, fk, delta, T, xk);
                                for (int i = 0; i < 9; i++)
                                {
                                    s[i] = s[i] + xk[i];
                                }
                                l4 = l4 + 1;
                            }
                        }
                }
            double[] xk0 = new double[9];
            for (int i = 0; i < 9; i++)
            {
                xk0[i] = s[i] / (double)l4;
            }
            uklk(xk0[0], xk0[1], xk0[2], xk0[3], xk0[4], xk0[5], T);
            odwr(T, Todwr);
            punkt(C2B, Todwr, xkko, ykko, xkki, ykki, xod, yod, nx0, ny0, fk, dx, dy);
            //Program zwraca: 
            // a) obliczone współrzędne punktu xkko i ykko w [mm], z odczytanych współrzędnych punktów w pikselach w układzie kamery xkyk;
            // b) obliczone współrzędne punktu xkki i ykki w [mm], z obliczonych w układzie kamery xkyk (idealne nie zniekształcone optycznie),
            // za pomocą macierzy Tk;
            // c) obliczone współrzędne punktu xod i yod w [mm], w układzie odniesienia xyz
            double[,] Kx = new double[4, 5];
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 5; j++)
                    Kx[i, j] = 0;

            double[,] drm = new double[12, 16];
            int n;

            zniekszt(xkko, ykko, xkki, ykki, 1, Kx, K, drm, error[5]);
            //Program oblicznia współczynników optycznego zniekształcenia radialnego.  
            // Obliczone współczynniki program wpisuje do macierzy K, czterowierszowej, pięciokolumnowej.
            // W wierszach zapisane są wartości 5 współczynników k1, k2, k3, p1, p2.
            // Ponadto program oblicza maksymalne wartości bezwzględne różnic
            // dxm i dym współrzędnych kamery odczytanych (xkko, ykko)[mm] i współrzędnych kamery
            // idealnych (xkki,ykki)[mm], oraz odległości rm między punktami odczytanymi i
            // idealnymi[mm]. Dodatkowo dla każdej wartości maksymalnej wyznaczane są indeksy tych punktów. 
            int[] lwsp = new int[5];
            lwsp[0] = 0;
            lwsp[1] = 1;
            lwsp[2] = 2;
            lwsp[3] = 3;
            lwsp[4] = 5;

            for (int i = 0; i < 5; i++)
            {
                n = lwsp[i];
                korekta(xkkos, ykkos, xods, yods, xkko, ykko, xkki, ykki, K, T, n, fk);
                //Program obliczający współrzędne skorygowane xkkos i ykkos ze współrzędnych
                //odczytanych xkko i ykko. 
                //Ponadto obliczane są współrzędne xods i yods na
                //płaszczyźnie xy układu xyz, odpowiadające współrzędnym skorygowanym xkkos i ykkos. 
                //Korekta przprowadzana jest za pomocą modelu zniekształceń optycznych, opisanego n= 0, 1, 2 ,3 lub 5 współczynnikami. 
                Kx = K;
                zniekszt(xods, yods, xod, yod, 0, Kx, K, drm, error[i]);
                //Program oblicza maksymalne wartości bezwzględne różnic
                // dxm i dym współrzędnych (xod,yod) i (xods, yods) punktów na
                // powierzchni szablonu xy układu odniesienia w mm. 
                // xods i yods to współrzędne punktów sablonu odpowiadające współrzędnym skorygowanym xkkos i ykkos.
                // Dodatkowo dla każdej wartości maksymalnej wyznaczane są indeksy tych punktów.
            }
        }

        public static void odwr(double[,] T, double[,] Todwr)
        {
            //funkcja odwracająca macierz jednorodną 
            Todwr[0, 0] = T[0, 0];
            Todwr[0, 1] = T[1, 0];
            Todwr[0, 2] = T[2, 0];
            Todwr[0, 3] = -(T[0, 0] * T[0, 3] + T[1, 0] * T[1, 3] + T[2, 0] * T[2, 3]);
            Todwr[1, 0] = T[0, 1];
            Todwr[1, 1] = T[1, 1];
            Todwr[1, 2] = T[2, 1];
            Todwr[1, 3] = -(T[0, 1] * T[0, 3] + T[1, 1] * T[1, 3] + T[2, 1] * T[2, 3]);
            Todwr[2, 0] = T[0, 2];
            Todwr[2, 1] = T[1, 2];
            Todwr[2, 2] = T[2, 2];
            Todwr[2, 3] = -(T[0, 2] * T[0, 3] + T[1, 2] * T[1, 3] + T[2, 2] * T[2, 3]);
            Todwr[3, 0] = 0;
            Todwr[3, 1] = 0;
            Todwr[3, 2] = 0;
            Todwr[3, 3] = 1;
        }

        public static void punkt(double[, ,] C, double[,] Todwr, double[,] xkko, double[,] ykko, double[,] xkki,
                                  double[,] ykki, double[,] xod, double[,] yod, double nx0, double ny0, double fk, double dx, double dy)
        {
            double nxp,
                    nyp,
                    xpk,
                    ypk,
                    x0,
                    y0;
            //z0;
            double[] rp = new double[4];
            double[] rpk = new double[4];
            rpk[0] = 0;
            rpk[1] = 0;
            rpk[2] = 0;
            rpk[3] = 0;

            for (int j = 0; j < 16; j++)
                for (int i = 0; i < 12; i++)
                {
                    ///////////////////////
                    nxp = -(C[i, j, 0] - nx0);
                    nyp = -(C[i, j, 1] - ny0);
                    xpk = nxp * 2.8 * 1e-3;
                    ypk = nyp * 2.8 * 1e-3;
                    // Normalnie od i oraz j należy odjąc odpowiednie koordynaty środka
                    // ale że indeksowanie w C# zaczyna sie od zera, koordynaty są zmniejszone o jeden
                    // zamiast 7 jest 6 a zamiast 9 jest 8
                    x0 = (i - 6) * dx;
                    y0 = (j - 8) * dy;
                    //z0=0;
                    //////////////////////
                    xod[i, j] = x0;
                    yod[i, j] = y0;
                    xkko[i, j] = xpk;
                    ykko[i, j] = ypk;
                    rp[0] = xod[i, j];
                    rp[1] = yod[i, j];
                    rp[2] = 0;
                    rp[3] = 1;
                    for (int k = 0; k < 4; k++)
                    {
                        rpk[k] = 0;
                        for (int l = 0; l < 4; l++)
                            rpk[k] += Todwr[k, l] * rp[l];
                    }
                    xkki[i, j] = -rpk[0] / (rpk[2] / fk - 1);
                    ykki[i, j] = -rpk[1] / (rpk[2] / fk - 1);
                }
        }

        public static void zniekszt(double[,] xkko, double[,] ykko, double[,] xkki, double[,] ykki, int n, double[,] Kx, double[,] K, double[,] drm, double[] error)
        {
            double a11 = 0,
                    a12 = 0,
                    a13 = 0,
                    a14 = 0,
                    a15 = 0,
                    a21 = 0,
                    a22 = 0,
                    a23 = 0,
                    a24 = 0,
                    a25 = 0,
                    a31 = 0,
                    a32 = 0,
                    a33 = 0,
                    a34 = 0,
                    a35 = 0,
                    a41 = 0,
                    a42 = 0,
                    a43 = 0,
                    a44 = 0,
                    a45 = 0,
                    a51 = 0,
                    a52 = 0,
                    a53 = 0,
                    a54 = 0,
                    a55 = 0,
                    b1 = 0,
                    b2 = 0,
                    b3 = 0,
                    b4 = 0,
                    b5 = 0;

            double dx = 0, dy = 0, r2 = 0, dxm = 0, ixm = 0, jxm = 0, dym = 0, iym = 0, jym = 0, rm = 0, irm = 0, jrm = 0;

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    dx = -xkki[i, j] + xkko[i, j];
                    dy = -ykki[i, j] + ykko[i, j];
                    r2 = Math.Pow(xkki[i, j], 2) + Math.Pow(ykki[i, j], 2);

                    if (n > 0)
                    {
                        a11 = a11 + 2 * Math.Pow(r2, 3);
                        a12 = a12 + 2 * Math.Pow(r2, 4);
                        a13 = a13 + 2 * Math.Pow(r2, 5);
                        a14 = a14 + (2 * xkki[i, j] * r2) * (2 * xkki[i, j] * ykki[i, j] + r2 + 2 * Math.Pow(ykki[i, j], 2)) + (2 * ykki[i, j] * r2) * (r2 + 2 * Math.Pow(ykki[i, j], 2));
                        a15 = a15 + (2 * xkki[i, j] * r2) * (r2 + 2 * Math.Pow(xkki[i, j], 2)) + (2 * ykki[i, j] * r2) * (2 * xkki[i, j] * ykki[i, j]);
                        b1 = b1 + 2 * (dx * xkki[i, j] + dy * ykki[i, j]) * r2;

                        a21 = a21 + 2 * Math.Pow(r2, 4);
                        a22 = a22 + 2 * Math.Pow(r2, 5);
                        a23 = a23 + 2 * Math.Pow(r2, 6);
                        a24 = a24 + (2 * xkki[i, j] * Math.Pow(r2, 2)) * (2 * xkki[i, j] * ykki[i, j] + r2 + 2 * Math.Pow(ykki[i, j], 2)) + (2 * ykki[i, j] * Math.Pow(r2, 2)) * (r2 + 2 * Math.Pow(ykki[i, j], 2));
                        a25 = a25 + (2 * xkki[i, j] * Math.Pow(r2, 2)) * (r2 + 2 * Math.Pow(xkki[i, j], 2)) + (2 * ykki[i, j] * Math.Pow(r2, 2)) * (2 * xkki[i, j] * ykki[i, j]);
                        b2 = b2 + 2 * (dx * xkki[i, j] + dy * ykki[i, j]) * Math.Pow(r2, 2);

                        a31 = a31 + 2 * Math.Pow(r2, 5);
                        a32 = a32 + 2 * Math.Pow(r2, 6);
                        a33 = a33 + 2 * Math.Pow(r2, 7);
                        a34 = a34 + (2 * xkki[i, j] * Math.Pow(r2, 3)) * (2 * xkki[i, j] * ykki[i, j] + r2 + 2 * Math.Pow(ykki[i, j], 2)) + (2 * ykki[i, j] * Math.Pow(r2, 3)) * (r2 + 2 * Math.Pow(ykki[i, j], 2));
                        a35 = a35 + (2 * xkki[i, j] * Math.Pow(r2, 3)) * (r2 + 2 * Math.Pow(xkki[i, j], 2)) + (2 * ykki[i, j] * Math.Pow(r2, 3)) * (2 * xkki[i, j] * ykki[i, j]);
                        b3 = b3 + 2 * (dx * xkki[i, j] + dy * ykki[i, j]) * Math.Pow(r2, 3);

                        a41 = a41 + (4 * xkki[i, j] * ykki[i, j]) * (xkki[i, j] * r2) + 2 * (r2 + 2 * Math.Pow(ykki[i, j], 2)) * (r2 * ykki[i, j]);
                        a42 = a42 + (4 * xkki[i, j] * ykki[i, j]) * (xkki[i, j] * Math.Pow(r2, 2)) + 2 * (r2 + 2 * Math.Pow(ykki[i, j], 2)) * (Math.Pow(r2, 2) * ykki[i, j]);
                        a43 = a43 + (4 * xkki[i, j] * ykki[i, j]) * (xkki[i, j] * Math.Pow(r2, 3)) + 2 * (r2 + 2 * Math.Pow(ykki[i, j], 2)) * (Math.Pow(r2, 3) * ykki[i, j]);
                        a44 = a44 + (4 * xkki[i, j] * ykki[i, j]) * (2 * xkki[i, j] * ykki[i, j]) + 2 * Math.Pow((r2 + 2 * Math.Pow(ykki[i, j], 2)), 2);
                        a45 = a45 + (4 * xkki[i, j] * ykki[i, j]) * (r2 + 2 * Math.Pow(xkki[i, j], 2)) + 2 * (r2 + 2 * Math.Pow(ykki[i, j], 2)) * (2 * xkki[i, j] * ykki[i, j]);
                        b4 = b4 + (4 * xkki[i, j] * ykki[i, j]) * dx + 2 * (r2 + 2 * Math.Pow(ykki[i, j], 2)) * dy;

                        a51 = a51 + 2 * (r2 + 2 * Math.Pow(xkki[i, j], 2)) * (xkki[i, j] * r2) + (4 * xkki[i, j] * ykki[i, j]) * (ykki[i, j] * r2);
                        a52 = a52 + 2 * (r2 + 2 * Math.Pow(xkki[i, j], 2)) * (xkki[i, j] * Math.Pow(r2, 2)) + (4 * xkki[i, j] * ykki[i, j]) * (ykki[i, j] * Math.Pow(r2, 2));
                        a53 = a53 + 2 * (r2 + 2 * Math.Pow(xkki[i, j], 2)) * (xkki[i, j] * Math.Pow(r2, 3)) + (4 * xkki[i, j] * ykki[i, j]) * (ykki[i, j] * Math.Pow(r2, 3));
                        a54 = a54 + 2 * (r2 + 2 * Math.Pow(xkki[i, j], 2)) * (2 * xkki[i, j] * ykki[i, j]) + (4 * xkki[i, j] * ykki[i, j]) * (r2 + 2 * Math.Pow(ykki[i, j], 2));
                        a55 = a55 + 2 * Math.Pow((r2 + 2 * Math.Pow(xkki[i, j], 2)), 2) + (4 * xkki[i, j] * ykki[i, j]) * (2 * xkki[i, j] * ykki[i, j]);
                        b5 = b5 + 2 * dx * (r2 + 2 * Math.Pow(xkki[i, j], 2)) * dx + (4 * xkki[i, j] * ykki[i, j]) * dy;
                    }

                    drm[i, j] = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));

                    if ((i == 0) && (j == 0))
                    {
                        dxm = Math.Abs(dx);
                        ixm = i;
                        jxm = j;
                        dym = Math.Abs(dy);
                        iym = i;
                        jym = j;
                        rm = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
                        irm = i;
                        jrm = j;
                    }
                    if ((i > 0) || (j > 0))
                    {
                        if (dxm < Math.Abs(dx))
                        {
                            dxm = Math.Abs(dx);
                            ixm = i;
                            jxm = j;
                        }
                        if (dym < Math.Abs(dy))
                        {
                            dym = Math.Abs(dy);
                            iym = i;
                            jym = j;
                        }
                        if (rm < Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2)))
                        {
                            rm = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
                            irm = i;
                            jrm = j;
                        }
                    }

                }
            }

            error[0] = dxm;
            error[1] = ixm+1;
            error[2] = jxm+1;
            error[3] = dym;
            error[4] = iym+1;
            error[5] = jym+1;
            error[6] = rm;
            error[7] = irm+1;
            error[8] = jrm+1;

            //Console.WriteLine();
            //Console.WriteLine("Moduł maksymalnego błędu dxm(ixm,jxm) współrzędnej x punktu obrazu odczytanego w [mm]");
            //Console.WriteLine(dxm);
            //Console.WriteLine(ixm + 1);
            //Console.WriteLine(jxm + 1);
            //Console.WriteLine("Moduł maksymalnego błędu dym(iym,jym) współrzędnej y punktu obrazu odczytanego w [mm]");
            //Console.WriteLine(dym);
            //Console.WriteLine(iym + 1);
            //Console.WriteLine(jym + 1);
            //Console.WriteLine("Maksymalny błąd drm(irm,jrm) odległości punktu obrazu odczytanego w [mm]");
            //Console.WriteLine(rm);
            //Console.WriteLine(irm + 1);
            //Console.WriteLine(jrm + 1);
            if (n > 0)
            {
                //A=[a11];
                //wa=det(A);
                //B=[b1]; 
                double k = b1 / a11;
                K[0, 0] = k;
                K[0, 1] = 0;
                K[0, 2] = 0;
                K[0, 3] = 0;
                K[0, 4] = 0;

                //A=[a11 a12; a21 a22];
                //wa=det(A);
                //B=[b1; b2];
                double[][] A2 = new double[2][];
                A2[0] = new double[2];
                A2[1] = new double[2];
                double[] k2 = new double[2];
                A2[0][0] = a11;
                A2[0][1] = a12;
                A2[1][0] = a21;
                A2[1][1] = a22;
                A2 = InvertMatrix(A2);
                k2[0] = A2[0][0] * b1 + A2[0][1] * b2;
                k2[1] = A2[1][0] * b1 + A2[1][1] * b2;
                K[1, 0] = k2[0];
                K[1, 1] = k2[1];
                K[1, 2] = 0;
                K[1, 3] = 0;
                K[1, 4] = 0;

                //A=[a11 a12 a13; a21 a22 a23 ; a31 a32 a33];
                //wa=det(A);
                //B=[b1; b2; b3];
                double[][] A3 = new double[3][];
                A3[0] = new double[3];
                A3[1] = new double[3];
                A3[2] = new double[3];
                double[] B3 = new double[3];
                double[] k3 = new double[3];
                B3[0] = b1;
                B3[1] = b2;
                B3[2] = b3;
                A3[0][0] = a11;
                A3[0][1] = a12;
                A3[0][2] = a13;
                A3[1][0] = a21;
                A3[1][1] = a22;
                A3[1][2] = a23;
                A3[2][0] = a31;
                A3[2][1] = a32;
                A3[2][2] = a33;
                A3 = InvertMatrix(A3);
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        k3[i] += A3[i][j] * B3[j];

                K[2, 0] = k3[0];
                K[2, 1] = k3[1];
                K[2, 2] = k3[2];
                K[2, 3] = 0;
                K[2, 4] = 0;

                //A=[a11 a12 a13 a14 a15; a21 a22 a23 a24 a25; a31 a32 a33 a34 a35; a41 a42 a43 a44 a45; a51 a52 a53 a54 a55];
                //wa=det(A);
                //B=[b1; b2; b3; b4; b5]; 
                double[][] A5 = new double[5][];
                A5[0] = new double[5];
                A5[1] = new double[5];
                A5[2] = new double[5];
                A5[3] = new double[5];
                A5[4] = new double[5];
                double[] B5 = new double[5];
                double[] k5 = new double[5];
                B5[0] = b1;
                B5[1] = b2;
                B5[2] = b3;
                B5[3] = b4;
                B5[4] = b5;
                A5[0][0] = a11;
                A5[0][1] = a12;
                A5[0][2] = a13;
                A5[0][3] = a14;
                A5[0][4] = a15;
                A5[1][0] = a21;
                A5[1][1] = a22;
                A5[1][2] = a23;
                A5[1][3] = a24;
                A5[1][4] = a25;
                A5[2][0] = a31;
                A5[2][1] = a32;
                A5[2][2] = a33;
                A5[2][3] = a34;
                A5[2][4] = a35;
                A5[3][0] = a41;
                A5[3][1] = a42;
                A5[3][2] = a43;
                A5[3][3] = a44;
                A5[3][4] = a45;
                A5[4][0] = a51;
                A5[4][1] = a52;
                A5[4][2] = a53;
                A5[4][3] = a54;
                A5[4][4] = a55;
                A5 = InvertMatrix(A5);
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        k5[i] += A5[i][j] * B5[j];

                K[3, 0] = k5[0];
                K[3, 1] = k5[1];
                K[3, 2] = k5[2];
                K[3, 3] = k5[3];
                K[3, 4] = k5[4];
            }
            if (n == 0)
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 5; j++)
                        K[i, j] = Kx[i, j];

            //Console.WriteLine("*******************************************************************");
            //Console.WriteLine("*******************************************************************");
        }

        public static void korekta(double[,] xkkos, double[,] ykkos, double[,] xods, double[,] yods, double[,] xkko, double[,] ykko, double[,] xkki, double[,] ykki, double[,] K, double[,] T, int n, double fk)
        {
            //if((n<0) || (n>5) || (n==4))
            //{
            //    disp(' ')
            //    disp('n=')
            //    disp(n)
            //    disp('niewłaściwa liczba współczynników');
            //    stop
            //}
            //Console.WriteLine("Korekta przprowadzona za pomocą modelu zniekształceń optycznych, opisanego n współczynnikami");
            //Console.WriteLine(n);
            double k1 = 0, k2 = 0, k3 = 0, p1 = 0, p2 = 0;
            int nx = n;
            if (n == 5)
                nx = 3;
            if (n > 0)
            {
                //load K
                k1 = K[nx, 0];
                //Console.WriteLine(k1);
                k2 = K[nx, 1];
                //Console.WriteLine(k2);
                k3 = K[nx, 2];
                //Console.WriteLine(k2);
                p1 = K[nx, 3];
                //Console.WriteLine(p1);
                p2 = K[nx, 4];
                //Console.WriteLine(p2);
            }
            //if(n==0)
            //{
            //    k1=0;
            //    k2=0;
            //    k3=0;
            //    p1=0;
            //    p2=0;
            //}
            //load Tk
            //fk=5.01;
            double[] rfk = new double[4];
            rfk[0] = 0;
            rfk[1] = 0;
            rfk[2] = fk;
            rfk[3] = 1;
            double xf, yf, zf;
            double[] rf = new double[4];
            for (int i = 0; i < 4; i++)
            {
                rf[i] = 0;
                for (int j = 0; j < 4; j++)
                    rf[i] += T[i, j] * rfk[j];
            }
            xf = rf[0];
            yf = rf[1];
            zf = rf[2];
            double r2, dx, dy, xp, yp, zp, kf;
            double[] rpk = new double[4];
            double[] rp = new double[4];
            for (int i = 0; i < 12; i++)
                for (int j = 0; j < 16; j++)
                {
                    r2 = Math.Pow(xkki[i, j], 2) + Math.Pow(ykki[i, j], 2);
                    dx = xkki[i, j] * (k1 * r2 + k2 * Math.Pow(r2, 2) + k3 * Math.Pow(r2, 3) + 2 * p1 * ykki[i, j]) + p2 * (r2 + 2 * Math.Pow(xkki[i, j], 2));
                    dy = ykki[i, j] * (k1 * r2 + k2 * Math.Pow(r2, 2) + k3 * Math.Pow(r2, 3) + 2 * p2 * xkki[i, j]) + p1 * (r2 + 2 * Math.Pow(ykki[i, j], 2));
                    xkkos[i, j] = xkko[i, j] - dx;
                    ykkos[i, j] = ykko[i, j] - dy;
                    rpk[0] = xkkos[i, j];
                    rpk[1] = ykkos[i, j];
                    rpk[2] = 0;
                    rpk[3] = 1;
                    for (int q = 0; q < 4; q++)
                    {
                        rp[q] = 0;
                        for (int w = 0; w < 4; w++)
                            rp[q] += T[q, w] * rpk[w];
                    }
                    xp = rp[0];
                    yp = rp[1];
                    zp = rp[2];
                    kf = -zf / (zp - zf);
                    xods[i, j] = kf * (xp - xf) + xf;
                    yods[i, j] = kf * (yp - yf) + yf;
                }
        }
        public static void doklB1B2(double[,] xkki1, double[,] ykki1, double zf11, double[,] xkki2, double[,] ykki2, double zf22, double[,] Tk1, double[,] Tk2, double[,] xod, double[,] yod)
        {
            //Program analizujący dokładność obliczeń położenia punktów szablonu  w układzie odniesienia xyz w milimetrach. 
            // Obliczenia wkonane są w programie odl0 na podstawie współrzędnych xk11 yk11 obrazu punktu P na matrycy kamery B1 
            // oraz współrzędnych xk22 yk22 obrazu tego samego punktu na matrycy kamery B2. 
            double xk11, yk11, xk22, yk22, dx, dy, dz, dxm = 0, ixm = 0, jxm = 0, dym = 0, iym = 0, jym = 0, dzm = 0, izm = 0, jzm = 0, rm = 0, irm = 0, jrm = 0;
            double[] rp = new double[4];
            double[,] bx = new double[12, 16];
            double[,] by = new double[12, 16];
            double[,] bz = new double[12, 16];
            double[,] drm = new double[12, 16];
            double[,] br = new double[12, 16];

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    xk11 = xkki1[i, j];
                    yk11 = ykki1[i, j];
                    xk22 = xkki2[i, j];
                    yk22 = ykki2[i, j];
                    odl0(xk11, yk11, zf11, xk22, yk22, zf22, Tk1, Tk2, rp);
                    dx = rp[0] - xod[i, j];
                    dy = rp[1] - yod[i, j];
                    dz = rp[2];
                    bx[i, j] = dx;
                    by[i, j] = dy;
                    bz[i, j] = dz;
                    drm[i, j] = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2) + Math.Pow(dz, 2));
                    br[i, j] = drm[i, j];

                    if ((i == 0) && (j == 0))
                    {
                        dxm = Math.Abs(dx);
                        ixm = i + 1;
                        jxm = j + 1;
                        dym = Math.Abs(dy);
                        iym = i + 1;
                        jym = j + 1;
                        dzm = Math.Abs(dz);
                        izm = i + 1;
                        jzm = j + 1;
                        rm = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2) + Math.Pow(dz, 2));
                        irm = i;
                        jrm = j;
                    }
                    if ((i > 0) || (j > 0))
                    {
                        if (dxm < Math.Abs(dx))
                        {
                            dxm = Math.Abs(dx);
                            ixm = i + 1;
                            jxm = j + 1;
                        }
                        if (dym < Math.Abs(dy))
                        {
                            dym = Math.Abs(dy);
                            iym = i + 1;
                            jym = j + 1;
                        }
                        if (dzm < Math.Abs(dz))
                        {
                            dzm = Math.Abs(dz);
                            izm = i + 1;
                            jzm = j + 1;
                        }

                        if (rm < Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2) + Math.Pow(dz, 2)))
                        {
                            rm = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2) + Math.Pow(dz, 2));
                            irm = i + 1;
                            jrm = j + 1;
                        }
                    }
                }
            }

            //Console.WriteLine();
            //Console.WriteLine("Moduł maksymalnego błędu dxm(ixm,jxm) współrzędnej x punktu obrazu obliczonego w [mm]");
            //Console.WriteLine(dxm);
            //Console.WriteLine(ixm);
            //Console.WriteLine(jxm);
            //Console.WriteLine("Moduł maksymalnego błędu dym(iym,jym) współrzędnej y punktu obrazu obliczonego w [mm]");
            //Console.WriteLine(dym);
            //Console.WriteLine(iym);
            //Console.WriteLine(jym);
            //Console.WriteLine("Moduł maksymalnego błędu dzm(izm,jzm) współrzędnej z punktu obrazu obliczonego w [mm]");
            //Console.WriteLine(dzm);
            //Console.WriteLine(izm);
            //Console.WriteLine(jzm);
            //Console.WriteLine("Maksymalny błąd drm(irm,jrm) odległości punktu obrazu obliczonego w [mm]");
            //Console.WriteLine(rm);
            //Console.WriteLine(irm);
            //Console.WriteLine(jrm);
        }

        public static void odl0(double xk11, double yk11, double zf11, double xk22, double yk22, double zf22, double[,] Tk1, double[,] Tk2, double[] rp)
        {
            double[] c1 = new double[3];
            double[] c2 = new double[3];
            double[] d = new double[3];
            for (int i = 0; i < 3; i++)
            {
                c1[i] = Tk1[i, 2];
                c2[i] = Tk2[i, 2];
                d[i] = Tk1[i, 3] - Tk2[i, 3];
            }
            double fi1 = kat(c1, c2);
            double fi2 = kat(c1, d);
            double fi3 = kat(c2, d);
            double fig = pi / 6;
            if ((fi1 < fig) && (fi2 < fig) && (fi3 < fig))
            {
                fi1 = fi1 * 180 / pi;
                fi2 = fi2 * 180 / pi;
                fi3 = fi3 * 180 / pi;
                //Console.WriteLine();
                //Console.WriteLine(" za mały kąt fi1 między osiami kamer c1 i c2");
                //Console.WriteLine(" i za małe kąty fi2 i fi3 między osiami  kamer c1 i c2 i wektorem d łączącym początki układów współrzędnych kamer");

                //Console.WriteLine();//(['fi1=',int2str(fi1),' stopni, ', 'fi2=',int2str(fi1),' stopni, ', 'fi3=',int2str(fi1),' stopni.'])
                Environment.Exit(0);
            }
            double[] rk11 = new double[4];
            double[] rk22 = new double[4];
            double[] rf11 = new double[4];
            double[] rf22 = new double[4];
            double[] rk1 = new double[4];
            double[] rk2 = new double[4];
            double[] rf1 = new double[4];
            double[] rf2 = new double[4];
            rk11[0] = xk11;
            rk11[1] = yk11;
            rk11[2] = 0;
            rk11[3] = 1;
            rk22[0] = xk22;
            rk22[1] = yk22;
            rk22[2] = 0;
            rk22[3] = 1;
            rf11[0] = 0;
            rf11[1] = 0;
            rf11[2] = zf11;
            rf11[3] = 1;
            rf22[0] = 0;
            rf22[1] = 0;
            rf22[2] = zf22;
            rf22[3] = 1;
            for (int i = 0; i < 4; i++)
            {
                rk1[i] = 0;
                rk2[i] = 0;
                rf1[i] = 0;
                rf2[i] = 0;
                for (int j = 0; j < 4; j++)
                {
                    rk1[i] += Tk1[i, j] * rk11[j];
                    rk2[i] += Tk2[i, j] * rk22[j];
                    rf1[i] += Tk1[i, j] * rf11[j];
                    rf2[i] += Tk2[i, j] * rf22[j];
                }
            }
            double xk1 = rk1[0],
                    yk1 = rk1[1],
                    zk1 = rk1[2],
                    xk2 = rk2[0],
                    yk2 = rk2[1],
                    zk2 = rk2[2],
                    xf1 = rf1[0],
                    yf1 = rf1[1],
                    zf1 = rf1[2],
                    xf2 = rf2[0],
                    yf2 = rf2[1],
                    zf2 = rf2[2],
                    l1 = xf1 - xk1,
                    m1 = yf1 - yk1,
                    n1 = zf1 - zk1,
                    l2 = xf2 - xk2,
                    m2 = yf2 - yk2,
                    n2 = zf2 - zk2;
            int nx = -1;
            // rozwiązania wynikające z równań (alfa.3b)
            // M=-m1*l2+l1*m2;
            double[] x = new double[6];
            double[] y = new double[6];
            double[] z = new double[6];
            //for (int i = 0; i < 6; i++)
            //{
            //    x[i] = 0;
            //    x[i] = 1;
            //    x[i] = 2;
            //}

            double[][] A1 = new double[3][];
            for (int i = 0; i < 3; i++)
                A1[i] = new double[3];
            double[] B1 = new double[3];
            double[] X1 = new double[3];
            A1[0][0] = m1;
            A1[0][1] = -l1;
            A1[0][2] = 0;
            A1[1][0] = 0;
            A1[1][1] = n1;
            A1[1][2] = -m1;
            A1[2][0] = m2;
            A1[2][1] = -l2;
            A1[2][2] = 0;

            B1[0] = m1 * xk1 - l1 * yk1;
            B1[1] = n1 * yk1 - m1 * zk1;
            B1[2] = m2 * xk2 - l2 * yk2;

            double M1 = det(A1);
            //if abs(M1)<1e-0
            //    M1
            //    disp('równanie alfa.3b |det(A1)|<1e-0 ')
            //end
            if (Math.Abs(M1) >= 1)
            {
                nx++;
                //x(nx)=(-l2*(m1*xk1-l1*yk1)+l1*(m2*xk2-l2*yk2))/M;
                //y(nx)=(-m2*(m1*xk1-l1*yk1)+m1*(m2*xk2-l2*yk2))/M;
                //z(nx)=(-n1*m2*(m1*xk1-l1*yk1))/(M*m1)-(n1*yk1-m1*zk1)/m1+n1*(m2*xk2-l2*yk2)/M;
                //X1=A1\B1;
                A1 = InvertMatrix(A1);
                for (int i = 0; i < 3; i++)
                {
                    X1[i] = 0;
                    for (int j = 0; j < 3; j++)
                        X1[i] += A1[i][j] * B1[j];
                }
                x[nx] = X1[0];
                y[nx] = X1[1];
                z[nx] = X1[2];
            }
            // rozwiązania wynikające z równań (alfa.4b)
            //M=n1*l2-l1*n2;
            double[][] A2 = new double[3][];
            for (int i = 0; i < 3; i++)
                A2[i] = new double[3];
            double[] B2 = new double[3];
            double[] X2 = new double[3];
            A2[0][0] = m1;
            A2[0][1] = -l1;
            A2[0][2] = 0;
            A2[1][0] = 0;
            A2[1][1] = n1;
            A2[1][2] = -m1;
            A2[2][0] = n2;
            A2[2][1] = 0;
            A2[2][2] = -l2;

            B2[0] = m1 * xk1 - l1 * yk1;
            B2[1] = n1 * yk1 - m1 * zk1;
            B2[2] = n2 * xk2 - l2 * zk2;

            //disp('alfa 4b')
            double M2 = det(A2);
            //if abs(M2)<1e-0
            //    M2
            //    disp('równanie alfa.4b |detA(2)|<1e-0')
            //end
            if (Math.Abs(M2) >= 1)
            {
                nx = nx + 1;
                //x(nx)=(n1*l2*(m1*xk1-l1*yk1)+l1*l2*(n1*yk1-m1*zk1))/(M*m1)-(l1*(n2*xk2-l2*zk2))/M;
                //y(nx)=(n2*(m1*xk1-l1*yk1)+l2*(n1*yk1-m1*zk1)-m1*(n2*xk2-l2*zk2))/M;
                //z(nx)=(n1*n2*(m1*xk1-l1*yk1)+l1*n2*(n1*yk1-m1*zk1))/(m1*M)-(n1*(n2*xk2-l2*zk2))/M;
                A2 = InvertMatrix(A2);
                for (int i = 0; i < 3; i++)
                {
                    X2[i] = 0;
                    for (int j = 0; j < 3; j++)
                        X2[i] += A2[i][j] * B2[j];
                }
                x[nx] = X2[0];
                y[nx] = X2[1];
                z[nx] = X2[2];
            }
            // rozwiązania wynikające z równań (alfa.5b)
            //M=n1*m2-m1*n2;
            double[][] A3 = new double[3][];
            for (int i = 0; i < 3; i++)
                A3[i] = new double[3];
            double[] B3 = new double[3];
            double[] X3 = new double[3];
            A3[0][0] = m1;
            A3[0][1] = -l1;
            A3[0][2] = 0;
            A3[1][0] = 0;
            A3[1][1] = n1;
            A3[1][2] = -m1;
            A3[2][0] = 0;
            A3[2][1] = n2;
            A3[2][2] = -m2;

            B3[0] = m1 * xk1 - l1 * yk1;
            B3[1] = n1 * yk1 - m1 * zk1;
            B3[2] = n2 * yk2 - m2 * zk2;
            //disp('alfa 5b')
            double M3 = det(A3);
            //if abs(M3)<1e-0 
            //    M3
            //    disp('równanie alfa.5b |det(A3)<1e-0')
            //end
            if (Math.Abs(M3) >= 1)
            {
                nx = nx + 1;
                //x(nx)=(m1*xk1-l1*yk1)/m1+(l1*m2*(n1*yk1-m1*zk1))/(M*m1)-(l1*(n2*yk2-m2*zk2))/M;
                //y(nx)=(m2*(n1*yk1-m1*zk1)-m1*(n2*yk2-m2*zk2))/M;
                //z(nx)=(n2*(n1*yk1-m1*zk1)-n1*(n2*yk2-m2*zk2))/M;
                A3 = InvertMatrix(A3);
                for (int i = 0; i < 3; i++)
                {
                    X3[i] = 0;
                    for (int j = 0; j < 3; j++)
                        X3[i] += A3[i][j] * B3[j];
                }
                x[nx] = X3[0];
                y[nx] = X3[1];
                z[nx] = X3[2];
            }
            // rozwiązania wynikające z równań (alfa.6b)
            //M=-m1*l2+l1*m2;
            double[][] A4 = new double[3][];
            for (int i = 0; i < 3; i++)
                A4[i] = new double[3];
            double[] B4 = new double[3];
            double[] X4 = new double[3];
            A4[0][0] = m1;
            A4[0][1] = -l1;
            A4[0][2] = 0;
            A4[1][0] = m2;
            A4[1][1] = -l2;
            A4[1][2] = 0;
            A4[2][0] = 0;
            A4[2][1] = n2;
            A4[2][2] = -m2;

            B4[0] = m1 * xk1 - l1 * yk1;
            B4[1] = m2 * xk2 - l2 * yk2;
            B4[2] = n2 * yk2 - m2 * zk2;
            //disp('alfa 6b')
            double M4 = det(A4);
            //if abs(M4)<1e-0 
            //    M4
            //    disp('równanie alfa.6b |det(A4)|<1e-0 ')
            //end
            if (Math.Abs(M4) >= 1)
            {
                nx = nx + 1;
                //x(nx)=(-l2*(m1*xk1-l1*yk1)+l1*(m2*xk2-l2*yk2))/M;
                //y(nx)=(-m2*(m1*xk1-l1*yk1)+m1*(m2*xk2-l2*yk2))/M;
                //z(nx)=(-n2*(m1*xk1-l1*yk1))/M+(m1*n2*(m2*xk2-l2*yk2))/(M*m2)-(n2*yk2-m2*zk2)/m2;
                A4 = InvertMatrix(A4);
                for (int i = 0; i < 3; i++)
                {
                    X4[i] = 0;
                    for (int j = 0; j < 3; j++)
                        X4[i] += A4[i][j] * B4[j];
                }
                x[nx] = X4[0];
                y[nx] = X4[1];
                z[nx] = X4[2];
            }
            // rozwiązania wynikające z równań (alfa.7b)
            //M=n1*l2-l1*n2;
            double[][] A5 = new double[3][];
            for (int i = 0; i < 3; i++)
                A5[i] = new double[3];
            double[] B5 = new double[3];
            double[] X5 = new double[3];
            A5[0][0] = n1;
            A5[0][1] = 0;
            A5[0][2] = -l1;
            A5[1][0] = m2;
            A5[1][1] = -l2;
            A5[1][2] = 0;
            A5[2][0] = 0;
            A5[2][1] = n2;
            A5[2][2] = -m2;

            B5[0] = n1 * xk1 - l1 * zk1;
            B5[1] = m2 * xk2 - l2 * yk2;
            B5[2] = n2 * yk2 - m2 * zk2;
            //disp('alfa 7b')
            double M5 = det(A5);
            //if abs(M5)<1e-0
            //    M5
            //    disp('równanie alfa.7b |det(A5)|<1e-0')
            //end
            if (Math.Abs(M5) >= 1)
            {
                nx = nx + 1;
                //x(nx)=(l2*(n1*xk1-l1*zk1))/M-(l1*n2*(m2*xk2-l2*yk2)+l1*l2*(n2*yk2-m2*zk2))/(M*m2);
                //y(nx)=(m2*(n1*xk1-l1*zk1)-n1*(m2*xk2-l2*yk2)-l1*(n2*yk2-m2*zk2))/M;
                //z(nx)=(n2*(n1*xk1-l1*zk1))/M-(n1*n2*(m2*xk2-l2*yk2)+n1*l2*(n2*yk2-m2*zk2))/(M*m2);
                A5 = InvertMatrix(A5);
                for (int i = 0; i < 3; i++)
                {
                    X5[i] = 0;
                    for (int j = 0; j < 3; j++)
                        X5[i] += A5[i][j] * B5[j];
                }
                x[nx] = X5[0];
                y[nx] = X5[1];
                z[nx] = X5[2];
            }
            // rozwiązania wynikające z równań (alfa.8b)
            //M=n1*m2-m1*n2;
            double[][] A6 = new double[3][];
            for (int i = 0; i < 3; i++)
                A6[i] = new double[3];
            double[] B6 = new double[3];
            double[] X6 = new double[3];
            A6[0][0] = 0;
            A6[0][1] = n1;
            A6[0][2] = -m1;
            A6[1][0] = m2;
            A6[1][1] = -l2;
            A6[1][2] = 0;
            A6[2][0] = 0;
            A6[2][1] = n2;
            A6[2][2] = -m2;

            B6[0] = n1 * yk1 - m1 * zk1;
            B6[1] = m2 * xk2 - l2 * yk2;
            B6[2] = n2 * yk2 - m2 * zk2;
            //disp('alfa 8b')
            double M6 = det(A6);
            //if abs(M6)<1e-0
            //    M6
            //    disp('równanie alfa.8b |det(A6)|<1e-0')
            //end
            if (Math.Abs(M6) >= 1)
            {
                nx = nx + 1;
                //x(nx)=(l2*(n1*yk1-m1*zk1))/M+(m2*xk2-l2*yk2)/m2-(m1*l2*(n2*yk2-m2*zk2))/(M*m2);
                //y(nx)=(m2*(n1*yk1-m1*zk1)-m1*(n2*yk2-m2*zk2))/M;
                //z(nx)=(n2*(n1*yk1-m1*zk1)-n1*(n2*yk2-m2*zk2))/M;
                A6 = InvertMatrix(A6);
                for (int i = 0; i < 3; i++)
                {
                    X6[i] = 0;
                    for (int j = 0; j < 3; j++)
                        X6[i] += A6[i][j] * B6[j];
                }
                x[nx] = X6[0];
                y[nx] = X6[1];
                z[nx] = X6[2];
            }
            if (nx == -1)
            {
                //Console.WriteLine();
                //Console.WriteLine("nx=0, brak rozwiązań");
                Environment.Exit(0);
            }
            if (nx >= 0)
            {
                //nx
                //x
                //y
                //z
                double sx = 0,
                        sy = 0,
                        sz = 0;
                for (int i = 0; i < 6; i++)
                {
                    sx = sx + x[i];
                    sy = sy + y[i];
                    sz = sz + z[i];
                }
                double x1 = sx / (nx + 1),
                        y1 = sy / (nx + 1),
                        z1 = sz / (nx + 1);
                rp[0] = x1;
                rp[1] = y1;
                rp[2] = z1;
                rp[3] = 1;
            }
        }

        public static double det(double[][] A)
        {
            double result;
            result = A[0][0] * A[1][1] * A[2][2] + A[1][0] * A[2][1] * A[0][2] + A[2][0] * A[0][1] * A[1][2] - A[0][2] * A[1][1] * A[2][0] - A[1][2] * A[2][1] * A[0][0] - A[2][2] * A[0][1] * A[1][0];

            return result;
        }
        public static double kat(double[] A, double[] B)
        {
            double[] L = new double[3];
            double Ls;
            L[0] = A[1] * B[2] - A[2] * B[1];
            L[1] = A[2] * B[0] - A[0] * B[2];
            L[2] = A[0] * B[1] - A[1] * B[0];
            Ls = Math.Sqrt(Math.Pow(L[0], 2) + Math.Pow(L[1], 2) + Math.Pow(L[2], 2));
            double M = Math.Abs(A[0] * B[0] + A[1] * B[1] + A[2] * B[2]);
            double fi = Math.Atan2(Ls, M);
            return fi;
        }
        public static void koordynaty( double[,] T, double[,] object1, double[,] object2, int missing, int nullcounter, double zf1, double zf2, 
                                        double[,] Tk1, double[,] Tk2)
        {
            double[] rp1 = new double[4];
            double[] rp2 = new double[4];
            double[] rp3 = new double[4];
            double[] rpA = new double[3];
            double[] rpB = new double[3];
            double[] rpC = new double[3];
            double[] rpD = new double[3];
            T[3,0] = 0;
            T[3,1] = 0;
            T[3,2] = 0;
            T[3,3] = 1;

            if ((nullcounter == 0) || (missing == 3))
            {
                odl0(object1[0, 0], object1[0, 1], zf1, object2[0, 0], object2[0, 1], zf2, Tk1, Tk2, rp1);
                odl0(object1[1, 0], object1[1, 1], zf1, object2[1, 0], object2[1, 1], zf2, Tk1, Tk2, rp2);
                odl0(object1[3, 0], object1[3, 1], zf1, object2[3, 0], object2[3, 1], zf2, Tk1, Tk2, rp3);
                rpB[0] = (rp1[0] - rp2[0]) / Math.Sqrt(Math.Pow((rp2[0] - rp1[0]), 2) + Math.Pow((rp2[1] - rp1[1]), 2) + Math.Pow((rp2[2] - rp1[2]), 2));
                rpB[1] = (rp1[1] - rp2[1]) / Math.Sqrt(Math.Pow((rp2[0] - rp1[0]), 2) + Math.Pow((rp2[1] - rp1[1]), 2) + Math.Pow((rp2[2] - rp1[2]), 2));
                rpB[2] = (rp1[2] - rp2[2]) / Math.Sqrt(Math.Pow((rp2[0] - rp1[0]), 2) + Math.Pow((rp2[1] - rp1[1]), 2) + Math.Pow((rp2[2] - rp1[2]), 2));
                rpA[0] = (rp1[0] - rp3[0]) / Math.Sqrt(Math.Pow((rp3[0] - rp1[0]), 2) + Math.Pow((rp3[1] - rp1[1]), 2) + Math.Pow((rp3[2] - rp1[2]), 2));
                rpA[1] = (rp1[1] - rp3[1]) / Math.Sqrt(Math.Pow((rp3[0] - rp1[0]), 2) + Math.Pow((rp3[1] - rp1[1]), 2) + Math.Pow((rp3[2] - rp1[2]), 2));
                rpA[2] = (rp1[2] - rp3[2]) / Math.Sqrt(Math.Pow((rp3[0] - rp1[0]), 2) + Math.Pow((rp3[1] - rp1[1]), 2) + Math.Pow((rp3[2] - rp1[2]), 2));
                rpC[0] = rpA[1] * rpB[2] - rpA[2] * rpB[1];
                rpC[1] = rpA[2] * rpB[0] - rpA[0] * rpB[2];
                rpC[2] = rpA[0] * rpB[1] - rpA[1] * rpB[0];
                rpD[0] = rp1[0] - ((rp1[0] - rp3[0]) / 2) - ((rp1[0] - rp2[0]) / 2);
                rpD[1] = rp1[1] - ((rp1[1] - rp3[1]) / 2) - ((rp1[1] - rp2[1]) / 2);
                rpD[2] = rp1[2] - ((rp1[2] - rp3[2]) / 2) - ((rp1[2] - rp2[2]) / 2);
                for (int i = 0; i < 3; i++)
                {
                    T[i, 0] = rpA[i];
                    T[i, 1] = rpB[i];
                    T[i, 2] = rpC[i];
                    T[i, 3] = rpD[i];
                }
            }
            else
                switch (missing)
                {
                    case 1:
                        odl0(object1[1, 0], object1[1, 1], zf1, object2[1, 0], object2[1, 1], zf2, Tk1, Tk2, rp1);
                        odl0(object1[2, 0], object1[2, 1], zf1, object2[2, 0], object2[2, 1], zf2, Tk1, Tk2, rp2);
                        odl0(object1[3, 0], object1[3, 1], zf1, object2[3, 0], object2[3, 1], zf2, Tk1, Tk2, rp3);

                        rpA[0] = (rp1[0] - rp2[0]) / Math.Sqrt(Math.Pow((rp2[0] - rp1[0]), 2) + Math.Pow((rp2[1] - rp1[1]), 2) + Math.Pow((rp2[2] - rp1[2]), 2));
                        rpA[1] = (rp1[1] - rp2[1]) / Math.Sqrt(Math.Pow((rp2[0] - rp1[0]), 2) + Math.Pow((rp2[1] - rp1[1]), 2) + Math.Pow((rp2[2] - rp1[2]), 2));
                        rpA[2] = (rp1[2] - rp2[2]) / Math.Sqrt(Math.Pow((rp2[0] - rp1[0]), 2) + Math.Pow((rp2[1] - rp1[1]), 2) + Math.Pow((rp2[2] - rp1[2]), 2));
                        rpB[0] = (rp3[0] - rp2[0]) / Math.Sqrt(Math.Pow((rp3[0] - rp2[0]), 2) + Math.Pow((rp3[1] - rp2[1]), 2) + Math.Pow((rp3[2] - rp2[2]), 2));
                        rpB[1] = (rp3[1] - rp2[1]) / Math.Sqrt(Math.Pow((rp3[0] - rp2[0]), 2) + Math.Pow((rp3[1] - rp2[1]), 2) + Math.Pow((rp3[2] - rp2[2]), 2));
                        rpB[2] = (rp3[2] - rp2[2]) / Math.Sqrt(Math.Pow((rp3[0] - rp2[0]), 2) + Math.Pow((rp3[1] - rp2[1]), 2) + Math.Pow((rp3[2] - rp2[2]), 2));
                        rpC[0] = rpA[1] * rpB[2] - rpA[2] * rpB[1];
                        rpC[1] = rpA[2] * rpB[0] - rpA[0] * rpB[2];
                        rpC[2] = rpA[0] * rpB[1] - rpA[1] * rpB[0];
                        rpD[0] = rp1[0] + ((rp3[0] - rp2[0]) / 2) - ((rp1[0] - rp2[0]) / 2);
                        rpD[1] = rp1[1] + ((rp3[1] - rp2[1]) / 2) - ((rp1[1] - rp2[1]) / 2);
                        rpD[2] = rp1[2] + ((rp3[2] - rp2[2]) / 2) - ((rp1[2] - rp2[2]) / 2);
                        for (int i = 0; i < 3; i++)
                        {
                            T[i, 0] = rpA[i];
                            T[i, 1] = rpB[i];
                            T[i, 2] = rpC[i];
                            T[i, 3] = rpD[i];
                        }
                        break;

                    case 2:
                        odl0(object1[0, 0], object1[0, 1], zf1, object2[0, 0], object2[0, 1], zf2, Tk1, Tk2, rp1);
                        odl0(object1[2, 0], object1[2, 1], zf1, object2[2, 0], object2[2, 1], zf2, Tk1, Tk2, rp2);
                        odl0(object1[3, 0], object1[3, 1], zf1, object2[3, 0], object2[3, 1], zf2, Tk1, Tk2, rp3);

                        rpA[0] = (rp1[0] - rp3[0]) / Math.Sqrt(Math.Pow((rp1[0] - rp3[0]), 2) + Math.Pow((rp1[1] - rp3[1]), 2) + Math.Pow((rp1[2] - rp3[2]), 2));
                        rpA[1] = (rp1[1] - rp3[1]) / Math.Sqrt(Math.Pow((rp1[0] - rp3[0]), 2) + Math.Pow((rp1[1] - rp3[1]), 2) + Math.Pow((rp1[2] - rp3[2]), 2));
                        rpA[2] = (rp1[2] - rp3[2]) / Math.Sqrt(Math.Pow((rp1[0] - rp3[0]), 2) + Math.Pow((rp1[1] - rp3[1]), 2) + Math.Pow((rp1[2] - rp3[2]), 2));
                        rpB[0] = (rp3[0] - rp2[0]) / Math.Sqrt(Math.Pow((rp3[0] - rp2[0]), 2) + Math.Pow((rp3[1] - rp2[1]), 2) + Math.Pow((rp3[2] - rp2[2]), 2));
                        rpB[1] = (rp3[1] - rp2[1]) / Math.Sqrt(Math.Pow((rp3[0] - rp2[0]), 2) + Math.Pow((rp3[1] - rp2[1]), 2) + Math.Pow((rp3[2] - rp2[2]), 2));
                        rpB[2] = (rp3[2] - rp2[2]) / Math.Sqrt(Math.Pow((rp3[0] - rp2[0]), 2) + Math.Pow((rp3[1] - rp2[1]), 2) + Math.Pow((rp3[2] - rp2[2]), 2));
                        rpC[0] = rpA[1] * rpB[2] - rpA[2] * rpB[1];
                        rpC[1] = rpA[2] * rpB[0] - rpA[0] * rpB[2];
                        rpC[2] = rpA[0] * rpB[1] - rpA[1] * rpB[0];
                        rpD[0] = rp1[0] - ((rp1[0] - rp3[0]) / 2) - ((rp3[0] - rp2[0]) / 2);
                        rpD[1] = rp1[1] - ((rp1[1] - rp3[1]) / 2) - ((rp3[1] - rp2[1]) / 2);
                        rpD[2] = rp1[2] - ((rp1[2] - rp3[2]) / 2) - ((rp3[2] - rp2[2]) / 2);
                        for (int i = 0; i < 3; i++)
                        {
                            T[i, 0] = rpA[i];
                            T[i, 1] = rpB[i];
                            T[i, 2] = rpC[i];
                            T[i, 3] = rpD[i];
                        }
                        break;

                    case 4:
                        odl0(object1[0, 0], object1[0, 1], zf1, object2[0, 0], object2[0, 1], zf2, Tk1, Tk2, rp1);
                        odl0(object1[1, 0], object1[1, 1], zf1, object2[1, 0], object2[1, 1], zf2, Tk1, Tk2, rp2);
                        odl0(object1[2, 0], object1[2, 1], zf1, object2[2, 0], object2[2, 1], zf2, Tk1, Tk2, rp3);

                        rpA[0] = (rp2[0] - rp3[0]) / Math.Sqrt(Math.Pow((rp2[0] - rp3[0]), 2) + Math.Pow((rp2[1] - rp3[1]), 2) + Math.Pow((rp2[2] - rp3[2]), 2));
                        rpA[1] = (rp2[1] - rp3[1]) / Math.Sqrt(Math.Pow((rp2[0] - rp3[0]), 2) + Math.Pow((rp2[1] - rp3[1]), 2) + Math.Pow((rp2[2] - rp3[2]), 2));
                        rpA[2] = (rp2[2] - rp3[2]) / Math.Sqrt(Math.Pow((rp2[0] - rp3[0]), 2) + Math.Pow((rp2[1] - rp3[1]), 2) + Math.Pow((rp2[2] - rp3[2]), 2));
                        rpB[0] = (rp1[0] - rp2[0]) / Math.Sqrt(Math.Pow((rp1[0] - rp2[0]), 2) + Math.Pow((rp1[1] - rp2[1]), 2) + Math.Pow((rp1[2] - rp2[2]), 2));
                        rpB[1] = (rp1[1] - rp2[1]) / Math.Sqrt(Math.Pow((rp1[0] - rp2[0]), 2) + Math.Pow((rp1[1] - rp2[1]), 2) + Math.Pow((rp1[2] - rp2[2]), 2));
                        rpB[2] = (rp1[2] - rp2[2]) / Math.Sqrt(Math.Pow((rp1[0] - rp2[0]), 2) + Math.Pow((rp1[1] - rp2[1]), 2) + Math.Pow((rp1[2] - rp2[2]), 2));
                        rpC[0] = rpA[1] * rpB[2] - rpA[2] * rpB[1];
                        rpC[1] = rpA[2] * rpB[0] - rpA[0] * rpB[2];
                        rpC[2] = rpA[0] * rpB[1] - rpA[1] * rpB[0];
                        rpD[0] = rp1[0] - ((rp2[0] - rp3[0]) / 2) - ((rp1[0] - rp2[0]) / 2);
                        rpD[1] = rp1[1] - ((rp2[1] - rp3[1]) / 2) - ((rp1[1] - rp2[1]) / 2);
                        rpD[2] = rp1[2] - ((rp2[2] - rp3[2]) / 2) - ((rp1[2] - rp2[2]) / 2);
                        for (int i = 0; i < 3; i++)
                        {
                            T[i, 0] = rpA[i];
                            T[i, 1] = rpB[i];
                            T[i, 2] = rpC[i];
                            T[i, 3] = rpD[i];
                        }
                        break;
                }
        }
    }
}
