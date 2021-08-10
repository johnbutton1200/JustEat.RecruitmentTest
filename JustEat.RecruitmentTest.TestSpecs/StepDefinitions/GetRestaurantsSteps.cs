using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JustEat.RecruitmentTest.RestClient.Base;
using JustEat.RecruitmentTest.RestClient.Requests;
using JustEat.RecruitmentTest.RestClient.ResponseModels;
using JustEat.RecruitmentTest.RestClient.Utils;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using static JustEat.RecruitmentTest.RestClient.ResponseData.GetRestaurantsResponseData;

namespace JustEat.RecruitmentTest.TestSpecs.StepDefinitions
{
    [Binding]
    public class GetRestaurantsSteps : JustEatBase
    {
        private readonly GetRestaurantsRequests _getRestaurantsRequests;
        private readonly ScenarioContext _scenarioContext;
        private readonly SchemaUtils _schemaUtils;

        public GetRestaurantsSteps(GetRestaurantsRequests getRestaurantsRequests, ScenarioContext scenarioContext, SchemaUtils schemaUtils)
        {
            _getRestaurantsRequests = getRestaurantsRequests;
            _scenarioContext = scenarioContext;
            _schemaUtils = schemaUtils;
        }

        [Given(@"I have a restaurants API")]
        public void GivenIHaveARestaurantsApi()
        {
            // Do nothing
        }
        
        [When(@"I request the restaurants by postcode '(.*)'")]
        public void WhenIRequestTheRestaurantsByPostcode(string postcode)
        {
            _scenarioContext["response"] = _getRestaurantsRequests.GetRestaurantsByPostcode(postcode);
        }

        [Then(@"the response status code is '(.*)'")]
        public void ThenTheResponseStatusCodeIs(HttpStatusCode expectedStatusCode)
        {
            var statusCode = _scenarioContext.Get<IRestResponse>("response").StatusCode;
            Assert.That(statusCode, Is.EqualTo(expectedStatusCode), "Status code is " + expectedStatusCode);
        }

        // This is an alternative to the above step scoped to "singleRequest" feature tag only,
        // where the response is obtained from the request performed in the [BeforeFeature] hook
        [Then(@"the response status code is '(.*)'"), Scope(Tag = "singleRequest")]
        public void StaticThenTheResponseStatusCodeIs(HttpStatusCode expectedStatusCode)
        {
            var response = GetRestaurantsResponse;
            _scenarioContext["response"] = response;
            Assert.That(response.StatusCode, Is.EqualTo(expectedStatusCode), $"Status code should be {expectedStatusCode}");
        }

        [Then(@"the Address schema should be correct for each restaurant")]
        public void ThenTheAddressSchemaShouldBeCorrectForEachRestaurant()
        {
            // Generates a JSchema from the provided schema
            var expectedJsonSchema = JSchema.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\AddressJsonSchema.json")));
            
            // Generate a JObject from the request response content
            var responseContentJObject = JObject.Parse(_scenarioContext.Get<IRestResponse>("response").Content);
            
            // Generate a JToken from the Restaurants sub-object
            var restaurantsJToken = responseContentJObject.GetValue("Restaurants");

            // Passes previously generated variables into helper method that validates each restaurant's address against the schema
            var allJsonValidationMessages = _schemaUtils.GetAllJsonValidationMessagesOfSubObject(restaurantsJToken, expectedJsonSchema, "Address");
            
            Assert.That(allJsonValidationMessages.Count, Is.EqualTo(0), "No validation messages should be present:\n" + string.Join("\n", allJsonValidationMessages));
        }

        [Then(@"the DeliveryEtaMinutes schema should be correct for each restaurant")]
        public void ThenTheDeliveryEtaMinutesSchemaShouldBeCorrectForEachRestaurant()
        {
            // Generates a JSchema from the provided schema
            var expectedJsonSchema = JSchema.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\DeliveryEtaMinutesJsonSchema.json")));

            // Generate a JObject from the request response content
            var responseContentJObject = JObject.Parse(_scenarioContext.Get<IRestResponse>("response").Content);

            // Generate a JToken from the Restaurants sub-object
            var restaurantsJToken = responseContentJObject.GetValue("Restaurants");

            // Passes previously generated variables into helper method that validates each restaurant's address against the schema
            var allJsonValidationMessages = _schemaUtils.GetAllJsonValidationMessagesOfSubObject(restaurantsJToken, expectedJsonSchema, "DeliveryEtaMinutes");

            Assert.That(allJsonValidationMessages.Count, Is.EqualTo(0), "No validation messages should be present:\n" + string.Join("\n", allJsonValidationMessages));
        }

