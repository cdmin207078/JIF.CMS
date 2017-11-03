﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.HttpApiResults
{
    public class APIResult
    {
        public int Code { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }
    }

    public class APIResult<T>
    {
        public int Code { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
