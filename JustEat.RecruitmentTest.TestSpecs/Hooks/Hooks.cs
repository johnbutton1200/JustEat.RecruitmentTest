using JustEat.RecruitmentTest.RestClient.Base;
using RestSharp;
using TechTalk.SpecFlow;
using static JustEat.RecruitmentTest.RestClient.ResponseData.GetRestaurantsResponseData;

namespace JustEat.RecruitmentTest.TestSpecs.Hooks
{
    [Binding]
    public class Hooks : JustEatBase
    {
        private const string BaseUrl = "https://uk.api.just-eat.io";
        
        [BeforeTestRun]
        public static void InitialiseJustEatRestClient()
        {
            Client = new RestSharp.RestClient(BaseUrl);
            Log.Info("JustEat Rest Client initialised with baseUrl: " + BaseUrl);
        }

        [BeforeFeature("singleRequest")]
        public static void ExecuteValidGetRestaurantsRequest()
        {
            const string validPostcode = "BS1 4DJ";
            var request = new RestRequest(GetRestaurantsResource, Method.GET)
                .AddUrlSegment("postcode", validPostcode);
            var response = Client.Execute(request);
            Log.Info($"Executed GetRestaurants request for:Resource: \n{request.Resource}\nUrlSegment: {request.Parameters}");
            GetRestaurantsResponse = response;
        }
    }
}
