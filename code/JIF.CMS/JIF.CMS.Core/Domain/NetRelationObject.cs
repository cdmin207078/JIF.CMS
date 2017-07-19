using JIF.CMS.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Domain
{
    /// <summary>
    /// 网状关系对象模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeRelationObject<T>
    {
        /// <summary>
        /// 当前节点
        /// </summary>
        public T Current { get; set; }

        public T Parent { get; set; }

        public List<TreeRelationObject<T>> Childs { get; set; }

        /// <summary>
        /// 是否具有父级
        /// </summary>
        public bool HasParents
        {
            get
            {
                return Parent != null;
            }
        }

        /// <summary>
        /// 是否具有子辈
        /// </summary>
        public bool HasChilds
        {
            get
            {
                return !Childs.IsNullOrEmpty();
            }
        }

    }
}
