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

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class NpoiExcelHelperTest
    {
        public readonly string _output_directory = @"C:\Users\Administrator\Desktop\npoiTestOutput";

        [TestMethod]
        public void Test_Wirte_StringArray()
        {
            string[,] res = new string[,] {
                {"A","B","C","D","E","F"},
                {"A1","B2","C3","D4","E5","F6"},
                {"A11","B22","C33","D44","E55","F66"},
            };

            NpoiExcelHelper excel = new NpoiExcelHelper();
            excel.CreateSheet("1-1");

            excel.Write(res, 0, 0, 0);

            excel.Export(Path.Combine(_output_directory, "Test_writeStringArray.xls"));
        }

        [TestMethod]
        public void Test_Write_NumberArray()
        {
            double[,] res = new double[,] {
                {1,2,3,4,5,6,7,8},
                {1.1,2.2,3.3,4.4,5.5,6.6,7.7,8.8},
                {1.11,2.22,3.33,4.44,5.55,6.66,7.77,8.88},
            };

            NpoiExcelHelper excel = new NpoiExcelHelper();
            excel.CreateSheet("1-1");

            excel.Write(res, 0, 3, 3);

            excel.Export(Path.Combine(_output_directory, "Test_writeNumberArray.xls"));
        }

        [TestMethod]
        public void Test_Write_DataTable()
        {
            Assert.Fail("未进行测试");

            //DataTable dt = new DataTable();
            //dt.Columns.Add("string", typeof(string));
            //dt.Columns.Add("int", typeof(int));
            //dt.Columns.Add("decimal", typeof(decimal));
            //dt.Columns.Add("double", typeof(double));
            //dt.Columns.Add("datetime", typeof(DateTime));


            //string[] colTitles = new List<string>() { "标题", "int 数值", "decimal 数值", "double 数值", "datetime 数值" }.ToArray();

            //for (int i = 0; i < 30000; i++)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr["string"] = "NPOI向Excel文件中插入数值时，可能会出现数字当作文本的情况（即左上角有个绿色三角），这样单元格的值就无法参与运算。这是因为在SetCellValue设置单元格值的时候使用了字符串进行赋值，默认被转换成了字符型。如果需要纯数字型的，请向SetCellValue中设置数字型变量。sheet.GetRow(2).CreateCell(2).SetCellValue(123);";
            //    dr["int"] = i;
            //    dr["decimal"] = Convert.ToDecimal(i) / 2;
            //    dr["double"] = Convert.ToDouble(i) / 2;
            //    dr["datetime"] = DateTime.Now;
            //    dt.Rows.Add(dr);
            //}

            //NpoiExcelHelper excel = new NpoiExcelHelper();

            //excel.Write(colTitles);
            //excel.Write(dt, rowIndex: 1);

            //excel.Export(string.Format("{0}Test_writeDataTable.xls", ResultFile));
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

            excel.Write(data, 0, 0, 0);

            excel.Export(Path.Combine(_output_directory, "Test_writeListValueType.xls"));
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

            /*
             * 注意 :
             * 使用 用户自定义实体类型写入Excel时,会自动根据 属性名称字母排序数据列
             */
            excel.Write(data, 0, 0, 0);

            excel.Export(Path.Combine(_output_directory, "Test_writeCustomerTypeListObject.xls"));
        }

        [TestMethod]
        public void Test_Write_DynamicList()
        {
            NpoiExcelHelper excel = new NpoiExcelHelper(true);
            excel.Write(new[] { "第一列", "第二列", "三", "肆", "E", "F" }, 0, 0, 0);

            var names = RandomHelper.Gen(RandomHelper.Format.Chinese, 500, 1000);

            var data = new List<dynamic>();

            for (int i = 1; i < 60000; i++)
            {
                dynamic o = new ExpandoObject();

                o.A = "Hello World";
                o.aasd = DateTime.Now.ToString("yyyy-MM-dd");
                o.c223C = 1.1m;
                o.asd21D = 2.2d;
                o.asdE = names[RandomHelper.Gen(1, 1000)];
                o.F_q = names[RandomHelper.Gen(1, 1000)];

                data.Add(o);
            }

            excel.Write(data, 0, 1, 0);

            excel.Export(Path.Combine(_output_directory, "Test_wirteDynamicList.xlsx"));
        }

        [TestMethod]
        public void Test_Write_AnonymousList()
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
            excel.Write(data, 0, 0, 0);
            excel.Export(Path.Combine(_output_directory, "Test_writeAnonymousList.xls"));

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
        public void Test_Write_MultiTheadWriteList()
        {
            Assert.Fail("未进行测试");

            //Parallel.For(0, 1000, (i) =>
            //{
            //    var data = new List<Product>();

            //    for (int j = 0; j < 10000; j++)
            //    {
            //        data.Add(new Product
            //        {
            //            SysNo = j,
            //            ProductId = "编号:" + j,
            //            Price = Convert.ToDecimal(j) / 3,
            //            CreateTime = DateTime.Now
            //        });
            //    }
            //    try
            //    {
            //        NpoiExcelHelper excel = new NpoiExcelHelper();

            //        excel.Write(data);

            //        excel.Export(string.Format("{0}Test_writeCustomerTypeListObject" + i + "_.xls", ResultFile));
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //        //throw;
            //    }
            //});

        }

        [TestMethod]
        public void Test_Read_List_Dynamic()
        {
            var file = @"C:\Users\Administrator\Desktop\npoiTestOutput\Test_wirteDynamicList.xlsx";
            var data = NpoiExcelHelper.Read(file, 1, 0, 0);

            var a = data.Select(d => new
            {
                A = d.A,
                B = d.B,
                C = d.C,
                D = d.D,
                E = d.E,
                F = d.F
            });

            var rs = JsonConvert.SerializeObject(a);

            Console.WriteLine(rs);
        }
    }
}
