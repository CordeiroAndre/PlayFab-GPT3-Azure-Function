using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using PlayFab;

namespace Company.Function
{
    public static class GenerateRandomHistory
    {
        [FunctionName("GenerateRandomHistory")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            PlayFabAuthenticationContext playFabAuthenticationContext = new PlayFabAuthenticationContext();

            string token = data?.TitleAuthenticationContext?.EntityToken;
            if (string.IsNullOrEmpty(token)) return new BadRequestObjectResult("The server couldn't get the token");
            playFabAuthenticationContext.EntityToken = token;

            if (playFabAuthenticationContext.IsEntityLoggedIn() == false) return new BadRequestObjectResult("User is not authenticated");


            /*Generate random history*/

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.openai.com/v1/completions"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer MY_GPT3_SECRET_KEY");

                    request.Content = new StringContent("{\n  \"model\": \"text-davinci-002\",\n  \"prompt\": \"Generate, in one phrease, a fictional history of a button that needs to be pressed multiple times.n\",\n  \"temperature\": 1,\n  \"max_tokens\": 100,\n  \"top_p\": 1,\n  \"frequency_penalty\": 0,\n  \"presence_penalty\": 0\n}");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                    var response = await httpClient.SendAsync(request);

                    dynamic formattedResponse = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());


                    return new OkObjectResult(formattedResponse.choices[0].text);
                }
            }
        }
    }
}
