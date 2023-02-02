﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TaskRESTAPI
{
    public class FileUploadOperatingFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.RequestBody == null 
                || !operation.RequestBody.Content.Any(x=>x.Key.Equals("multipart/form-data", StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }
            operation.Parameters.Clear();

            if (context.ApiDescription.ParameterDescriptions[0].Type ==typeof(IFormFile)
                || context.ApiDescription.ParameterDescriptions[0].Type == typeof(List<IFormFile>))
            {
                var uploadedFileMediaType = new OpenApiMediaType()
                {
                    Schema = new OpenApiSchema()
                    {
                        Type = "object",
                        Properties =
                        {
                            ["files"] = new OpenApiSchema()
                            {
                                Type ="array",
                                Items = new OpenApiSchema
                                {
                                    Type = "file",
                                    Format = "binary"
                                }
                            }
                        },
                        Required = new HashSet<string> { "files"}
                    }
                };
                operation.RequestBody = new OpenApiRequestBody()
                {
                    Content = {["multipart/form-data"] =uploadedFileMediaType}
                };
            }
        }
    }
}
