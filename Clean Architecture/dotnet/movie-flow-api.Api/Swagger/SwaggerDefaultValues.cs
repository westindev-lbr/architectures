using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace movie_flow_api.Api.Swagger;

public class SwaggerDefaultValues : IOperationFilter
{
    // Méthode de l'inteface IOperationFilter
    // Permet de définir les valeurs par défaut des paramètres de la requête
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Vérifie si la description de l'API est obsolète
        var apiDescription = context.ApiDescription;
        operation.Deprecated |= apiDescription.IsDeprecated();

        // Parcours les réponses de l'opération
        foreach (var responseType in context.ApiDescription.SupportedResponseTypes)
        {
            var responseKey = responseType.IsDefaultResponse
                ? "default"
                : responseType.StatusCode.ToString();
            var response = operation.Responses[responseKey];

            foreach (var contentType in response.Content.Keys)
            {
                if (responseType.ApiResponseFormats.All(x => x.MediaType != contentType))
                {
                    response.Content.Remove(contentType);
                }
            }
        }

        // Parcours les paramètres de l'opération
        // Si le paramètre est null, on ne fait rien
        if (operation.Parameters == null)
            return;

        // Parcours les paramètres de l'opération
        foreach (var parameter in operation.Parameters)
        {
            // Récupère la description du paramètre
            var description = apiDescription.ParameterDescriptions.First(
                p => p.Name == parameter.Name
            );

            // Définit la description du paramètre
            parameter.Description ??= description.ModelMetadata?.Description;

            // Définit la valeur par défaut du paramètre
            if (
                parameter.Schema.Default == null
                && description.DefaultValue != null
                && description.DefaultValue is not DBNull
                && description.ModelMetadata is ModelMetadata modelMetadata
            )
            {
                // REF: https://github.com/Microsoft/aspnet-api-versioning/issues/429#issuecomment-605402330
                var json = JsonSerializer.Serialize(
                    description.DefaultValue,
                    modelMetadata.ModelType
                );
                parameter.Schema.Default = OpenApiAnyFactory.CreateFromJson(json);
            }

            parameter.Required |= description.IsRequired;
        }
    }
}
