using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using ThoughtWorks.QRCode.Codec;

namespace Source.Admin.Web.Common
{
    public static class QRCodeHelp
    {

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="codID"></param>
        /// <returns></returns>
        public static byte[] GetCodBit(string url,int Scale,int Version,int XY)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = Scale;// 3;
            qrCodeEncoder.QRCodeVersion = Version;// 0;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            Bitmap bmp = qrCodeEncoder.Encode(url);
            int su = XY;
            bmp = new Bitmap(bmp, su, su);


            MemoryStream ms = new MemoryStream();
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] bytes = ms.GetBuffer();
            ms.Close();
            
            return bytes;
        }

    }
}