using Microsoft.AspNetCore.Mvc;
using myte.Models;


namespace myte.Services
{
    public class LoginService  
    {

        private HttpClient _httpClient;

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("http://localhost:5273");
        }



        public async Task<Login> AddLoginAsync(Login login)
        {
            var apiResposta = await _httpClient.PostAsJsonAsync($"/api/Account/Login/Login", login);
            apiResposta.EnsureSuccessStatusCode();
            return await apiResposta.Content.ReadFromJsonAsync<Login>();

        }







    }
}
