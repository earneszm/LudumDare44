using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StockController : MonoBehaviour
{
    [SerializeField]
    private UIStock stockPrefab;

    [SerializeField]
    private List<StockType> stockTypes;
    public List<StockType> StockTypes { get { return stockTypes; } }

    [SerializeField]
    private RectTransform stockPanelList;
    private List<UIStock> stockListings = new List<UIStock>();

    public List<StockData> stockDataList = new List<StockData>();

    private void Start()
    {
        foreach (var stockType in stockTypes)
        {
            var newStockData = new StockData(stockType);
            stockDataList.Add(newStockData);

            var uiStockListing = Instantiate(stockPrefab, stockPanelList);
            uiStockListing.SetUpStock(newStockData);
            stockListings.Add(uiStockListing);
        }

        OrderStockList();

        UIManager.Instance.InitializeStockContainers(this, stockDataList);
    }

    public void OrderStockList()
    {
        var orderedStocks = stockListings.OrderByDescending(x => x.CurrentValue).ToList();

        for (int i = 0; i < orderedStocks.Count; i++)
        {
            orderedStocks[i].transform.SetSiblingIndex(i + 1);
        }
    }

    public void RefreshStockListUI(bool doSortList = true)
    {
        if(doSortList)
            OrderStockList();

        stockListings.ForEach(x => x.RefreshText());
    }

    public void DoStockTicks()
    {
        StockData indexFund = null;
        foreach (var stock in stockDataList)
        {
            if (stock.IsIndexFund)
                indexFund = stock;
            else
                stock.DoStockTick();
        }

        if (indexFund != null)
        {
            var count = stockDataList.Where(x => x.IsIndexFund == false).Count();
            indexFund.SetCurrentValue(stockDataList.Where(x => x.IsIndexFund == false).Sum(x => x.SharePrice) / count);
        }

        RefreshStockListUI();
    }

    public void BuyStock(StockType stockType, int quantity, bool isRequiresCash = true)
    {       
        var stockDatum = stockDataList.FirstOrDefault(x => x.StockType == stockType);

        // check that we can buy this amount
        if(isRequiresCash && stockDatum.SharePrice * quantity > GameManager.Instance.Cash)
        {
            Debug.LogError(string.Format("Attempting to buy {0} shares of: {1} at price: {2}. Not enough Current Cash: {3}", quantity, stockType.name, stockDatum.SharePrice, GameManager.Instance.Cash));
            return;
        }

        float stockValue = 0;

        if (stockDatum == null)
            Debug.LogError(string.Format("Could not find stocktype: '{0}' in current list", stockType.name));
        else
            stockValue = stockDatum.BuyShares(quantity);

        if (isRequiresCash)
            GameManager.Instance.OnCashChanged(-stockValue);

        stockListings.ForEach(x => x.RefreshText());
    }

    /// <summary>
    /// pass null to sell everything for this stock type
    /// </summary>
    /// <param name="stockType"></param>
    /// <param name="numShares"></param>
    public void SellStock(StockType stockType, int? numShares = null)
    {
        float stockValue = 0;
        var stockDatum = stockDataList.FirstOrDefault(x => x.StockType == stockType);

        if (stockDatum == null)
            Debug.LogError(string.Format("Could not find stocktype: '{0}' in current list", stockType.name));
        else if (numShares.HasValue)
            stockValue = stockDatum.SellShares(numShares.Value);
        else
            stockValue = stockDatum.SellAllShares();

        stockListings.ForEach(x => x.RefreshText());

        GameManager.Instance.OnCashChanged(stockValue);
    }

    public void SellAllStock()
    {
        float totalValue = 0;

        foreach (var datum in stockDataList)
        {
            totalValue += datum.SellAllShares();
        }

        GameManager.Instance.OnCashChanged(totalValue);
    }

}
