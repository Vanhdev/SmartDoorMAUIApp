using MauiApp1.Data;
using MauiApp1.Pages.Authentication;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    internal interface IAuthService
    {
        Task<TokenDTO> Login(LoginAccountForm loginAccount);
    }

    internal class AuthService : IAuthService
    {
        private string _baseUrl = Global.Global.baseUrl;
        public AuthService() { }
        public async Task<TokenDTO> Login(LoginAccountForm loginAccount)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseUrl}/api/Auth/login";
                    var serializeContent = JsonConvert.SerializeObject(loginAccount);
                    Console.WriteLine(serializeContent);
                    var stringContent = new StringContent(serializeContent, Encoding.UTF8, "application/json");
                    var apiResponse = await client.PostAsync(url, stringContent);

                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string body = await apiResponse.Content.ReadAsStringAsync();

                        TokenDTO token = JsonConvert.DeserializeObject<TokenDTO>(body);

                        return token;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return null;
            }
        }
    }
}
