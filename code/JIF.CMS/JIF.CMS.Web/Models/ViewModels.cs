using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JIF.CMS.Web.Models
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            Articles = new List<Article>();
        }

        public List<Article> Articles { get; set; }
    }
}