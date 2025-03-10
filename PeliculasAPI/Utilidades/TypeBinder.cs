using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace PeliculasAPI.Utilidades
{
    public class TypeBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nombrePropiedad = bindingContext.ModelName;
            var valor = bindingContext.ValueProvider.GetValue(nombrePropiedad);

            if(valor == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }
            try
            {
                var tipoDestino = bindingContext.ModelMetadata.ModelType;
                var valorDeserializado = JsonSerializer.Deserialize(valor.FirstValue!,
                    tipoDestino, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                bindingContext.Result = ModelBindingResult.Success(valorDeserializado);
            }
            catch (Exception)
            {
                bindingContext.ModelState.TryAddModelError(nombrePropiedad,
                    "Valor con formato incorrecto");
                
            }
            return Task.CompletedTask;
        }
    }
}
