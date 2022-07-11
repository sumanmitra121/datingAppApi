using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Helpers
{
    public class userParams
    {
        private const int MaxPageSize = 50;
        public int pageNumber { get; set; } = 1;
        public int _pageSize=10;

        public int pageSize
        {
             get => _pageSize;
             set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        
        public string CurrentUsername { get; set; }
        public string gender { get; set; }

         public int minAge { get; set; } = 18;
        public int maxAge { get; set; } = 150;

         public string orderBy { get; set; } = "lastActive";

    }
}