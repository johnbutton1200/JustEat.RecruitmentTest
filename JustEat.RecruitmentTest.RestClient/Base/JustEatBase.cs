using log4net;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using TechTalk.SpecFlow;

namespace JustEat.RecruitmentTest.RestClient.Base
{
    public class JustEatBase
    {
        public static IRestClient Client;
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public readonly JsonDeserializer Deserializer = new JsonDeserializer();
        public static IRestResponse StaticRequestResponse { get; set; }
    }
}
