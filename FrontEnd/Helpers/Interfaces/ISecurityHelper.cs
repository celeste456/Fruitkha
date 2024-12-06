using FrontEnd.APIModels;
using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface ISecurityHelper
    {
        LoginAPI Login(UserViewModel user);
        bool Register(UserViewModel user);

    }
}
