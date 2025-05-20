using Microsoft.OpenApi.Models;
using QLDangKyHocPhan.DTOs.excel;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var fileParams = context.MethodInfo.GetParameters()
            .Where(p => p.ParameterType == typeof(UploadRequest))
            .ToList();

        if (!fileParams.Any())
            return;

        operation.RequestBody = new OpenApiRequestBody
        {
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties = new Dictionary<string, OpenApiSchema>
                        {
                            ["ImportFile"] = new OpenApiSchema
                            {
                                Type = "string",
                                Format = "binary",
                                Description = "File to upload (e.g., .xlsx)"
                            },
                            ["HasHeader"] = new OpenApiSchema
                            {
                                Type = "boolean",
                                Description = "Indicates if the file has a header row"
                            }
                        },
                        Required = new HashSet<string> { "ImportFile" }
                    }
                }
            },
            Description = "Upload a file for processing",
            Required = false // Không bắt buộc request body
        };
    }
}