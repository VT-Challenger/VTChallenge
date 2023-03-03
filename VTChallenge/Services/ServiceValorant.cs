using Newtonsoft.Json;
using System.Security.Policy;
using VTChallenge.Models;

namespace VTChallenge.Services {
    public class ServiceValorant : IServiceValorant {

        private HttpClient httpClient;
        private string url;

        public ServiceValorant(HttpClient httpClient) {
            this.httpClient = httpClient;
            this.url = "https://api.henrikdev.xyz/";
        }

        public async Task<UserApi> GetAccountAsync(string username, string tagline) {
            string request = "valorant/v1/account/" + username + "/" + tagline;
            string url = this.url + request;

            var response = await httpClient.GetAsync(url);

            string jsonReponse = await response.Content.ReadAsStringAsync();

            if (jsonReponse == null) {
                return null;
            } else {
                return JsonConvert.DeserializeObject<UserApi>(jsonReponse);
            }
        }
    }
}
