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



    /// <summary>
    /// 树关系对象基类
    /// </summary>
    public abstract class TreeRelationObject : BaseEntity
    {
        /// <summary>
        /// 分类排序
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// 所属父级分类编号
        /// </summary>
        public int ParentId { get; set; }

        public virtual TreeRelationObject Parent { get; set; }

        public virtual List<TreeRelationObject> Childs { get; set; }

        /// <summary>
        /// 是否具有父级
        /// </summary>
        public virtual bool HasParents
        {
            get
            {
                return Parent != null;
            }
        }

        /// <summary>
        /// 是否具有子辈
        /// </summary>
        public virtual bool HasChilds
        {
            get
            {
                return !Childs.IsNullOrEmpty();
            }
        }

    }

    /// <summary>
    /// 树关系对象遍历包裹对象
    /// </summary>
    public class TreeRelationObjectTraverseWrapper<TEntity>
        where TEntity : TreeRelationObject
    {
        /// <summary>
        /// 所属层级
        /// </summary>
        public int Level { get; set; }


        public TEntity Current { get; set; }
    }
}
