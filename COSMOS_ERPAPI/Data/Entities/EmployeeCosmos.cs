using System.Text.Json.Serialization;

namespace ERPAPI.Data.Cosmos.Entities
{
    public class EmployeeCosmos
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("employeeID")]
        public int EmployeeID { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonPropertyName("personalEmail")]
        public string PersonalEmail { get; set; }

        [JsonPropertyName("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonPropertyName("postalAddress")]
        public string PostalAddress { get; set; }

        [JsonPropertyName("gender")]
        public int Gender { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("designation")]
        public int Designation { get; set; }

        [JsonPropertyName("basicPay")]
        public decimal BasicPay { get; set; }

        [JsonPropertyName("needTransportation")]
        public bool NeedTransportation { get; set; }

        [JsonPropertyName("notes")]
        public string Notes { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}