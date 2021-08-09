using JustEat.RecruitmentTest.RestClient.Base;
using RestSharp;
using RestSharp.Validation;

namespace JustEat.RecruitmentTest.RestClient.Requests
{
    public class GetRestaurantsRequests : JustEatBase
    {
        private const string Resource = "/restaurants/bypostcode/{postcode}";

        public IRestResponse GetRestaurantsByPostcode(string postcode)
        {
            var request = new RestRequest(Resource, Method.GET)
                .AddUrlSegment("postcode", postcode);
            var response = Client.Execute(request);
            Log.Info($"Executed GetRestaurants request for: {request.Resource}");
            return response;
        }
    }
}
