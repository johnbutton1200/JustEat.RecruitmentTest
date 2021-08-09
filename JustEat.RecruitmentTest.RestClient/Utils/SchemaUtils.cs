using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JustEat.RecruitmentTest.RestClient.Utils
{
    public class SchemaUtils
    {
        public IList<string> GetAllJsonValidationMessages(JToken jToken, JSchema jSchema)
        {
            IList<string> allJsonValidationMessages = new List<string>();

            foreach (var restaurant in jToken.Children())
            {
                var address = restaurant.SelectToken("Address");
                (address ?? throw new InvalidOperationException("Address is null")).IsValid(jSchema, out IList<string> messages);
                if (messages.Count <= 0) continue;
                foreach (var message in messages)
                {
                    allJsonValidationMessages.Add(message);
                }
            }

            return allJsonValidationMessages;
        }

    }
}
