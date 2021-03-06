﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core
{
    //[Serializable]
    [DataContract]
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex < 1 ? 1 : pageIndex;

            this.AddRange(source.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList());
        }

        public PagedList(IList<T> source, int pageIndex, int pageSize)
        {
            TotalCount = source.Count();
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex < 1 ? 1 : pageIndex;
            this.AddRange(source.Skip((PageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        [DataMember]
        public int PageIndex { get; set; }

        [DataMember]
        public int PageSize { get; set; }
        //public int IndividualPagesDisplayedCount { get; private set; }

        [DataMember]
        public int TotalCount { get; set; }

        [DataMember]
        public int TotalPages { get; set; }

        [DataMember]
        public bool HasPreviousPage
        {
            get { return (PageIndex > 1); }
        }

        [DataMember]
        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }

        //public int FirstIndividualPageIndex
        //{
        //    get
        //    {
        //        if ((this.TotalPages < this.IndividualPagesDisplayedCount) ||
        //          ((this.PageIndex - (this.IndividualPagesDisplayedCount / 2)) < 0))
        //        {
        //            return 1;
        //        }
        //        if ((this.PageIndex + (this.IndividualPagesDisplayedCount / 2)) >= this.TotalPages)
        //        {
        //            return (this.TotalPages - this.IndividualPagesDisplayedCount);
        //        }
        //        return (this.PageIndex - (this.IndividualPagesDisplayedCount / 2));
        //    }
        //}

        //public int LastIndividualPageIndex
        //{
        //    get
        //    {
        //        int num = this.IndividualPagesDisplayedCount / 2;
        //        if ((this.IndividualPagesDisplayedCount % 2) == 0)
        //        {
        //            num--;
        //        }
        //        if ((this.TotalPages < this.IndividualPagesDisplayedCount) ||
        //            ((this.PageIndex + num) >= this.TotalPages))
        //        {
        //            return (this.TotalPages - 1);
        //        }
        //        if ((this.PageIndex - (this.IndividualPagesDisplayedCount / 2)) < 0)
        //        {
        //            return (this.IndividualPagesDisplayedCount - 1);
        //        }
        //        return (this.PageIndex + num);
        //    }
        //}
    }
}
