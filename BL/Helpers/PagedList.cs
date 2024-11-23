using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Helpers
{
    public class PagedList<T>:List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;


        public PagedList(List<T> data, int count,int pageNumber, int pagesize)
        {
            TotalCount = count;
            PageSize = pagesize;
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count/(double) pagesize);
            AddRange(data);
        }

      public static PagedList<T> ToPagedList(IQueryable<T> Source , int pageNumber , int pageSize , string? sortPram = null , bool descending = false)
        {
            var count = Source.Count();
            if (!string.IsNullOrEmpty(sortPram)) 
            {
                var propertyInfo = typeof(T).GetProperty(sortPram);

                if (propertyInfo != null)
                {
                    throw new ArgumentException($"Property {sortPram} not found in type {typeof(T).Name}");
                }
                var parameter = Expression.Parameter(typeof(T) , "p");
                var property = Expression.Property(parameter, propertyInfo);

                var conversion = Expression.Convert(parameter, typeof(object));

                var lambda = Expression.Lambda<Func<T, object>>(conversion, parameter);

                Source = descending? Source.OrderByDescending(lambda) : Source.OrderBy(lambda);

            }

            var items = Source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items,count,pageNumber, pageSize);
        }

    }
}
