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

    [SerializeField]
    private RectTransform stockList;

    private List<UIStock> allStocks = new List<UIStock>();

    private void Start()
    {
        foreach (var stockType in stockTypes)
        {
            var stock = Instantiate(stockPrefab, stockList);
            stock.SetUpStock(stockType);
            allStocks.Add(stock);
        }

        OrderStockList();
    }

    public void OrderStockList()
    {
        var orderedStocks = allStocks.OrderByDescending(x => x.CurrentValue).ToList();

        for (int i = 0; i < orderedStocks.Count; i++)
        {
            orderedStocks[i].transform.SetSiblingIndex(i + 1);
        }
    }

}
