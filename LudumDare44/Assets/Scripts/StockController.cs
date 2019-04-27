using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            allStocks.Add(stock);
        }
    }

}
