using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace ClientApp.ApiService
{
    class ServerApi
    {
        private const string apiURL = "https://localhost:44373/api/Home";
        private readonly RestClient client = new RestClient(apiURL);
       
        public List<Card> GetCards ()
        {
            var request = new RestRequest(Method.GET);                  
            var response = client.Get(request);                 
        
            return JsonConvert.DeserializeObject<List<Card>>(response.Content);
        }

        public void CreateCard (Card card)
        {
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(JsonConvert.SerializeObject(card));

            client.Execute(request);
        }

        public void UpdateCard(Card card, int id)
        {
            var request = new RestRequest(apiURL + "/" + id, Method.PUT);
           
            request.AddJsonBody(JsonConvert.SerializeObject(card));
            client.Execute(request);
        }

        public void DeleteCard(int id)
        {
            var request = new RestRequest(apiURL + "/" + id, Method.DELETE);

            client.Execute(request);
        }
    }
}
