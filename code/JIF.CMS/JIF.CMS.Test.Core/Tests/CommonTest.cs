using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using JIF.CMS.Core.Extensions;
using System.Collections.Generic;
using JIF.CMS.Test.Core.Entities;
using System.Collections;
using JIF.CMS.Core.Helpers;
using System.Text;
using Newtonsoft.Json;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Text.RegularExpressions;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void Exception_Performance_Test()
        {
            var count = 1000;

            Stopwatch watch = new Stopwatch();
            watch.Start();


            for (int i = 0; i < count; i++)
            {
                try
                {
                    var a = 1;
                    var b = 0;
                    var c = a / b;
                }
                catch { }
            }

            watch.Show("抛出异常");


            for (int i = 0; i < count; i++)
            {
                try
                {
                    try
                    {
                        var a = 1;
                        var b = 0;
                        var c = a / b;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch { }

            }

            watch.Show("抛出嵌套异常");


            for (int i = 0; i < count; i++)
            {
                try
                {
                    try
                    {
                        try
                        {
                            var a = 1;
                            var b = 0;
                            var c = a / b;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    catch (Exception ex) { throw ex; }
                }
                catch { }
            }

            watch.Show("三个套异常");


            for (int i = 0; i < count; i++)
            {
                var a = 1;
                var b = 1;
                var c = a / b;
            }
            watch.Show("无异常");


            for (int i = 0; i < count; i++)
            {
                try
                {
                    var a = 1;
                    var b = 1;
                    var c = a / b;
                }
                catch { }
            }
            watch.Show("无异常, 无 try-cache");
        }

        [TestMethod]
        public void Proerty_Is_Key_Words_Test()
        {

            var stu = new Student
            {
                @class = "5666"
            };

            Console.WriteLine(JsonConvert.SerializeObject(stu));
        }

        class Student
        {
            public string @class { get; set; }
        }


        [TestMethod]
        public void Json_To_DataTable()
        {
            var famliies = new List<Famliy>
            {
                new Famliy {
                    Id = 1,
                    Father = new Person { Name = "李雷", Relationship="父亲" },
                    Mother = new Person { Name = "韩梅梅", Relationship="母亲" },
                },
                new Famliy {
                    Id = 2,
                    Father = new Person { Name = "司马相如", Relationship="父亲" },
                    Mother = new Person { Name = "卓文君", Relationship="母亲" },
                },
            };

            var json = JsonConvert.SerializeObject(famliies);

            Console.WriteLine(json);

            var data = JsonConvert.DeserializeObject(json);

            //var dt = (DataTable)JsonConvert.DeserializeObject(json, typeof(DataTable));
            //var dt = Tabulate(json);
            var dt = JsonStringToDataTable(json);

        }


        public static DataTable Tabulate(string json)
        {
            var jsonLinq = JObject.Parse(json);

            // Find the first array using Linq
            var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
            var trgArray = new JArray();
            foreach (JObject row in srcArray.Children<JObject>())
            {
                var cleanRow = new JObject();
                foreach (JProperty column in row.Properties())
                {
                    // Only include JValue types
                    if (column.Value is JValue)
                    {
                        cleanRow.Add(column.Name, column.Value);
                    }
                }

                trgArray.Add(cleanRow);
            }

            return JsonConvert.DeserializeObject<DataTable>(trgArray.ToString());
        }


        public static DataTable JsonStringToDataTable(string jsonString)
        {
            DataTable dt = new DataTable();
            string[] jsonStringArray = System.Text.RegularExpressions.Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
            List<string> ColumnsName = new List<string>();
            foreach (string jSA in jsonStringArray)
            {
                string[] jsonStringData = System.Text.RegularExpressions.Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                foreach (string ColumnsNameData in jsonStringData)
                {
                    try
                    {
                        int idx = ColumnsNameData.IndexOf(":");
                        string ColumnsNameString = ColumnsNameData.Substring(0, idx - 1).Replace("\"", "");
                        if (!ColumnsName.Contains(ColumnsNameString))
                        {
                            ColumnsName.Add(ColumnsNameString);
                        }
                    }
                    catch
                    {
                        throw new Exception(string.Format("Error Parsing Column Name : {0}", ColumnsNameData));
                    }
                }
                break;
            }
            foreach (string AddColumnName in ColumnsName)
            {
                dt.Columns.Add(AddColumnName);
            }
            foreach (string jSA in jsonStringArray)
            {
                string[] RowData = System.Text.RegularExpressions.Regex.Split(jSA.Replace("{", "").Replace("}", ""), ",");
                DataRow nr = dt.NewRow();
                foreach (string rowData in RowData)
                {
                    try
                    {
                        int idx = rowData.IndexOf(":");
                        string RowColumns = rowData.Substring(0, idx - 1).Replace("\"", "");
                        string RowDataString = rowData.Substring(idx + 1).Replace("\"", "");
                        nr[RowColumns] = RowDataString;
                    }
                    catch
                    {
                        continue;
                    }
                }
                dt.Rows.Add(nr);
            }
            return dt;
        }


        public class Famliy
        {
            public int Id { get; set; }

            public Person Father { get; set; }

            public Person Mother { get; set; }
        }

        public class Person
        {
            public string Relationship { get; set; }

            public string Name { get; set; }
        }


        [TestMethod]
        public void MyTestMethodxxx()
        {
            var o = new
            {
                a = 1,
                b = "2",
                c = DateTime.Now,
                d = new string[] { "A", "B", "C" }
            };

            Console.WriteLine(JsonConvert.SerializeObject(o));

            var p = "hello world";
            var sp = JsonConvert.SerializeObject(p);
            Console.WriteLine(sp);

            Console.WriteLine(JsonConvert.DeserializeObject<string>(sp));

        }
    }
}
