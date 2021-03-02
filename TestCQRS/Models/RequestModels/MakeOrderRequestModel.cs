using System.Text.Json.Serialization;
using MediatR;
using TestCQRS.Models.ResponseModels;

namespace TestCQRS.Models
{
    public class MakeOrderRequestModel : IRequest<OrderResponseModel>
    {
        [JsonPropertyName("orderName")]
        public string OrderName { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("amount")]
        public double Amount { get; set; }
    }
}
