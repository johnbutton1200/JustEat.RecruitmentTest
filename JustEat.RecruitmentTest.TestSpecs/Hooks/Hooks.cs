using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoDi;
using RestSharp;
using TechTalk.SpecFlow;
using JustEat.RecruitmentTest.RestClient.Base;

namespace JustEat.RecruitmentTest.TestSpecs.Hooks
{
    [Binding]
    public class Hooks : JustEatBase
    {
        //private readonly IObjectContainer objectContainer;
        private const string BaseUrl = "https://uk.api.just-eat.io";
        
        public Hooks(IObjectContainer objectContainer)
        {
            //this.objectContainer = objectContainer;
        }

        [BeforeTestRun]
        public static void InitialiseJustEatRestClient()
        {
            Client = new RestSharp.RestClient(BaseUrl);
            Log.Info("JustEat Rest Client initialised with baseUrl: " + BaseUrl);
        }

        [BeforeFeature("singleRequest")]
        public static void ExecuteValidGetRestaurantsRequest()
        {
            const string resource = "/restaurants/bypostcode/{postcode}";

            var request = new RestRequest(resource, Method.GET)
                .AddUrlSegment("postcode", "BS5 7JW");
            var response = Client.Execute(request);
            Log.Info($"Executed GetRestaurants request for:Resource: \n{request.Resource}\nUrlSegment: {request.Parameters}");
            StaticRequestResponse = response;
        }
    }
}
