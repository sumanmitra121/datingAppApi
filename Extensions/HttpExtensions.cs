using System.Text.Json;
using TestApi.Helpers;

namespace TestApi.Extensions
{
    public static class HttpExtensions
    {
         public static void AddPaginationheader(this HttpResponse response,
         int currentPage,
         int itemsperpage,
         int totalItems,
         int toalPages){
                var paginationaHeader = new paginations(currentPage,itemsperpage,totalItems,toalPages);
                response.Headers.Add("Pagination",JsonSerializer.Serialize(paginationaHeader));
                response.Headers.Add("Access-Control-Expose-Headers","Pagination");
                

         }
    }
}