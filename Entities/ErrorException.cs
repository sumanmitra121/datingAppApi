using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi.Entities
{
    public class ErrorException
    {
        public int statusCode { get; set; }
        public string msg { get; set; }
        public string details { get; set; }

        public ErrorException(int statusCode ,string msg=null,string details=null)
        {  
            this.statusCode = statusCode;
            this.msg = msg;
            this.details = details;
        }
    }
}