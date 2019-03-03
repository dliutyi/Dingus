using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Dingus.Services
{
    public enum HttpMethod { Get, Post };
    public class NetServices
    {
        private HttpClient _httpClient;

        public NetServices(int timeout = 100, int maxBufferSize = 2000000000)
        {
            _httpClient = new HttpClient()
            {
                Timeout = TimeSpan.FromSeconds(timeout),
                MaxResponseContentBufferSize = maxBufferSize
            };
        }

        public async Task<T> GetDeserializedObject<T>(string uri, object data) => await GetDeserializedObject<T>(uri, HttpMethod.Get, data); 
        public async Task<T> GetDeserializedObject<T>(string uri, JsonSerializerSettings settings) => await GetDeserializedObject<T>(uri, HttpMethod.Get, null, settings);

        public async Task<T> GetDeserializedObject<T>(string uri, HttpMethod method = HttpMethod.Get, object data = null, JsonSerializerSettings settings = null)
        {
            string readResponse = await GetRawObject(uri, method, data);
            return JsonConvert.DeserializeObject<T>(readResponse, settings);
        }

        public async Task<string> GetRawObject(string uri, HttpMethod method = HttpMethod.Get, object data = null)
        {
            HttpResponseMessage response = await GetResponse(uri, method, data);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            throw new HttpRequestException();
        }

        public async Task<HttpResponseMessage> GetResponse(string uri, HttpMethod method = HttpMethod.Get, object data = null)
        {
            try
            {
                switch (method)
                {
                    case HttpMethod.Get:
                        return await _httpClient.GetAsync(uri);
                    case HttpMethod.Post:
                        StringContent content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                        return await _httpClient.PostAsync(uri, content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
