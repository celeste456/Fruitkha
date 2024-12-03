using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;
using FrontEnd.Helpers.Interfaces;

namespace FrontEnd.Helpers.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        public HttpClient Client { get; set; }

        public IFormDataBuilder _formDataBuilder;

        public ServiceRepository(HttpClient _client, IFormDataBuilder formDataBuilder, IConfiguration configuration)
        {
            Client = _client;
            _formDataBuilder = formDataBuilder;
            Client.BaseAddress = new Uri(configuration.GetValue<string>("BackEnd:Url") ?? "");
        }
        public HttpResponseMessage GetResponse(string url)
        {
            return Client.GetAsync(url).Result;
        }
        public HttpResponseMessage PutResponse(string url, object model)
        {
            return Client.PutAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage PostResponse(string url, object model)
        {
            return Client.PostAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage DeleteResponse(string url)
        {
            return Client.DeleteAsync(url).Result;
        }

        public HttpResponseMessage PostResponse<T>(string url, T model, IFormFile image)
        {
            var formData = _formDataBuilder.BuildFormData(model, image);
            return Client.PostAsync(url, formData).Result;
        }

        public HttpResponseMessage PutResponse<T>(string url, T model, IFormFile image)
        {
            var formData = _formDataBuilder.BuildFormData(model, image);
            return Client.PutAsync(url, formData).Result;
        }
    }
}
