using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using PMS.Web.Helper;
using Microsoft.Extensions.Configuration;
using Serilog;
using Newtonsoft.Json;

namespace PMS.Web.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ErrorHandlerMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(UnauthorizedAccessException uEx)
            {
                JsonError jsonError = new CustomException(ErrorCodes.GE005.ToString(), _configuration).GetErrorObject();
                Log.Logger.Error("----------####################### UnAuthorized Exception Block #############################------------");
                Log.Logger.Error("Exception: {@uEx} and " + Environment.NewLine + "custom error: {@jsonError}", uEx, jsonError);
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.ContentType = Constants.JsonContentType;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(jsonError)).ConfigureAwait(false);
            }
            catch (CustomException cEx)
            {
                JsonError jsonError = cEx.GetErrorObject();
                Log.Logger.Error("----------####################### Custom Exception Block #############################------------");
                Log.Logger.Error("Exception: {@cEx} and " + Environment.NewLine + "custom error: {@jsonError}", cEx, jsonError);
                context.Response.StatusCode = Convert.ToInt32(jsonError.Error.Code);
                context.Response.ContentType = Constants.JsonContentType;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(jsonError)).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                JsonError jsonError = new CustomException(ErrorCodes.Default.ToString(), _configuration).GetErrorObject();
                Log.Logger.Error("----------####################### Unhandled Exception Block #############################------------");
                Log.Logger.Error("Exception: {@ex} and " + Environment.NewLine + "custom error: {@jsonError}", ex, jsonError);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = Constants.JsonContentType;
                await context.Response.WriteAsync(JsonConvert.SerializeObject(jsonError)).ConfigureAwait(false);
            }                      
        }
    }
}
