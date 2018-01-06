using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace JIF.CMS.Core.Helpers
{
    public static class ImageHelper
    {
        public enum ThumbnailType
        {
            /// <summary>
            /// 指定高宽缩放（可能变形）
            /// </summary>
            HW,
            /// <summary>
            /// 指定宽，高按比例
            /// </summary>
            W,
            /// <summary>
            /// 指定高，宽按比例
            /// </summary>
            H,
            /// <summary>
            /// 指定高宽裁减（不变形）, 裁剪图片正中位置
            /// </summary>
            Cut,
        }

        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式（w:指定宽，高度自适应；h:指定高度，宽度自适应；cut:裁剪,图片中心向外；默认w）</param>   
        public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, ThumbnailType mode)
        {
            try
            {
                using (Image originalImage = Image.FromFile(originalImagePath))
                {

                    if (mode != ThumbnailType.Cut)
                    {
                        double d1 = double.Parse(width.ToString()) / double.Parse(height.ToString());

                        //宽高比
                        double wd = double.Parse(originalImage.Width.ToString()) / double.Parse(originalImage.Height.ToString());
                        //高宽比
                        double hd = double.Parse(originalImage.Height.ToString()) / double.Parse(originalImage.Width.ToString());


                        double itsw = 0d;
                        double itsh = 0d;

                        //与宽高比对比
                        if (d1 > wd)
                        {
                            itsw = d1 - wd;
                        }
                        else
                        {
                            itsw = wd - d1;
                        }

                        //与高宽比对比
                        if (d1 > hd)
                        {
                            itsh = d1 - hd;
                        }
                        else
                        {
                            itsh = hd - d1;
                        }

                        //如果高宽比更接近比例
                        if (itsw > itsh)
                        {
                            mode = ThumbnailType.W;
                        }
                        else
                        {
                            mode = ThumbnailType.H;
                        }
                    }


                    int towidth = width;
                    int toheight = height;

                    int x = 0;
                    int y = 0;
                    int ow = originalImage.Width;
                    int oh = originalImage.Height;

                    switch (mode)
                    {
                        case ThumbnailType.HW://指定高宽缩放（可能变形） 
                            break;
                        case ThumbnailType.W://指定宽，高按比例
                            toheight = originalImage.Height * width / originalImage.Width;
                            break;
                        case ThumbnailType.H://指定高，宽按比例
                            towidth = originalImage.Width * height / originalImage.Height;
                            break;
                        case ThumbnailType.Cut://指定高宽裁减（不变形）
                            if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight)
                            {
                                oh = originalImage.Height;
                                ow = originalImage.Height * towidth / toheight;
                                y = 0;
                                x = (originalImage.Width - ow) / 2;
                            }
                            else
                            {
                                ow = originalImage.Width;
                                oh = originalImage.Width * height / towidth;
                                x = 0;
                                y = (originalImage.Height - oh) / 2;
                            }
                            break;
                        default:
                            break;
                    }

                    //新建一个bmp图片
                    Image bitmap = new Bitmap(towidth, toheight);

                    //新建一个画板
                    Graphics g = Graphics.FromImage(bitmap);

                    //设置高质量插值法
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

                    //设置高质量,低速度呈现平滑程度
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    //清空画布并以透明背景色填充
                    g.Clear(Color.Transparent);

                    //在指定位置并且按指定大小绘制原图片的指定部分
                    g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight), new Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);

                    try
                    {
                        //以jpg格式保存缩略图
                        bitmap.Save(thumbnailPath, ImageFormat.Jpeg);
                    }
                    finally
                    {
                        originalImage.Dispose();
                        bitmap.Dispose();
                        g.Dispose();
                    }
                }
            }
            catch
            {

            }
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="code">验证码文字</param>
        /// <param name="imgWidth">图片宽度</param>
        /// <param name="imgHeight">图片高度</param>
        /// <returns></returns>
        public static byte[] GenValidateCode(string code, int width = 120, int height = 40)
        {
            if (string.IsNullOrWhiteSpace(code) || width < 1 || height < 1)
            {
                throw new ArgumentException("ImageHelper : GenerateValidateCode param err.");
            }

            // 可选字体
            //string[] oFontNames = { "华文彩云", "方正舒体", "华文琥珀", "华文行楷", "Calibri (西文正文)", "Arial Black" };
            string[] oFontNames = { "Calibri (西文正文)", "Arial Black" };

            // 字符栅格大小, 用于控制每个字符书写位置
            var lattice = width / code.Length;

            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);

            try
            {
                g.Clear(Color.White);

                //背景噪点生成
                for (int i = 0; i < 100; i++)
                {
                    Pen bgnoise = new Pen(RandomHelper.GenColor(100), 0);
                    int x1 = RandomHelper.GenNumber(0, width);
                    int y1 = RandomHelper.GenNumber(0, height);
                    g.DrawRectangle(bgnoise, x1, y1, 1, 1);
                }

                // 背景干扰线
                for (int i = 0; i < 10; i++)
                {
                    Pen pen = new Pen(RandomHelper.GenColor(100), RandomHelper.GenNumber(0, 5));
                    var p1 = new Point(RandomHelper.GenNumber(0, width), RandomHelper.GenNumber(0, height));
                    var p2 = new Point(RandomHelper.GenNumber(0, width), RandomHelper.GenNumber(0, height));

                    g.DrawLine(pen, p1, p2);
                }


                for (int i = 0; i < code.Length; i++)
                {
                    var s = code[i].ToString();

                    //文字距中
                    var format = new StringFormat(StringFormatFlags.NoClip);
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    var font = new Font(
                        oFontNames[RandomHelper.GenNumber(0, oFontNames.Length - 1)],
                        RandomHelper.GenNumber(18, 24),
                        FontStyle.Bold);

                    var brush = new SolidBrush(RandomHelper.GenColor());

                    // 字符旋转角度
                    //var angel = RandomHelper.Gen(-45, 45);

                    //g.TranslateTransform(dot.X, dot.Y);
                    //g.RotateTransform(angel);

                    // 随机字符位置
                    //var x = i * lattice + RandomHelper.GenNumber(0, 5);
                    //var y = (height - font.Height) / 2 + RandomHelper.GenNumber(-5, 5);

                    var x = i * lattice;
                    var y = (height - font.Height) / 2;
                    g.DrawString(s, font, brush, x, y);

                    //g.ResetTransform();
                    //g.RotateTransform(-angel);

                    //g.TranslateTransform(-2, -dot.Y);//移动光标到指定位置，每个字符紧凑显示，避免被软件识别

                }

                //保存图片数据
                using (var stream = new MemoryStream())
                {
                    bmp.Save(stream, ImageFormat.Png);
                    return stream.ToArray();
                }
            }
            finally
            {
                g.Dispose();
                bmp.Dispose();
            }

        }
    }
}
