using JIF.CMS.Core.Domain;
using JIF.CMS.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Extensions
{
    public static class TreeRelationObjectExtension
    {
        /// <summary>
        /// 获取对象关系关系树结构
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source">原始对象数据列表</param>
        /// <returns>只返回已排序的顶层节点, 分支使用各个节点依次访问</returns>
        public static IEnumerable<TEntity> AsTreeRelation<TEntity>(this IEnumerable<TEntity> source)
            where TEntity : TreeRelationObject
        {
            if (source.IsNullOrEmpty())
                return source;

            source = source.OrderBy(d => d.ParentId).ThenBy(d => d.OrderIndex);


            if (!source.IsNullOrEmpty())
            {
                //set current &parent
                foreach (var s in source)
                {
                    s.Parent = source.FirstOrDefault(d => d.Id == s.ParentId);
                    s.Childs = source.Where(d => d.ParentId == s.Id).OrderBy(d => d.OrderIndex).ToList();
                }
            }

            return source.Where(d => d.Parent == null).OrderBy(d => d.OrderIndex).ToList();
        }

        /// <summary>
        /// 先序遍历对象树, 获得对象树一维序列
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="soruce"></param>
        /// <returns></returns>
        public static IEnumerable<TreeRelationObjectTraverseWrapper<TEntity>> GetTreeRelationPreorder<TEntity>(this IEnumerable<TEntity> source)
            where TEntity : TreeRelationObject
        {

            if (source.IsNullOrEmpty())
                return new List<TreeRelationObjectTraverseWrapper<TEntity>>();

            // 获得关系树
            source = source.AsTreeRelation();

            var result = new List<TreeRelationObjectTraverseWrapper<TEntity>>();

            // 先序遍历对象树
            recursiveTreebyPreorderTraversal(source, result, 0);

            return result;
        }

        /// <summary>
        /// 递归 - 先序遍历对象树
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="oTree"></param>
        /// <param name="result"></param>
        /// <param name="level"></param>
        private static void recursiveTreebyPreorderTraversal<TEntity>(IEnumerable<TEntity> source, List<TreeRelationObjectTraverseWrapper<TEntity>> result, int level)
            where TEntity : TreeRelationObject
        {
            foreach (var o in source)
            {
                result.Add(new TreeRelationObjectTraverseWrapper<TEntity>
                {
                    Level = level,
                    Current = o
                });

                if (!o.Childs.IsNullOrEmpty())
                {
                    recursiveTreebyPreorderTraversal(o.Childs.Select(d => (TEntity)d).ToList(), result, level + 1);
                }
            }
        }
    }
}
