using System.Collections.Generic;
using Newtonsoft.Json;

namespace JustEat.RecruitmentTest.RestClient.ResponseModels
{
    public class GetRestaurantsSchema
    {
        [JsonProperty("Restaurants")]
        public List<Restaurant> Restaurants { get; set; }

        public class Address
        {
            [JsonProperty("City")]
            public string City { get; set; }

            [JsonProperty("FirstLine")]
            public string FirstLine { get; set; }

            [JsonProperty("Postcode")]
            public string Postcode { get; set; }

            [JsonProperty("Latitude")]
            public decimal Latitude { get; set; }

            [JsonProperty("Longitude")]
            public decimal Longitude { get; set; }
        }

        public class Rating
        {
            [JsonProperty("Count")]
            public int Count { get; set; }

            [JsonProperty("Average")]
            public double Average { get; set; }

            [JsonProperty("StarRating")]
            public double StarRating { get; set; }
        }

        public class Restaurant
        {
            [JsonProperty("Id")]
            public int Id { get; set; }

            [JsonProperty("Name")]
            public string Name { get; set; }

            [JsonProperty("UniqueName")]
            public string UniqueName { get; set; }

            [JsonProperty("Address")]
            public Address Address { get; set; }

            [JsonProperty("City")]
            public string City { get; set; }

            [JsonProperty("Postcode")]
            public string Postcode { get; set; }

            [JsonProperty("Latitude")]
            public double Latitude { get; set; }

            [JsonProperty("Longitude")]
            public double Longitude { get; set; }

            [JsonProperty("Rating")]
            public Rating Rating { get; set; }

            [JsonProperty("RatingStars")]
            public double RatingStars { get; set; }

            [JsonProperty("NumberOfRatings")]
            public int NumberOfRatings { get; set; }

            [JsonProperty("RatingAverage")]
            public double RatingAverage { get; set; }

            [JsonProperty("Description")]
            public string Description { get; set; }

            [JsonProperty("Url")]
            public string Url { get; set; }
        }
    }
}
