using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace Source.Admin.Web.Common
{
    public class ImagesHelper
    {
        //使用方法调用GenerateHighThumbnail()方法即可  
        //参数oldImagePath表示要被缩放的图片路径  
        //参数newImagePath表示缩放后保存的图片路径  
        //参数width和height分别是缩放范围宽和高  
        public static void GenerateHighThumbnail(string oldImagePath, string newImagePath, int width, int height)
        {
            System.Drawing.Image oldImage = System.Drawing.Image.FromFile(oldImagePath);
            int newWidth = AdjustSize(width, height, oldImage.Width, oldImage.Height).Width;
            int newHeight = AdjustSize(width, height, oldImage.Width, oldImage.Height).Height;
            //。。。。。。。。。。。  
            System.Drawing.Image thumbnailImage = oldImage.GetThumbnailImage(newWidth, newHeight, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(thumbnailImage);
            //处理JPG质量的函数  
            ImageCodecInfo ici = GetEncoderInfo("image/jpeg");
            if (ici != null)
            {
                System.Drawing.Imaging.EncoderParameters ep = new System.Drawing.Imaging.EncoderParameters(1);
                ep.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)100);
                bm.Save(newImagePath, ici, ep);
                //释放所有资源，不释放，可能会出错误。  
                ep.Dispose();
                ep = null;
            }
            ici = null;
            bm.Dispose();
            bm = null;
            thumbnailImage.Dispose();
            thumbnailImage = null;
            oldImage.Dispose();
            oldImage = null;

        }


        private static bool ThumbnailCallback()
        {
            return false;
        }


        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }


        public struct PicSize
        {
            public int Width;
            public int Height;
        }


        public static PicSize AdjustSize(int spcWidth, int spcHeight, int orgWidth, int orgHeight)
        {
            PicSize size = new PicSize();
            // 原始宽高在指定宽高范围内，不作任何处理   
            if (orgWidth <= spcWidth && orgHeight <= spcHeight)
            {
                size.Width = orgWidth;
                size.Height = orgHeight;
            }
            else
            {
                // 取得比例系数   
                float w = orgWidth / (float)spcWidth;
                float h = orgHeight / (float)spcHeight;
                // 宽度比大于高度比   
                if (w > h)
                {
                    size.Width = spcWidth;
                    size.Height = (int)(w >= 1 ? Math.Round(orgHeight / w) : Math.Round(orgHeight * w));
                }
                // 宽度比小于高度比   
                else if (w < h)
                {
                    size.Height = spcHeight;
                    size.Width = (int)(h >= 1 ? Math.Round(orgWidth / h) : Math.Round(orgWidth * h));
                }
                // 宽度比等于高度比   
                else
                {
                    size.Width = spcWidth;
                    size.Height = spcHeight;
                }
            }
            return size;
        }

        /// <summary>
        /// 接受前台 剪切大小
        /// </summary>
        /// <param name="path">原来文件路径</param>
        /// <param name="npath">文件新路径</param>
        /// <param name="px"></param>
        /// <param name="py"></param>
        /// <param name="pw"></param>
        /// <param name="ph"></param>
        /// <returns></returns>
        public static Boolean imgCut(String path, String npath, float px, float py, float pw, float ph)//, int scaleWidth, int scaleHeight
        {
            try
            {
                //string imgpath = string.Empty;
                Bitmap b = new Bitmap(path);

                string extend = Path.GetExtension(path).ToLower();

                //剪裁图片

                RectangleF rec = new RectangleF(px, py, pw, ph);
                Bitmap nb = b.Clone(rec, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                //从重保存图片
                //ppath = ppath.Replace(extend, "") + "_sml" + extend;

                nb.Save(npath);
                b.Dispose();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }


        }
    }
}