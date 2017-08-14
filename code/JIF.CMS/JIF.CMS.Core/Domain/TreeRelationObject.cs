using JIF.CMS.Core.Extensions;
using System.Collections.Generic;

namespace JIF.CMS.Core.Domain
{
    /// <summary>
    /// 树关系对象基类
    /// </summary>
    public abstract class TreeRelationObject : BaseEntity
    {
        /// <summary>
        /// 分类排序
        /// </summary>
        public virtual int OrderIndex { get; set; }

        /// <summary>
        /// 所属父级分类编号
        /// </summary>
        public virtual int ParentId { get; set; }

        public virtual TreeRelationObject Parent { get; set; }

        public virtual IEnumerable<TreeRelationObject> Childs { get; set; }

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
