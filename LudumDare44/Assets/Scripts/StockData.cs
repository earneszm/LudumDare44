using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockData
{
    public StockType StockType;

    public bool IsIndexFund { get { return StockType.IndexFund; } }

    private float sharePrice;
    public float SharePrice { get { return sharePrice; } }

    private int quantityOwned;
    public int QuantityOwned { get { return quantityOwned; } }
    public float OwnedValue { get { return QuantityOwned * SharePrice; } }

    private VolatilityDetail volatility;

    public StockData(StockType stockType)
    {
        this.StockType = stockType;
        volatility = GameUtils.GetVolatility(stockType.Riskyness);
        sharePrice = Random.Range(stockType.MinStartingValue, stockType.MaxStartingValue);
    }

    public void DoStockTick()
    {
        var changeTick = GameUtils.GetStockChangePercent(volatility);
        var previousAmount = OwnedValue;

        sharePrice = sharePrice * (1 + (changeTick.Item2 / 100));

        // if it crashed send alert
        if (changeTick.Item1 == true)
        {
            var lostAmount = previousAmount - OwnedValue;
            AudioManager.Instance.Crash();
            UIManager.Instance.CreateAlert(string.Format("{0} just crashed by {1}%! {2}", StockType.name, (-changeTick.Item2).ToString("N0"), lostAmount > 0 ? "You lost $" + lostAmount.ToString("N2") : ""), StockType.Color);
        }
    }

   public void SetCurrentValue(float value)
   {
       sharePrice = value;
   }

    public float BuyShares(int number)
    {
        quantityOwned += number;
        return SharePrice * number;
    }

    public float SellShares(int numShares)
    {
        if (numShares > QuantityOwned)
            numShares = QuantityOwned;

        quantityOwned -= numShares;

        return numShares * SharePrice;
    }

    public float SellAllShares()
    {
        var returnValue = OwnedValue;
        quantityOwned = 0;

        return returnValue;
    }

    public int MaxSharesForPrice(float amount)
    {
        return Mathf.FloorToInt(amount / SharePrice);
    }
}
