using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;

namespace FrontEnd.Helpers.Implementations
{
    public class FormDataBuilder : IFormDataBuilder
    {
        public MultipartFormDataContent BuildFormData<T>(T model, IFormFile image)
        {
            var formData = new MultipartFormDataContent();

            // Procesar las propiedades del modelo
            foreach (var property in typeof(T).GetProperties())
            {
                var value = property.GetValue(model);
                if (value != null)
                {
                    formData.Add(new StringContent(value.ToString()), property.Name);
                }
            }

            // Procesar la imagen
            if (image != null)
            {
                var streamContent = new StreamContent(image.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(image.ContentType);
                formData.Add(streamContent, "photo", image.FileName);
            }

            return formData;
        }
    }
}
