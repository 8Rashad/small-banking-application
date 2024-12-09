using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BankApplication.Languages
{
    public class AddAcceptLanguageHeader : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
                {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Description = "Language preference for localization ('en' for English, 'fr' for French, 'es' for Spanish)",
                Required = false,
                Schema = new OpenApiSchema { 
                    Type = "string",
                    Enum = new List<IOpenApiAny>
                {
                    new OpenApiString("en-US"), 
                    new OpenApiString("fr-FR"), 
                    new OpenApiString("es-ES")  
                }
                }
            });
        }
    }
}
