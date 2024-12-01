using System.ComponentModel;
using System.Text.Json.Serialization;
using ReceiptProcessor.Converters;

namespace ReceiptProcessor.Models
{
    public class Receipt
    {
        [JsonIgnore]
        public string? Id { get; set; }
        public required string Retailer { get; set; }
        //automatically convert the string format into a DateOnly using a converter I implemented.
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public required DateOnly PurchaseDate { get; set; }

        //automatically convert the string into a TimeOnly using a converter I implemented.
        [JsonConverter(typeof(TimeOnlyJsonConverter))]
        public required TimeOnly PurchaseTime { get; set; }
        public required IList<Item> Items { get; set; }
        //using Decimal instead of float/double etc. because we are dealing with money which should be exact.
        public required Decimal Total { get; set; }

        [JsonIgnore]
        public int Points { get; set; }

    }
}
