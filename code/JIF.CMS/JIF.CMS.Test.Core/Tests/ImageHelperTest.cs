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
        private static readonly string _imgSrc = @"E:\WorkDocument\JIF.Common\JIF.Common.Test\assert\1.jpg";

        private static readonly string _outputSrc = @"C:\Users\Administrator\Desktop\img-fix";

        [TestMethod]
        public void Test_MakeThumbnail()
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
        public void Test_BatchMaskThumbnail()
        {
            var folder = @"E:\WorkDocument\Waldorf\code\Waldorf.Web\file\images\20160728\";

            #region file names

            var filenames = new List<string>() {
                "6a25026817dc47bdb03f38297114484a",
                "4e986c2d4a7f436a996c3b65fecf36fa",
                "b69ceb0ff38e429aad6c03f016af0a60",
                "f44e44326d6841108c8b9157e8f72555",
                "656d37c0ca0047d284a4a746d96f94d4",
                "a9a45b5e9f474533b4f9f4ff1446e1b0",
                "33f31a8afab24bc688173edf6fc2cf69",
                "ddf03745c51a4ab694c76dd54596a4e4",
                "4c691f1705f6403f94f4b6bef6eac1e2",
                "b38fa0a93a0f4b92a7374f972ba58a8e",
                "e53c3ab5f86d4e6a9ebf4b8fd007ef64",
                "abee7b10984c45ffb955ca71a7a31311",
                "41a16fbcadf6449785a05f48b942fc5a",
                "d7cd4ba67b0442cf8b9494ccc2f846f5",
                "29509b001ca447abbf19fa6c95700cdb",
                "ce21b44db3904be9954df78bb75f5ca2",
                "afa0dd69c62249bb9a9e7f389ebe4989",
                "aa18e78069ab48fcb69aec8a1e5dad1f",
                "78a08cbd53ac4af89a775c2530779f51",
                "7f55d44d699b45d0b5f427eeaa2f9fe0",
                "9c14d227feab4f628283b91946ff7ae2",
                "e51b116a416e4b028a33ca84e3a953ce",
                "a42c1d5da7c349689500a9c8a11d8847",
                "720a51a7f4264d03a843466ba8bd2a6a",
                "89da77d8c8f947498b8845260c424ef9",
                "29c96439165c4df7a5c2864c1821d8d3",
                "8f6db44764534eaa9448d808d58e1f05",
                "855d4a820d5c44beb27c9483505a3ebb",
                "eb94004865d34ce1bd2031c31670684e",
                "7c41a86caa094f23a183bc41c0ea25f2",
                "ed35634884ad495f8306a3f02896b233",
                "3da244c1696743c0877cb459a7fc1fb4",
                "3bb9a00b87774d40ae910aa61c19f87d",
                "91a662dc650d411abcf9b80ec9786b23",
                "5d4ac77bc8d341578a9c6e97a2f8e492",
                "9792c4f3a4ea40fc895a0ead0384b1b5",
                "c6477dac52d24be9a18d3c530d4b7971",
                "9d8de849fcd549c5a40f5f7f0f07d4f3",
                "2e09787d2c4d4373bf3d1848ed4b4cb1",
                "05471ba806f047799605d9501d1e48be",
                "88892b489b4e45b0860e0fbacdb61491",
                "2ae49fa9a3cb4ad8805152cf3409c465",
                "6f26c6c4e8ca4a0ebf5f80d397108315",
                "8a9703fc27e2485b801c5f9be2955b1a",
                "087e5792576f4b0f8a92172864596045",
                "86ad1decde784cca8cdda55410cebed5",
                "6e31033563ba4537b4439e21a164410d",
                "c29d6e137c0a4027abeecfb49615a8da",
                "6dc6f75b41ce4f739953ea57918cd9e9",
                "28fd50970e2943d2b8b4e1ac5863a5eb",
                "632cf1b67e4c4d8e9f2da3773aea2af5",
                "39f65f8ad8684004a3a3729895aa8b8b",
                "8b063f2f7e4d4c1fb6fae3fafb6385fb",
                "b0c7cd9b48924afb86e0b9e75c18618b",
                "5ce54adf3f6e454caeab04bd2a149aab",
                "d7de032b93a144c9aacdb1aa886f79e7",
                "e908ee8437684dd089f5f03f0b7c0b01",
                "bab7598ae4834ebba4f82f979701c727",
                "ae9ffcf7fb654d98b132b78c2eacdd0b",
                "d519408ead874d8ab549c38b0d05845d",
                "9368dbb144454fa3979b0fda8aadb276",
                "df8aaefa9bb14d509e62e4c63da8b758",
                "8ec1ddc5f17543479e1151a57a358291",
                "20a8797566b0468b9d7397414e869d5f",
                "53aabdc7fdf644b887b7b792d8303a3d",
                "07442ad1f6344947a172df1692449d52",
                "04ea9b4979e340dd9bf2dbe0f65d88e5",
                "acb916541aed4cd3a839c92c76b7c47e",
                "583fcf1aa0a24c8993e37ba181b06192",
                "a8aeaa5f23054a9687169f4835eca8b8",
                "6fe8554f4ead4026b1dfba47a6d08b61",
                "c75bd89708d443e89d5d01a4a37ab17e",
                "da5c010bb05c4ce4b19f258566fbb947",
                "9de2ec232e484a48aed62e2548d461f2",
                "5acd46fe865d4ffc942cf8ece1142985",
                "cf12e8cb6ae9451f9da344c7a662d0d4",
                "83da1e05b41247ecbcb312df50a112ab",
                "f5538fde35ae46039fb3cae63aa5fb69",
                "9a6b1ad588c44cef94ae721d548b5665",
                "6fb5afbc60e94116a3d1dd08376232d0",
                "bf0abb5f0ae246ef8c78434ca2431469",
                "a14cc3f12ae3479ab321201758415204",
                "21eeb02985184554abcb470057ea8078",
                "ad98992cabf8473a9395cd15061111cb",
                "042717d0a1414d998c5442b2d0e67bb6",
                "d9b7df857dc5428dbaa6fea6014f4a69",
                "420b106253b742748d1c2484a2f5e7c5",
                "9907d89b83e34350a62a5d7dc72ad74d",
                "818200fae2be479b9dc65a176bd15af3",
                "7156f56540a440afba55fe4bee228f54",
                "0414bae0378646c28ef1a6300e47248c",
                "76edfbd2f60a4fba9ce48d17995c3ed3",
                "00b4ebd699d94e6cafdd47ce7fa16e3e",
                "2b620e5b740243f7afd24a56f1d7c841",
                "d34d29885eee43319d503f6b84a7bd4c",
                "7bc0ce2d95bc4987bc6eab11ad2ae6f4",
                "47fe013f0901405dba68acb3e6624bc1",
                "335a552f25724ac7b378b34ba8c1ce18",
                "920dbf7714284bfa9862f5c343a3552c",
                "fca364d5032d43b88b1c19a48a50a26b",
                "da7b1777b4124c978c359bb95bee5f4d",
                "ed3f73720e554780b86d86ee17989c36"};
            #endregion

            foreach (var fn in filenames)
            {

                var ofn = folder + fn + ".jpg";
                var tfn = @"E:\WorkDocument\Waldorf\code\Waldorf.Web\file\images\20160728\" + fn + "_thumb.jpg";
                ImageHelper.MakeThumbnail(ofn, tfn, 260, 175, ImageHelper.ThumbnailType.Cut);

                //File.Move(ofn, @"E:\WorkDocument\Waldorf\code\Waldorf.Web\file\images\20160728\" + fn.Substring(8) + ".jpg");
            }
        }

        [TestMethod]
        public void Test_SystemPathTruncate()
        {
            var path = @"/file/images//201607287156f56540a440afba55fe4bee228f54.jpg";

            Console.WriteLine("GetFileName : " + Path.GetFileName(path));
            Console.WriteLine("GetFileNameWithoutExtension : " + Path.GetFileNameWithoutExtension(path));
            Console.WriteLine("GetDirectoryName : " + Path.GetDirectoryName(path));
            Console.WriteLine("GetExtension : " + Path.GetExtension(path));
        }


        [TestMethod]
        public void Test_Generate_VerifyCodeImg()
        {
            //var f = @"C:\Users\Administrator\Desktop\gen-verifycode\";
            //var verifycodes = RandomHelper.Gen(RandomHelper.Format.NumChar, 4, 100);

            //foreach (var code in verifycodes)
            //{
            //    using (var imgBuffer = ImageHelper.GenValidateCode(code, 120, 40))
            //    {
            //        Bitmap.FromStream(imgBuffer).Save(string.Format("{0}{1}.jpg", f, code));
            //    }
            //}
        }
    }
}
