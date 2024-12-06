using FrontEnd.APIModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class SecurityHelper : ISecurityHelper
    {
        IServiceRepository ServiceRepository;

        public SecurityHelper(IServiceRepository serviceRepository)
        {
            ServiceRepository = serviceRepository;
        }

        public LoginAPI Login(UserViewModel user)
        {
            try
            {
                HttpResponseMessage response = ServiceRepository
                                                    .PostResponse("/api/Auth/login", new { user.UserName, user.Password });
                var content = response.Content.ReadAsStringAsync().Result;
                LoginAPI loginAPI = JsonConvert.DeserializeObject<LoginAPI>(content);

                return loginAPI;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Register(UserViewModel model)
        {
            try
            {
                HttpResponseMessage response = ServiceRepository
                    .PostResponse("/api/Auth/register", new
                    {
                        Username = model.UserName,
                        Password = model.Password,
                        Email = model.Email
                    });

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var errorResponse = JsonConvert.DeserializeObject<dynamic>(content);

                    throw new Exception(errorResponse.message.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during registration: {ex.Message}");
            }
        }


    }
}
