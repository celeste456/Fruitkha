using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IFormDataBuilder
    {

        MultipartFormDataContent BuildFormData<T>(T model, IFormFile image);

    }
}
