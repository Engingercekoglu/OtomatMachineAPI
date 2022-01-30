using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using OtomatMachine.Entity.Dtos;
using OtomatMachine.Entity.Entities;
using OtomatMachineAPI;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OtomatMachineIntegrationTest
{
    public class IntegrationTest
    {
       protected readonly string applicationUrl = "https://localhost:44328/";

        protected readonly HttpClient _client;
        protected IntegrationTest()
        {
            var appfactory = new WebApplicationFactory<Startup>();
            _client = appfactory.CreateClient();
        }

        protected dynamic DeserializeJsonFromStream(Stream stream)
        {
            if (stream == null || !stream.CanRead)
            {
                return default(dynamic);
            }

            using (StreamReader streamReader = new StreamReader(stream))
            {
                using (JsonTextReader jsonTextReader = new JsonTextReader((TextReader)streamReader))
                {
                    return new JsonSerializer().Deserialize<dynamic>((JsonReader)jsonTextReader);
                }
            }
        }

    }
}
