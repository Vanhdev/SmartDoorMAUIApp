using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using MauiApp1.Data;


namespace MauiApp1.Services
{
    public interface ITokenService
    {
        Task<TokenDTO> GetToken();
        Task RemoveToken();
        Task SetToken(TokenDTO tokenDTO);
    }
    public class TokenService
    {
        private readonly ILocalStorageService localStorageService;

        public TokenService(ILocalStorageService localStorageService)
        {
            this.localStorageService = localStorageService;
        }

        public async Task SetToken(TokenDTO tokenDTO)
        {
            await localStorageService.SetItemAsync("token", tokenDTO.AuthToken);
        }

        public async Task<string> GetToken()
        {
            return await localStorageService.GetItemAsync<string>("token");
        }

        public async Task RemoveToken()
        {
            await localStorageService.RemoveItemAsync("token");
        }
    }
}
