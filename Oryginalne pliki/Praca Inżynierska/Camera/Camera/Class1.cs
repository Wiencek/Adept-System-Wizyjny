using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Controls;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;

using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Camera
{
    public static class GlobalVar
    {
        /// <summary>
        /// Global variable that is constant.
        /// </summary>
        public const string GlobalString = "Important Text";


        public static Bitmap bitmap, Picture, Picture2;
        public static int thresholdno, thresholdSize, ErodeCount, DilateCount;
        public static MJPEGStream stream;

        public static  void Dilate()
        {
            Rectangle rect = new Rectangle(0, 0, GlobalVar.Picture2.Width, GlobalVar.Picture2.Height);
            System.Drawing.Imaging.BitmapData bmpData =
            GlobalVar.Picture2.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
            GlobalVar.Picture2.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;
            double temp, temp2;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * GlobalVar.Picture.Height;
            byte[] rgbValues = new byte[bytes];
            byte[] rgbValues2 = new byte[bytes];


            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues2, 0, bytes);


            for (int counter = 5 + 4 * GlobalVar.Picture2.Width; counter < rgbValues.Length - 3 - 4 * GlobalVar.Picture2.Width; counter += 4)
            {

                if (rgbValues[counter] == 0)
                {
                    if (rgbValues[counter] != rgbValues[counter - 4] ||
                        rgbValues[counter] != rgbValues[counter + 4] ||
                        rgbValues[counter] != rgbValues[counter - 4 * GlobalVar.Picture2.Width] ||
                        rgbValues[counter] != rgbValues[counter + 4 * GlobalVar.Picture2.Width] ||
                        rgbValues[counter] != rgbValues[counter + (4 * GlobalVar.Picture2.Width) - 4] ||
                        rgbValues[counter] != rgbValues[counter + (4 * GlobalVar.Picture2.Width) + 4] ||
                        rgbValues[counter] != rgbValues[counter - (4 * GlobalVar.Picture2.Width) - 4] ||
                        rgbValues[counter] != rgbValues[counter - (4 * GlobalVar.Picture2.Width) - 4])
                    {
                        rgbValues2[counter] = 255;
                        rgbValues2[counter + 1] = 255;
                        rgbValues2[counter - 1] = 255;
                    }
                }

            }



            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues2, 0, ptr, bytes);


            // Unlock the bits.
            GlobalVar.Picture2.UnlockBits(bmpData);
        }

        public static void Erode()
        {
            Rectangle rect = new Rectangle(0, 0, GlobalVar.Picture2.Width, GlobalVar.Picture2.Height);
            System.Drawing.Imaging.BitmapData bmpData =
            GlobalVar.Picture2.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
            GlobalVar.Picture2.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;
            double temp, temp2;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * GlobalVar.Picture.Height;
            byte[] rgbValues = new byte[bytes];
            byte[] rgbValues2 = new byte[bytes];


            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues2, 0, bytes);


            for (int counter = 5 + 4 * GlobalVar.Picture2.Width; counter < rgbValues.Length - 3 - 4 * GlobalVar.Picture2.Width; counter += 4)
            {

                if (rgbValues[counter] == 255)
                {
                    if (rgbValues[counter] != rgbValues[counter - 4] ||
                        rgbValues[counter] != rgbValues[counter + 4] ||
                        rgbValues[counter] != rgbValues[counter - 4 * GlobalVar.Picture2.Width] ||
                        rgbValues[counter] != rgbValues[counter + 4 * GlobalVar.Picture2.Width] ||
                        rgbValues[counter] != rgbValues[counter + (4 * GlobalVar.Picture2.Width) - 4] ||
                        rgbValues[counter] != rgbValues[counter + (4 * GlobalVar.Picture2.Width) + 4] ||
                        rgbValues[counter] != rgbValues[counter - (4 * GlobalVar.Picture2.Width) - 4] ||
                        rgbValues[counter] != rgbValues[counter - (4 * GlobalVar.Picture2.Width) - 4])
                    {
                        rgbValues2[counter] = 0;
                        rgbValues2[counter + 1] = 0;
                        rgbValues2[counter - 1] = 0;
                        //counter += 4;
                    }
                }

            }



            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues2, 0, ptr, bytes);


            // Unlock the bits.
            GlobalVar.Picture2.UnlockBits(bmpData);
        }
        public static void threshold()
        {
            Rectangle rect = new Rectangle(0, 0, GlobalVar.Picture.Width, GlobalVar.Picture.Height);
            GlobalVar.bitmap = (Bitmap)GlobalVar.Picture.Clone();
            System.Drawing.Imaging.BitmapData bmpData =
            GlobalVar.bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
            GlobalVar.bitmap.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;
            double temp;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * GlobalVar.bitmap.Height;
            byte[] rgbValues = new byte[bytes];


            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            for (int counter = 1; counter < rgbValues.Length; counter += 3)
            {
                temp = rgbValues[counter - 1] * 0.0721 + rgbValues[counter] * 0.7154 + rgbValues[counter + 1] * 0.2125;//grey scale conversion

                if (temp > GlobalVar.thresholdno - GlobalVar.thresholdSize && temp < GlobalVar.thresholdno + GlobalVar.thresholdSize)
                {
                    rgbValues[counter] = 255;
                    rgbValues[counter - 1] = 255;
                    rgbValues[counter + 1] = 255;
                }
                else
                {
                    rgbValues[counter] = 000;
                    rgbValues[counter - 1] = 000;
                    rgbValues[counter + 1] = 000;
                }
            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            GlobalVar.bitmap.UnlockBits(bmpData);
            GlobalVar.Picture2 = new Bitmap(GlobalVar.bitmap);
        }
    }
}
