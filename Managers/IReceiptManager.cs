using ReceiptProcessor.Models;

namespace ReceiptProcessor.Managers
{
    public interface IReceiptManager
    {
        /**
         * <summary>
         * <c>SaveReceipt</c> saves a receipt to memory
         * </summary> <returns>a string representing a UUID</returns>
         */
        public string SaveReceipt(Models.Receipt receipt);
        /**
         * <summary>
         *  <c>GetPoints</c> gets the point value of a receipt
         * </summary> 
         * <returns> the number of points the receipt earned, or null if the receipt doesn't exist</returns>
         */
        public int? GetPoints(string id);
    }
}
