using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.DTOs
{
    public class photoDTO
    {
        
        public int Id { get; set; }
        public string url { get; set; }
        public bool isMain  { get; set; }
    }
}