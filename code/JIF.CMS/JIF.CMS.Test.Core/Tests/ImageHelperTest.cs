using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using JIF.CMS.Core.Helpers;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class ImageHelperTest
    {
        // 1920 * 1100
        private static readonly string _imgSrc = @"C:\Users\Administrator\Desktop\ImageHelperTest\timg.jpg";

        private static readonly string _outputSrc = @"C:\Users\Administrator\Desktop\ImageHelperTest\";

        [TestMethod]
        public void MakeThumbnail_Test()
        {
            ImageHelper.MakeThumbnail(_imgSrc, _outputSrc + @"\W - 192 - 110.jpg", 192, 110, ImageHelper.ThumbnailType.W);
            ImageHelper.MakeThumbnail(_imgSrc, _outputSrc + @"\W - 180 - 80.jpg", 180, 80, ImageHelper.ThumbnailType.W);
            ImageHelper.MakeThumbnail(_imgSrc, _outputSrc + @"\W - 200 - 200.jpg", 200, 200, ImageHelper.ThumbnailType.W);

            ImageHelper.MakeThumbnail(_imgSrc, _outputSrc + @"\H - 192 - 110.jpg", 192, 110, ImageHelper.ThumbnailType.H);
            ImageHelper.MakeThumbnail(_imgSrc, _outputSrc + @"\H - 180 - 80.jpg", 180, 80, ImageHelper.ThumbnailType.H);
            ImageHelper.MakeThumbnail(_imgSrc, _outputSrc + @"\H - 200 - 200.jpg", 200, 200, ImageHelper.ThumbnailType.H);

            ImageHelper.MakeThumbnail(_imgSrc, _outputSrc + @"\HW - 192 - 110.jpg", 192, 110, ImageHelper.ThumbnailType.HW);
            ImageHelper.MakeThumbnail(_imgSrc, _outputSrc + @"\HW - 180 - 80.jpg", 180, 80, ImageHelper.ThumbnailType.HW);
            ImageHelper.MakeThumbnail(_imgSrc, _outputSrc + @"\HW - 200 - 200.jpg", 200, 200, ImageHelper.ThumbnailType.HW);

            ImageHelper.MakeThumbnail(_imgSrc, _outputSrc + @"\Cut - 192 - 110.jpg", 192, 110, ImageHelper.ThumbnailType.Cut);
            ImageHelper.MakeThumbnail(_imgSrc, _outputSrc + @"\Cut - 180 - 80.jpg", 180, 80, ImageHelper.ThumbnailType.Cut);
            ImageHelper.MakeThumbnail(_imgSrc, _outputSrc + @"\Cut - 200 - 200.jpg", 200, 200, ImageHelper.ThumbnailType.Cut);
        }

        [TestMethod]
        public void Test_Generate_VerifyCodeImg()
        {
            var verifycodes = RandomHelper.GenStringList(RandomHelper.CharSchemeEnum.NumCharL, 4, 100);

            foreach (var code in verifycodes)
            {
                using (var imgBuffer = new MemoryStream(ImageHelper.GenValidateCode(code, 120, 40)))
                {
                    Image.FromStream(imgBuffer).Save(string.Format("{0}{1}.jpg", _outputSrc, code));
                }
            }
        }
    }
}
