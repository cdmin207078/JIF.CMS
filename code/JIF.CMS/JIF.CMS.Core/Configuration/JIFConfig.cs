﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JIF.CMS.Core.Configuration
{
    public class JIFConfig
    {
        public WebSiteInfoConfig WebSiteInfo { get; set; }

        public AttachmentConfig AttachmentConfig { get; set; }
    }

    public class WebSiteInfoConfig
    {
        public string SiteName { get; set; }

        public string Email { get; set; }

        public string GitHub { get; set; }

        public bool ShowComments { get; set; }
    }


    public class AttachmentConfig
    {
        public string UploadAddress { get; set; }
    }
}
