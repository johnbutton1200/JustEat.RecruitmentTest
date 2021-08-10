using log4net;
using RestSharp;
using RestSharp.Serialization.Json;

namespace JustEat.RecruitmentTest.RestClient.Base
{
    public class JustEatBase
    {
        protected static IRestClient Client;
        protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly JsonDeserializer Deserializer = new JsonDeserializer();
        protected const string BaseUrl = "https://uk.api.just-eat.io";
        protected const string GetRestaurantsResource = "/restaurants/bypostcode/{postcode}";
    }
}
