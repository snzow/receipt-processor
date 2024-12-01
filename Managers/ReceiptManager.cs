using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.Features;
using ReceiptProcessor.Models;

namespace ReceiptProcessor.Managers
{
    public class ReceiptManager : IReceiptManager
    {
        //storing the data by id in memory for fast lookups
        private readonly Dictionary<string, Receipt> receiptDictionary = [];

        public string SaveReceipt(Receipt receipt)
        {
            string id = GenerateId();
            receiptDictionary.Add(id, receipt);
            return id;
        }

        public int? GetPoints(string id)
        {
            Receipt receipt = receiptDictionary[id];
            if (receipt == null) {
                return null;
            }
            return CalculatePoints(receipt);
        }

        private static int? CalculatePoints(Receipt receipt)
        {
            int points = 0;
            string nameToCompare = Regex.Replace(receipt.Retailer, "[^A-Za-z0-9-]", "");
            points += nameToCompare.Length;
            if(receipt.Total % 1 == 0)
            {
                //if the total is a round number it is ALSO a multiple of 0.25
                points += 75;
            }
            else if(receipt.Total % .25m == 0)
            {
                //if it is a multiple of 0.25 add 25
                points += 25;
            }
            //in c# integer divisions ignore the fraction so this will get the correct 5 points per 2 items, essentially a floor function without the extra step
            points += 5 * (receipt.Items.Count / 2);
            foreach (Item item in receipt.Items)
            {
                if(item.ShortDescription.Trim().Length % 3 == 0)
                {
                    //if the trimmed length of the description i s a multiple of 3, multiply by 0.2 and round up
                    int pricePoints = (int)Math.Ceiling(item.Price * 0.2m);
                    points += pricePoints;
                }
            }
            if(receipt.PurchaseDate.Day % 2 == 1)
            {
                points += 6;
            }
            if (receipt.PurchaseTime.IsBetween(new TimeOnly(14, 0), new TimeOnly(16, 0)))
            {
                points += 10;
            }
            return points;
        }

        private static string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
