using System.Linq;

namespace Common.Tool.Extend
{
    public static class ICollection_T
    {


        /// <summary>
        /// 对IQueryable`T数据进行分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页条目数</param>
        /// <returns></returns>
        public static IQueryable<T> QueryForPage<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            var page = pageIndex; //当前页
            var size = pageSize;  //每页的记录数
            int skipCount = (page - 1) * size; //前面共有几条记录

            var resultSet = skipCount == 0 ? query.Take(size) : query.Skip(skipCount).Take(size);
            return resultSet;
        }


        /// <summary>
        /// 对IQueryable`T数据进行分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="start">开始分页的条目索引</param>
        /// <param name="length">每页条目数</param>
        /// <returns></returns>
        public static IQueryable<T> QueryForStart<T>(this IQueryable<T> query, int start, int length)
        {
            var resultSet = start == 0 ? query.Take(length) : query.Skip(start).Take(length);
            return resultSet;
        }


    }
}
