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
        public WebSiteInfoConfiguration WebSiteInfo { get; set; }

        public AttachmentConfiguration AttachmentConfig { get; set; }

        public RedisConfiguration RedisConfig { get; set; }


        public class WebSiteInfoConfiguration
        {
            public string SiteName { get; set; }

            public string Email { get; set; }

            public string GitHub { get; set; }

            public bool ShowComments { get; set; }
        }


        public class AttachmentConfiguration
        {
            public string UploadAddress { get; set; }
        }

        /// <summary>
        /// Redis缓存配置节点
        /// </summary>
        public class RedisConfiguration
        {
            /// <summary>
            /// 是否启用
            /// </summary>
            public bool Enabled { get; set; }

            /// <summary>
            /// 服务地址
            /// </summary>
            public string Server { get; set; }
        }
    }
}
