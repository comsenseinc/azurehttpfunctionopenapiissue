using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public static class Function
    {
        [FunctionName("Function1")]
        [OpenApiOperation(operationId: "Run1", tags: new[] { "function" }, Description = "Function1's description")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody("application/json", typeof(MySubmittedData), Description = "Submit data to obtain name and address", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(MySubmittedData.MyResponse), Description = "The OK response")]
        public static async Task<IActionResult> Run1(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            return new OkObjectResult(new MySubmittedData.MyResponse { Code = 1, Message = "Test response" });
        }

        [FunctionName("Function2")]
        [OpenApiOperation(operationId: "Run2", tags: new[] { "function" }, Description = "Function2's description")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody("application/json", typeof(YourSubmittedData), Description = "Submit data to obtain policy info", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(YourSubmittedData.MyResponse), Description = "The OK response")]
        public static async Task<IActionResult> Run2(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
        ILogger log)
        {
            return new OkObjectResult(new YourSubmittedData.MyResponse { Code = 1, Desccription = "Test response" });
        }
    }

    public class MySubmittedData
    {
        public string Id { get; set; }
        public string Country { get; set; }
        [OpenApiPropertyDescription("The request object for MySubmittedData class")]
        public MyRequest Request { get; set; }

        public record MyRequest
        {
            [OpenApiPropertyDescription("The person's name")]
            public string Name { get; set; }
            [OpenApiPropertyDescription("The person's address")]
            public string Address { get; set; }
        }

        public record MyResponse
        {
            public int Code { get; set; }
            public string Message { get; set; }
        }
    }

    public class YourSubmittedData
    {
        public long Number { get; set; }
        public string Criteria { get; set; }
        [OpenApiPropertyDescription("The request object for YourSubmittedData class")]
        public MyRequest2 Request { get; set; }

        public record MyRequest2
        {
            [OpenApiPropertyDescription("The person's vehicle name")]
            public string Vehicle { get; set; }
            [OpenApiPropertyDescription("The person's policy number")]
            public string PolicyNumber { get; set; }
        }

        public record MyResponse
        {
            public byte Code { get; set; }
            public string Desccription { get; set; }
        }
    }
}