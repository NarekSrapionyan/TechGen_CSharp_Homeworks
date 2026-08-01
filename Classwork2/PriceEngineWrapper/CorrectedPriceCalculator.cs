using System.Reflection;
using ACA.PriceEngine;

namespace PriceEngineWrapper;

public static class CorrectedPriceCalculator
{
    public static decimal CalculateCorrectedPayable(PriceEngine engine, PriceInput input)
    {
        var engineType = engine.GetType();

        var subtotalMethod = engineType.GetMethod("ComputeSubtotal", BindingFlags.NonPublic | BindingFlags.Instance);
        var countUnitsMethod = engineType.GetMethod("CountUnits", BindingFlags.NonPublic | BindingFlags.Instance);
        var volumeMethod = engineType.GetMethod("ApplyVolumeDiscount", BindingFlags.NonPublic | BindingFlags.Instance);
        var loyaltyMethod = engineType.GetMethod("ApplyLoyaltyDiscount", BindingFlags.NonPublic | BindingFlags.Instance);
        var couponMethod = engineType.GetMethod("ApplyCoupon", BindingFlags.NonPublic | BindingFlags.Instance);
        var vatMethod = engineType.GetMethod("ApplyVat", BindingFlags.NonPublic | BindingFlags.Instance);
        var roundMethod = engineType.GetMethod("RoundMoney", BindingFlags.NonPublic | BindingFlags.Static);

        if (subtotalMethod == null)
            throw new Exception("ComputeSubtotal method was not found");

        if (countUnitsMethod == null)
            throw new Exception("CountUnits method was not found");

        if (volumeMethod == null)
            throw new Exception("ApplyVolumeDiscount method was not found");

        if (loyaltyMethod == null)
            throw new Exception("ApplyLoyaltyDiscount method was not found");

        if (couponMethod == null)
            throw new Exception("ApplyCoupon method was not found");

        if (vatMethod == null)
            throw new Exception("ApplyVat method was not found");

        if (roundMethod == null)
            throw new Exception("RoundMoney method was not found");

        decimal subtotal = (decimal)subtotalMethod.Invoke(engine, new object[] { input.Lines })!;
        int totalunits = (int)countUnitsMethod.Invoke(engine, new object[] { input.Lines })!;

        decimal afterVolume = (decimal)volumeMethod.Invoke(engine, new object[] { subtotal, totalunits })!;
        decimal afterLoyalty = (decimal)loyaltyMethod.Invoke(engine, new object[] { afterVolume, input.LoyaltyTier })!;
        decimal afterCoupon = (decimal)couponMethod.Invoke(engine, new object[] { afterLoyalty, input.CouponAmount })!;
        decimal afterVat = (decimal)vatMethod.Invoke(engine, new object[] { afterCoupon, input.VatRate })!;

        decimal finalResult = (decimal)roundMethod.Invoke(null, new object[] { afterVat })!;

        return finalResult;
    }
}