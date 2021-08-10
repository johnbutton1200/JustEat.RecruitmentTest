using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace JustEat.RecruitmentTest.RestClient.Utils
{
    public class SchemaUtils
    {
        // Reusable method that validates all instances of a specified sub-object in a JToken against a JSchema
        public IList<string> GetAllJsonValidationMessagesOfSubObject(JToken jToken, JSchema jSchema, string subObject)
        {
            IList<string> allJsonValidationMessages = new List<string>();

            foreach (var child in jToken.Children())
            {
                var selectToken = child.SelectToken(subObject);

                // If schema is invalid, validation messages are added to allJsonValidationMessages
                (selectToken ?? throw new InvalidOperationException($"{subObject} is null")).IsValid(jSchema, out IList<string> messages);
                if (messages.Count == 0) continue;
                foreach (var message in messages)
                {
                    allJsonValidationMessages.Add(message);
                }
            }

            return allJsonValidationMessages;
        }
    }
}
