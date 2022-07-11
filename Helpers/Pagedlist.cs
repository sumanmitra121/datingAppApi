using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TestApi.Helpers
{
    public class Pagedlist<T> : List<T>
    {
        public Pagedlist(IEnumerable<T> items,int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count/ (double) pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<Pagedlist<T>> createAsync(IQueryable<T> src, int pageNumber,int pageSize){

            var count= await src.CountAsync();
            var items = await src.Skip((pageNumber - 1) *  pageSize).Take(pageSize).ToListAsync();
            return new Pagedlist<T>(items,count,pageNumber,pageSize);
        }
 
    }
}