using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using JIF.CMS.Core.Helpers;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using JIF.CMS.Test.Core.Entities;
using System.Dynamic;
using System.IO;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class NpoiExcelHelperTest
    {
        private readonly string _output_directory = @"C:\Users\Administrator\Desktop\npoiTestOutput";

        [TestMethod]
        public void Test_CreateSheet_byName()
        {
            NpoiExcelHelper excel = new NpoiExcelHelper();

            excel.CreateSheet();
            excel.CreateSheet();
            excel.CreateSheet("金蛇狂舞");
            excel.CreateSheet();
            excel.CreateSheet("大鱼海棠");

            excel.Export(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + ".xls"));
        }

        [TestMethod]
        public void Test_Wirte_String_Array()
        {
            string[,] res = new string[,] {
                {"A","B","C","D","E","F"},
                {"A1","B2","C3","D4","E5","F6"},
                {"A11","B22","C33","D44","E55","F66"},
            };

            NpoiExcelHelper excel = new NpoiExcelHelper();
            excel.CreateSheet("1-1");

            excel.Write(res, 0, 0, 0);

            excel.Export(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + ".xls"));
        }

        [TestMethod]
        public void Test_Write_Number_Array()
        {
            double[,] res = new double[,] {
                {1,2,3,4,5,6,7,8},
                {1.1,2.2,3.3,4.4,5.5,6.6,7.7,8.8},
                {1.11,2.22,3.33,4.44,5.55,6.66,7.77,8.88},
            };

            NpoiExcelHelper excel = new NpoiExcelHelper();
            excel.CreateSheet("1-1");

            excel.Write(res, 0, 3, 3);

            excel.Export(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + ".xls"));
        }

        [TestMethod]
        public void Test_Write_DataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("string", typeof(string));
            dt.Columns.Add("int", typeof(int));
            dt.Columns.Add("decimal", typeof(decimal));
            dt.Columns.Add("double", typeof(double));
            dt.Columns.Add("datetime", typeof(DateTime));


            string[] colTitles = new List<string>() { "标题", "int 数值", "decimal 数值", "double 数值", "datetime 数值" }.ToArray();

            for (int i = 0; i < 30000; i++)
            {
                DataRow dr = dt.NewRow();
                dr["string"] = "NPOI向Excel文件中插入数值时，可能会出现数字当作文本的情况（即左上角有个绿色三角），这样单元格的值就无法参与运算。这是因为在SetCellValue设置单元格值的时候使用了字符串进行赋值，默认被转换成了字符型。如果需要纯数字型的，请向SetCellValue中设置数字型变量。sheet.GetRow(2).CreateCell(2).SetCellValue(123);";
                dr["int"] = i;
                dr["decimal"] = Convert.ToDecimal(i) / 2;
                dr["double"] = Convert.ToDouble(i) / 2;
                dr["datetime"] = DateTime.Now;
                dt.Rows.Add(dr);
            }

            NpoiExcelHelper excel = new NpoiExcelHelper();
            excel.CreateSheet();

            excel.Write(colTitles, 0, 0, 0);
            excel.Write(dt, 0, 1, 0);

            excel.Export(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + ".xls"));
        }

        [TestMethod]
        public void Test_Write_ListValueType()
        {
            List<string> data = new List<string>() { "A", "B", "C" };
            //List<List<string>> data = new List<List<string>>
            //{
            //    new List<string> {"A"},
            //    new List<string> {"B","C"},
            //    new List<string> {"D","E","F"}
            //};


            NpoiExcelHelper excel = new NpoiExcelHelper();
            excel.CreateSheet();

            excel.Write(data, 0, 0, 0);

            excel.Export(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + ".xls"));
        }

        [TestMethod]
        public void Test_Write_Customer_Object_List()
        {
            var data = new List<Product>();

            for (int i = 0; i < 10000; i++)
            {
                data.Add(new Product
                {
                    SysNo = i,
                    ProductId = "编号:" + i,
                    Price = Convert.ToDecimal(i) / 3,
                    CreateTime = DateTime.Now
                });
            }

            NpoiExcelHelper excel = new NpoiExcelHelper();
            excel.CreateSheet();

            /*
             * 注意 :
             * 使用 用户自定义实体类型写入Excel时,会自动根据 属性名称字母排序数据列
             */
            excel.Write(data, 0, 0, 0);

            excel.Export(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + ".xls"));
        }

        [TestMethod]
        public void Test_Write_Dynamic_List()
        {
            NpoiExcelHelper excel = new NpoiExcelHelper(true);
            excel.CreateSheet();

            excel.Write(new[] { "第一列", "第二列", "三", "肆" }, 0, 0, 0);

            var names = RandomHelper.GenString(RandomHelper.CharSchemeEnum.Chinese, 500, 1000);

            var data = new List<dynamic>();

            for (int i = 1; i < 60000; i++)
            {
                dynamic o = new ExpandoObject();

                o.A = "Hello World";
                o.aasd = DateTime.Now.ToString("yyyy-MM-dd");
                o.c223C = 1.1m;
                o.asd21D = 2.2d;

                data.Add(o);
            }


            excel.Write(data, 0, 1, 0);

            excel.Export(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + ".xlsx"));
        }

        [TestMethod]
        public void Test_Write_Anonymous_List()
        {
            var res = new List<Product>();

            for (int i = 0; i < 10000; i++)
            {
                res.Add(new Product
                {
                    SysNo = i,
                    ProductId = "编号:" + i,
                    Price = Convert.ToDecimal(i) / 3,
                    CreateTime = DateTime.Now
                });
            }

            var data = res.Select(d => new
            {
                B = d.ProductId,
                A = d.SysNo,
                c = d.Price,
            }).ToList();

            NpoiExcelHelper excel = new NpoiExcelHelper();
            excel.CreateSheet();

            excel.Write(new string[] { "作曲", "作词", "键盘" }, 0, 0, 0);
            excel.Write(data, 0, 1, 0);
            excel.Export(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + ".xls"));

        }

        [TestMethod]
        public void Test_Write_By_SheetName_Anonymous_List()
        {
            var res = new List<Product>();

            for (int i = 0; i < 10000; i++)
            {
                res.Add(new Product
                {
                    SysNo = i,
                    ProductId = "编号:" + i,
                    Price = Convert.ToDecimal(i) / 3,
                    CreateTime = DateTime.Now
                });
            }

            var data = res.Select(d => new
            {
                B = d.ProductId,
                A = d.SysNo,
                c = d.Price,
            }).ToList();

            NpoiExcelHelper excel = new NpoiExcelHelper();
            excel.CreateSheet("金蛇狂舞");
            excel.Write(data, "金蛇狂舞", 1, 1);
            excel.Export(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + ".xls"));
        }

        [TestMethod]
        public void Test_Write_SetStyle()
        {
            Assert.Fail("未进行测试");

            //List<dynamic> data = new List<dynamic>();

            //for (int i = 0; i < 10000; i++)
            //{
            //    dynamic o = new ExpandoObject();

            //    o.A = "Hello World";
            //    o.B = DateTime.Now.ToString("yyyy-MM-dd");
            //    o.C = 1.1m;
            //    o.D = 2.2d;

            //    data.Add(o);
            //}

            //NpoiExcelHelper excel = new NpoiExcelHelper();

            //excel.Write(data, 0, 0, 0);

            //ICellStyle footerCellstyle = excel.GetWorkBook().CreateCellStyle();
            //footerCellstyle.FillForegroundColor = HSSFColor.Red.Index;
            //footerCellstyle.FillPattern = FillPattern.SolidForeground;
            //excel.SetStyle(footerCellstyle, rowIndex: 5);

            //excel.Export(string.Format("{0}Test_setStyle.xls", ResultFile));

        }

        [TestMethod]
        public void Test_Write_MultiTheadWrite_List()
        {
            Parallel.For(0, 10, (i) =>
            {
                var data = new List<Product>();

                for (int j = 0; j < 100; j++)
                {
                    data.Add(new Product
                    {
                        SysNo = j,
                        ProductId = "编号:" + j,
                        Price = Convert.ToDecimal(j) / 3,
                        CreateTime = DateTime.Now
                    });
                }
                try
                {
                    NpoiExcelHelper excel = new NpoiExcelHelper();
                    excel.CreateSheet("山丘");
                    excel.Write(data, 0, 0, 0);

                    Console.WriteLine(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + "_" + i.ToString() + ".xls"));

                    excel.Export(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + "_" + i.ToString() + ".xls"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            });

        }

        [TestMethod]
        public void Test_Read_Dynamic_List()
        {
            var file = @"C:\Users\Administrator\Desktop\npoiTestOutput\Test_Write_Customer_Object_List.xls";
            var data = NpoiExcelHelper.Read(file, 0, 0, 0);

            var a = data.Select(d => new
            {
                A = d.A,
                B = d.B,
                C = d.C,
                D = d.D,
            });

            var rs = JsonConvert.SerializeObject(a);

            Console.WriteLine(rs);
        }

        [TestMethod]
        public void Test_Write_col_10_row_700()
        {
            NpoiExcelHelper excel = new NpoiExcelHelper(true);
            excel.CreateSheet("镜子");

            for (int i = 0; i < 10; i++)
            {
                excel.Write("第 " + i + " 列", 0, 0, i);
            }


            var data = new List<dynamic>();

            for (int i = 1; i < 600; i++)
            {
                dynamic o = new ExpandoObject();

                o.A1 = RandomHelper.GenNumber(0, 10000);
                o.A2 = RandomHelper.GenNumber(0, 10000);
                o.A3 = RandomHelper.GenNumber(0, 10000);
                o.A4 = RandomHelper.GenNumber(0, 10000);
                o.A5 = RandomHelper.GenNumber(0, 10000);
                o.A6 = RandomHelper.GenNumber(0, 10000);
                o.A7 = RandomHelper.GenNumber(0, 10000);
                o.A8 = RandomHelper.GenNumber(0, 10000);
                o.A9 = RandomHelper.GenNumber(0, 10000);
                o.A10 = RandomHelper.GenNumber(0, 10000);

                data.Add(o);
            }

            excel.Write(data, 0, 1, 0);

            excel.Export(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + ".xlsx"));
        }

        [TestMethod]
        public void Test_Write_col_30_row_300000()
        {
            NpoiExcelHelper excel = new NpoiExcelHelper(true);
            excel.CreateSheet();

            for (int i = 0; i < 30; i++)
            {
                excel.Write("第 " + i + " 列", 0, 0, i);
            }


            var data = new List<dynamic>();

            for (int i = 1; i < 100000; i++)
            {
                dynamic o = new ExpandoObject();

                o.A1 = RandomHelper.GenNumber(0, 10000);
                o.A2 = RandomHelper.GenNumber(0, 10000);
                o.A3 = RandomHelper.GenNumber(0, 10000);
                o.A4 = RandomHelper.GenNumber(0, 10000);
                o.A5 = RandomHelper.GenNumber(0, 10000);
                o.A6 = RandomHelper.GenNumber(0, 10000);
                o.A7 = RandomHelper.GenNumber(0, 10000);
                o.A8 = RandomHelper.GenNumber(0, 10000);
                o.A9 = RandomHelper.GenNumber(0, 10000);
                o.A10 = RandomHelper.GenNumber(0, 10000);
                o.A11 = RandomHelper.GenNumber(0, 10000);
                o.A12 = RandomHelper.GenNumber(0, 10000);
                o.A13 = RandomHelper.GenNumber(0, 10000);
                o.A14 = RandomHelper.GenNumber(0, 10000);
                o.A15 = RandomHelper.GenNumber(0, 10000);
                o.A16 = RandomHelper.GenNumber(0, 10000);
                o.A17 = RandomHelper.GenNumber(0, 10000);
                o.A18 = RandomHelper.GenNumber(0, 10000);
                o.A19 = RandomHelper.GenNumber(0, 10000);
                o.A20 = RandomHelper.GenNumber(0, 10000);
                o.A21 = RandomHelper.GenNumber(0, 10000);
                o.A22 = RandomHelper.GenNumber(0, 10000);
                o.A23 = RandomHelper.GenNumber(0, 10000);
                o.A24 = RandomHelper.GenNumber(0, 10000);
                o.A25 = RandomHelper.GenNumber(0, 10000);
                o.A26 = RandomHelper.GenNumber(0, 10000);
                o.A27 = RandomHelper.GenNumber(0, 10000);
                o.A28 = RandomHelper.GenNumber(0, 10000);
                o.A29 = RandomHelper.GenNumber(0, 10000);
                o.A30 = RandomHelper.GenNumber(0, 10000);


                data.Add(o);
            }

            excel.Write(data, 0, 1, 0);

            excel.Export(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + ".xlsx"));
        }

        [TestMethod]
        public void Test_Write_Multiple_Sheet()
        {

            NpoiExcelHelper excel = new NpoiExcelHelper(true);
            excel.CreateSheet("郭源潮");

            for (int i = 0; i < 10; i++)
            {
                excel.Write("第 " + i + " 列", 0, 0, i);
            }


            var data = new List<dynamic>();

            for (int i = 1; i < 100; i++)
            {
                dynamic o = new ExpandoObject();

                o.A1 = RandomHelper.GenNumber(0, 10000);
                o.A2 = RandomHelper.GenNumber(0, 10000);
                o.A3 = RandomHelper.GenNumber(0, 10000);
                o.A4 = RandomHelper.GenNumber(0, 10000);
                o.A5 = RandomHelper.GenNumber(0, 10000);
                o.A6 = RandomHelper.GenNumber(0, 10000);
                o.A7 = RandomHelper.GenNumber(0, 10000);
                o.A8 = RandomHelper.GenNumber(0, 10000);
                o.A9 = RandomHelper.GenNumber(0, 10000);
                o.A10 = RandomHelper.GenNumber(0, 10000);

                data.Add(o);
            }

            excel.Write(data, 0, 1, 0);


            excel.CreateSheet("空港曲");

            for (int i = 0; i < 19; i++)
            {
                excel.Write("第 " + i + " 列", 1, 0, i);
            }


            var data2 = new List<dynamic>();

            for (int i = 1; i < 50; i++)
            {
                dynamic o = new ExpandoObject();

                o.A1 = RandomHelper.GenNumber(0, 10000);
                o.A2 = RandomHelper.GenNumber(0, 10000);
                o.A3 = RandomHelper.GenNumber(0, 10000);
                o.A4 = RandomHelper.GenNumber(0, 10000);
                o.A5 = RandomHelper.GenNumber(0, 10000);
                o.A6 = RandomHelper.GenNumber(0, 10000);
                o.A7 = RandomHelper.GenNumber(0, 10000);
                o.A8 = RandomHelper.GenNumber(0, 10000);
                o.A9 = RandomHelper.GenNumber(0, 10000);
                o.A10 = RandomHelper.GenNumber(0, 10000);
                o.A11 = RandomHelper.GenNumber(0, 10000);
                o.A12 = RandomHelper.GenNumber(0, 10000);
                o.A13 = RandomHelper.GenNumber(0, 10000);
                o.A14 = RandomHelper.GenNumber(0, 10000);
                o.A15 = RandomHelper.GenNumber(0, 10000);
                o.A16 = RandomHelper.GenNumber(0, 10000);
                o.A17 = RandomHelper.GenNumber(0, 10000);
                o.A18 = RandomHelper.GenNumber(0, 10000);
                o.A19 = RandomHelper.GenNumber(0, 10000);

                data2.Add(o);
            }

            excel.Write(data2, 1, 1, 0);

            excel.Export(Path.Combine(_output_directory, MethodBase.GetCurrentMethod().Name + ".xlsx"));
        }


        [TestMethod]
        public void MyTestMethod()
        {
            var file = @"C:\Users\Administrator\Downloads\Documents\Mios开发\线下支付宝还款导入2007.xlsx";
            var data = NpoiExcelHelper.Read(file, 0, 1, 0);

            Console.WriteLine(JsonConvert.SerializeObject(data));
        }
    }
}










