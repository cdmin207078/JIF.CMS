using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JIF.CMS.Test.Core.Tests
{
    [TestClass]
    public class PerentChildDataTest
    {

        private List<BookCategory> categories = new List<BookCategory>
        {
            new BookCategory{ Id = 1, Name = "Boss"},

            new BookCategory{ Id = 2, Name = "CEO"},

            new BookCategory{ Id = 3, Name = "CTO"},
            new BookCategory{ Id = 4, Name = "Development Manager"},
            new BookCategory{ Id = 5, Name = "Developer"},

            new BookCategory{ Id = 6, Name = "CHO"},
            new BookCategory{ Id = 7, Name = "HR"}

        };

        private List<CategoryRelation> relations = new List<CategoryRelation>
        {
            new CategoryRelation{ Id = 1, CId = 2, PId = 1 },
            new CategoryRelation{ Id = 2, CId = 3, PId = 1 },
            new CategoryRelation{ Id = 3, CId = 6, PId = 1 },


            new CategoryRelation{ Id = 4, CId = 4, PId = 3 },
            new CategoryRelation{ Id = 5, CId = 5, PId = 4 },

            new CategoryRelation{ Id = 6, CId = 7, PId = 6 },
            //new CategoryRelation{ Id = 7, CId = 4, PId = 3 },
            //new CategoryRelation{ Id = 8, CId = 4, PId = 3 },
            //new CategoryRelation{ Id = 9, CId = 4, PId = 3 },

        };

        [TestMethod]
        public void Get_Childs_Recursive_Test()
        {
            var id = 1;

            // first level
            var rels = new List<List<int>>();

            rels.Add(relations.Where(d => d.PId.Equals(id)).Select(d => d.CId).ToList());

            // if have childs
            if (rels.Any())
            {
                var hasNextLevel = true;

                while (hasNextLevel)
                {
                    var subs = relations.Where(d => rels.Last().Contains(d.PId)).Select(d => d.CId).ToList();

                    if (subs.Any())
                        rels.Add(subs);
                    else
                        hasNextLevel = false;
                }
            }

            // build result
            var childs = new List<BookCategory>();

            if (rels.Any())
            {
                var flat = new List<int>();

                // flat rels
                rels.ForEach(d => flat.AddRange(d));

                childs = categories.Where(d => flat.Contains(d.Id)).ToList();
            }


            Console.WriteLine(JsonConvert.SerializeObject(rels));
            Console.WriteLine(JsonConvert.SerializeObject(childs));
        }


        [TestMethod]
        public void Get_Childs_Quick_Test()
        {

        }


        [TestMethod]
        public void Get_Parent_Test()
        {

        }

    }



    class BookCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    class CategoryRelation
    {
        public int Id { get; set; }

        public int OrderIndex { get; set; }

        public int CId { get; set; }

        public int PId { get; set; }
    }
}
