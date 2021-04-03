using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolutional.Project.Admin.Filter
{
    public class SwaggerOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            context.ApiDescription.TryGetMethodInfo(out var methodInfo);
            var authorizeAttributes = methodInfo.CustomAttributes.OfType<IAuthorizeData>();

            if (!authorizeAttributes.Any()) return;

            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            if (!operation.Parameters.Any(x =>
                x.Name.Equals("Authorization", StringComparison.CurrentCultureIgnoreCase) &&
                x.In.Value == ParameterLocation.Header))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "access token",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "String",
                        Default = new OpenApiString("Bearer ")
                    }
                });
            }
        }
    }
}