        [Then(@"the DeliveryEtaMinutes schema should deliberately fail")]
        public void ThenTheDeliveryEtaMinutesSchemaShouldDeliberatelyFail()
        {
            // Generates a JSchema from the provided schema
            var expectedJsonSchema = JSchema.Parse(File.ReadAllText(Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\IncorrectDeliveryEtaMinutesJsonSchema.json")));

            // Generate a JObject from the request response content
            var responseContentJObject = JObject.Parse(_scenarioContext.Get<IRestResponse>("response").Content);

            // Generate a JToken from the Restaurants sub-object
            var restaurantsJToken = responseContentJObject.GetValue("Restaurants");

            // Passes previously generated variables into helper method that validates each restaurant's address against the schema
            var allJsonValidationMessages = _schemaUtils.GetAllJsonValidationMessagesOfSubObject(restaurantsJToken, expectedJsonSchema, "DeliveryEtaMinutes");

            Assert.That(allJsonValidationMessages.Count, Is.EqualTo(0), "No validation messages should be present:\n" + string.Join("\n", allJsonValidationMessages));
        }
        
        [Then(@"all restaurants with more than 1 rating should have a star rating greater than 0")]
        public void ThenAllRestaurantsWithMoreThanRatingShouldHaveAStarRatingGreaterThan()
        {
            var responseContent = Deserializer.Deserialize<GetRestaurantsResponseModel>(_scenarioContext.Get<IRestResponse>("response"));

            foreach (var restaurant in responseContent.Restaurants.Where(restaurant => restaurant.NumberOfRatings > 1))
            {
                Assert.That(restaurant.Rating.StarRating, Is.GreaterThan(0), "StarRating greater than 0");
            }
        }

        [Then(@"all the restaurants with no ratings should have a star rating of 0")]
        public void ThenAllTheRestaurantsWithNoRatingsShouldHaveAStarRatingOf()
        {
            var response = _scenarioContext.Get<IRestResponse>("response");
            var responseContent = Deserializer.Deserialize<GetRestaurantsResponseModel>(response);
            
            foreach (var restaurant in responseContent.Restaurants.Where(restaurant => restaurant.NumberOfRatings == 0))
            {
                Assert.That(restaurant.Rating.StarRating, Is.EqualTo(0), "StarRating is 0");
            }
        }

        [Then(@"the first restaurant returned will have a valid URL")]
        public void ThenTheFirstRestaurantReturnedWillHaveAValidUrl()
        {
            var response = _scenarioContext.Get<IRestResponse>("response");
            var responseContent = Deserializer.Deserialize<GetRestaurantsResponseModel>(response);
            var restaurantUrl = responseContent.Restaurants[0].Url;

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(restaurantUrl));
            var restaurantUrlResponse = Task.Run(() => httpClient.SendAsync(request)).Result;
            Assert.That(restaurantUrlResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                "Status code of restaurant URL request is: OK");
        }
        
        [Given(@"I have performed a valid Get Restaurants request")]
        public void GivenIHavePerformedAValidGetRestaurantsRequest()
        {
            // Do nothing because request is performed once in [BeforeFeature] hook
        }

        [Then(@"all Address field values are valid for each restaurant")]
        public void ThenAllAddressFieldValuesAreValidForEachRestaurant()
        {
            var response = _scenarioContext.Get<IRestResponse>("response");
            var responseContent = Deserializer.Deserialize<GetRestaurantsResponseModel>(response).Restaurants;
            Assert.IsNotNull(responseContent);
            Assert.Multiple(() =>
            {
                foreach (var address in responseContent.Select(restaurant => restaurant.Address))
                {
                    Assert.That(address.City, Is.Not.Null.Or.Empty, "City not null or empty");
                    Assert.That(address.FirstLine, Is.Not.Null.Or.Empty, "FirstLine not null or empty");
                    Assert.That(address.Postcode, Is.Not.Null.Or.Empty, "Postcode not null or empty");
                    Assert.That(address.Longitude <= 180 || address.Longitude >= 180, "Longitude is valid");
                    Assert.That(address.Latitude <= 180 || address.Latitude >= 180, "Latitude is valid");
                }
            });
        }
    }
}
