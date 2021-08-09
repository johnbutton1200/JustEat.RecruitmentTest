using JustEat.RecruitmentTest.RestClient.Base;
using RestSharp;

namespace JustEat.RecruitmentTest.RestClient.Requests
{
    public class GetRestaurantsRequests : JustEatBase
    {
        public IRestResponse GetRestaurantsByPostcode(string postcode)
        {
            var request = new RestRequest(GetRestaurantsResource, Method.GET)
                .AddUrlSegment("postcode", postcode);
            var response = Client.Execute(request);
            Log.Info($"Executed GetRestaurants request for: {request.Resource}\n" +
                     $"With parameters: {string.Join("\n", request.Parameters)}");
            return response;
        }
    }
}
