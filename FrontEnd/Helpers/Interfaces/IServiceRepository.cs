namespace FrontEnd.Helpers.Interfaces
{
    public interface IServiceRepository
    {
        HttpClient Client { get; set; }
        HttpResponseMessage GetResponse(string url);
        HttpResponseMessage PutResponse(string url, object model);
        HttpResponseMessage PostResponse(string url, object model);
        HttpResponseMessage DeleteResponse(string url);

        //manejo de imagenes 
        HttpResponseMessage PostResponse<T>(string url, T model, IFormFile image);
        HttpResponseMessage PutResponse<T>(string url, T model, IFormFile image);
    }
}
