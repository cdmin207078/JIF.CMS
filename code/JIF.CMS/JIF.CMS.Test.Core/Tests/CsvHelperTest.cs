using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIF.CMS.Core.Helpers;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class CsvHelperTest
    {
        [TestMethod]
        public void Write_List_Test()
        {
            var count = 10000;

            var names = RandomHelper.GenStringList(RandomHelper.CharSchemeEnum.Char, 10, count);
            var ages = RandomHelper.GenNumberList(1, 99, count);
            var genders = RandomHelper.GenBoolean(count);

            var records = new List<dynamic>();

            for (int i = 0; i < count; i++)
            {
                dynamic o = new ExpandoObject();
                o.name = names[i];
                o.age = ages[i];
                o.gender = genders[i];

                records.Add(o);
            }

            using (TextWriter writer = File.CreateText("c:\\TextWriter.csv"))
            {
                var csv = new CsvHelper.CsvWriter(writer);

                csv.WriteRecords(records);
            }
        }
    }
}
