
namespace ReceiptProcessor.Models
{
    public class Item
    {
        public required string ShortDescription { get; set; }
        //using Decimal instead of float/double etc. because we are dealing with money which should be exact.
        public required Decimal Price { get; set; }
    }
}
