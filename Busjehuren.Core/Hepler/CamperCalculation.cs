using Busjehuren.Core.EF;
using Busjehuren.Core.Models;

namespace Busjehuren.Core.Hepler
{
   public static class CamperCalculation
    {
       public static decimal CalculatePackagePrice(int dayCount, int personCount, PakketModel pakket)
       {
           decimal price = pakket.Prijs * pakket.Valuta.Koers * pakket.Number;
           if (pakket.PakketType == 10) price *= dayCount;
           if (pakket.IsPerPersoon.Value) price *= personCount;
           else if (pakket.MilePackageAmount.HasValue && pakket.MilePackageAmount.Value > 1) price *= pakket.MilePackageAmount.Value;
           return price;
       }

       public static decimal CalculateLocalPackagePrice(int dayCount, int personCount, PakketModel pakket)
       {
           decimal price = pakket.Prijs * pakket.Number;
           if (pakket.PakketType == 10) price *= dayCount;
           if (pakket.IsPerPersoon.Value) price *= personCount;
           else if (pakket.MilePackageAmount.HasValue && pakket.MilePackageAmount.Value > 1) price *= pakket.MilePackageAmount.Value;
           return price;
       }
    }
}
