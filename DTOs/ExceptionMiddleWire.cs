
using System.Net;
using System.Text.Json;
using TestApi.Entities;

namespace TestApi.DTOs
{
    public class ExceptionMiddleWire
    {
        private RequestDelegate _next;
        private ILogger<ExceptionMiddleWire> _logger;
        private IHostEnvironment Env;
        public ExceptionMiddleWire(RequestDelegate _next,ILogger<ExceptionMiddleWire> _logger,IHostEnvironment env)
        {
             Env = env;
             this._logger = _logger;
             this._next = _next;
        }

        public async Task InvokeAsync(HttpContext _context){
            try{
                await _next(_context);
            }
            catch(Exception ex){
                     _logger.LogError(ex,ex.Message);
                     _context.Response.ContentType = "applicaton/json";
                     _context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                     var _response = Env.IsDevelopment()
                       ? new ErrorException(_context.Response.StatusCode,ex.Message,ex.StackTrace?.ToString()) 
                       : new ErrorException(_context.Response.StatusCode,"Internel Server Error");

                       var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                       var json = JsonSerializer.Serialize(_response,options);
                       await _context.Response.WriteAsync(json);
            }
        }
    }
}