using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Data;
using Newtonsoft.Json;

namespace MauiApp1.Services
{
    internal interface IUserService
    {
        Task<int> Insert(User uiPageTypeModel);
        Task<List<User>> Get();
        Task<User> Get(string id);
        Task<bool> Update(string ID, User uiPageTypeModel);
        Task<bool> Delete(string id);
    }
    internal class UserService : IUserService
    {
        private string _baseUrl = Global.Global.baseUrl;
        private TokenService tokenService;
        public UserService(TokenService tokenService) {
            this.tokenService = tokenService;
        }

        public async Task<bool> Delete(string id)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseUrl}/api/User/{id}";
                    var apiResponse = await client.DeleteAsync(url);

                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return false;
        }
        public async Task<List<User>> Get()
        {
            var returnResponse = new List<User>();
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseUrl}/api/User/";
                    var apiResponse = await client.GetAsync(url);

                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var response = await apiResponse.Content.ReadAsStringAsync();
                        returnResponse = JsonConvert.DeserializeObject<List<User>>(response);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return returnResponse;
        }
        public async Task<User> Get(string ID)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseUrl}/api/User/{ID}";
                    var apiResponse = await client.GetAsync(url);

                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var response = await apiResponse.Content.ReadAsStringAsync();
                        var deserilizeResponse = JsonConvert.DeserializeObject<User>(response);

                        return deserilizeResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return null;

        }
        public async Task<int> Insert(User uiPageTypeModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseUrl}/api/User";

                    var serializeContent = JsonConvert.SerializeObject(uiPageTypeModel);
                    Console.WriteLine(serializeContent);
                    var stringContent = new StringContent(serializeContent, Encoding.UTF8, "application/json");
                    var apiResponse = await client.PostAsync(url, stringContent);

                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return 0;
            }
            return 0;
        }
        public async Task<bool> Update(string ID, User uiPageTypeModel)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseUrl}/api/User/{ID}";

                    var serializeContent = JsonConvert.SerializeObject(uiPageTypeModel);

                    var apiResponse = await client.PutAsync(url, new StringContent(serializeContent, Encoding.UTF8, "application/json"));

                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return false;
        }

        public async Task<List<Data.Device>> GetDevices(string name, string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    string url = $"{_baseUrl}/api/User/get-devices";
                    UriBuilder uri = new UriBuilder(url);
                    uri.Query = $"name={name}";
                    url = uri.ToString();

                    var apiResponse = await client.GetAsync(url);

                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var response = await apiResponse.Content.ReadAsStringAsync();
                        var deserilizeResponse = JsonConvert.DeserializeObject<List<Data.Device>>(response);

                        return deserilizeResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return null;

        }

        public async Task<Data.Device> GetDevice(string name, string token, string deviceName)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    string url = $"{_baseUrl}/api/User/get-device";
                    UriBuilder uri = new UriBuilder(url);
                    uri.Query = $"name={name}&deviceName={deviceName}";
                    url = uri.ToString();

                    var apiResponse = await client.GetAsync(url);

                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var response = await apiResponse.Content.ReadAsStringAsync();
                        var deserilizeResponse = JsonConvert.DeserializeObject<Data.Device>(response);

                        return deserilizeResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return null;

        }
    }
}
