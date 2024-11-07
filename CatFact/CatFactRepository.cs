using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatFact
{
    public class CatFactRepository : ICatFactRepository
    {
        private readonly HttpClient _httpClient;

        public CatFactRepository()
        {
            _httpClient = new HttpClient();
        }

        public CatFact GetRandomCatFact()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://catfact.ninja/fact");

            // Tilføj headers til anmodningen
            request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
           // request.Headers.Add("X-CSRF-TOKEN", "3vCVy2iUW6uyD0VJCe3gZkL6TGK6WUMiCCwTZVVS"); // Eksempel på CSRF-token

            // Sender anmodningen og venter på svaret
            var response = _httpClient.Send(request); // Synkront kald

            // Tjekker om svaret var succesfuldt
            if (response.IsSuccessStatusCode)
            {
                // Læs indholdet som en string synkront
                var json = response.Content.ReadAsStringAsync().Result; // Blokering indtil indholdet er tilgængeligt
               
                // Deserialiserer JSON til en CatFact objekt
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase // Indstil til at håndtere camelCase
                };

                var catFact = JsonSerializer.Deserialize<CatFact>(json, options);

                return catFact;
            }
            throw new Exception("Fejl under hentning af kattefakta.");
        }
    }
}
