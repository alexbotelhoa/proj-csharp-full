using System;
using System.IO;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

using Template.Core.DTOs;
using Template.Core.Helpers;
using Template.Core.Exceptions;
using Template.Integration.Options;
using Template.Integration.Integrations.Interfaces;

namespace Template.Integration.Integrations
{
    public class UserIntegration : IUserIntegration
    {
        private readonly ComunicacaoHttp _comunicacaoHttp;

        public UserIntegration(ComunicacaoHttp comunicacaoHttp)
        {
            _comunicacaoHttp = comunicacaoHttp;
        }

        private static IConfiguration GetConfiguration()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration;
        }

        public async Task<dynamic> CreateCall(string method, HeaderRequestHelper header, string endpoint, object query, object body)
        {
            object result = null;
            string fullUrl = null;
            HttpResponseMessage response = null;

            try
            {
                fullUrl = _comunicacaoHttp.Configuracoes[0].Url + endpoint;
                var json = JsonConvert.SerializeObject(body);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("ContentType", "application/json");
                httpClient.DefaultRequestHeaders.Add("ChannelId", header.channelId);
                httpClient.DefaultRequestHeaders.Add("Aplication", header.aplication);
                httpClient.DefaultRequestHeaders.Add("api-key", GetConfiguration().GetSection("TemplateApiKey").Value);

                var users = await GetUsersFromDatabaseAsync();

                switch (method)
                {
                    case "GET":
                        return JsonConvert.SerializeObject(users); // TODO
                        
                        //response = await httpClient.GetAsync(fullUrl);
                        //break;
                    case "POST":
                        //GetUserResponseDTO user = (GetUserResponseDTO)users.FirstOrDefault(u => u.Id == query.userId);
                        return JsonConvert.SerializeObject(users);

                        //response = await httpClient.PostAsync(fullUrl, data);
                        //break;
                    case "PUT": 
                        response = await httpClient.PutAsync(fullUrl, data); 
                        break;
                    case "DELETE": 
                        response = await httpClient.DeleteAsync(fullUrl); 
                        break;
                }

                if (!response.IsSuccessStatusCode && (int)response.StatusCode != 204)
                {
                    result = response.Content.ReadAsStringAsync();
                    dynamic errorMessage = JsonConvert.DeserializeAnonymousType(Convert.ToString(result), new { message = "Error" });
                    throw new Exception(errorMessage);
                }

                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw new HttpException(response.StatusCode, new HttpExceptionHelper
                {
                    Service = "Chamada ServiceIntegration",
                    Url = fullUrl,
                    Header = header,
                    Request = "Query: "+ JsonConvert.SerializeObject(query) + " // Body: " + JsonConvert.SerializeObject(body),
                    Result = result,
                    Response = response
                }, ex.Message);
            }
        }

        private Task<IEnumerable<dynamic>> GetUsersFromDatabaseAsync()
        {
            var users = new List<dynamic>
            {
                new UserDTO
                {
                    Id = 1,
                    Name = "Usuário 1",
                    Email = "usuario1@example.com"
                },
                new UserDTO
                {
                    Id = 2,
                    Name = "Usuário 2",
                    Email = "usuario2@example.com"
                },
                new UserDTO
                {
                    Id = 3,
                    Name = "Usuário 3",
                    Email = "usuario3@example.com"
                }
            };

            //var users = new List<dynamic>();

            return Task.FromResult<IEnumerable<dynamic>>(users);
        }
    }
}
