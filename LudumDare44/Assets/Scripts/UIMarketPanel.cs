using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIMarketPanel : MonoBehaviour
{
    [SerializeField]
    private RectTransform itemList;
    [SerializeField]
    private UIMarketItem marketItemPrefab;

    private StockController sc;
    private List<UIMarketItem> marketItems = new List<UIMarketItem>();

    bool isHoldingAlt = false;
    bool isHoldingShift = false;
    bool isHoldingControl = false;
    bool isHoldingSpace = false;

    bool isHoldingAltLastFrame = false;
    bool isHoldingShiftLastFrame = false;
    bool isHoldingControlLastFrame = false;
    bool isHoldingSpaceLastFrame = false;

    public void Initialize(StockController sc, List<StockData> stockDataList)
    {
        this.sc = sc;

        foreach (var datum in stockDataList)
        {
            var item = Instantiate(marketItemPrefab, itemList);
            item.SetUpRow(this, datum);

            marketItems.Add(item);
        }
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            isHoldingAlt = Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
            isHoldingShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            isHoldingControl = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
            isHoldingSpace = Input.GetKey(KeyCode.Space);

            // only update UI if a modifier has changed since last frame
            if (isHoldingAlt != isHoldingAltLastFrame ||
                isHoldingShift != isHoldingShiftLastFrame ||
                isHoldingControl != isHoldingControlLastFrame ||
                isHoldingSpace != isHoldingSpaceLastFrame)
            {
                RefreshUI();
            }

            isHoldingAltLastFrame = isHoldingAlt;
            isHoldingShiftLastFrame = isHoldingShift;
            isHoldingControlLastFrame = isHoldingControl;
            isHoldingSpaceLastFrame = isHoldingSpace;
        }
    }

    public void Show()
    {
        SortItems();
        gameObject.SetActive(true);
    }

    public void SortItems()
    {
        var orderedStocks = marketItems.OrderByDescending(x => x.stockData.SharePrice).ToList();

        for (int i = 0; i < orderedStocks.Count; i++)
        {
            orderedStocks[i].transform.SetSiblingIndex(i);
            orderedStocks[i].RefreshUI(GetModifier());
        }
    }

    public void OnMarketRowChanged()
    {
        RefreshUI();
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
        GameManager.Instance.ToggleGamePlayActive(true);
    }

    private void RefreshUI()
    {
        marketItems.ForEach(x => x.RefreshUI(GetModifier()));
        sc.RefreshStockListUI(false);
    }

    public void OnSellAll()
    {
        GameManager.Instance.SellAllShares();

        RefreshUI();
    }

    private int GetModifier()
    {
        if (isHoldingSpace)
            return 0;

        if (isHoldingControl)
            return 50;

        if (isHoldingAlt)
            return 20;

        if (isHoldingShift)
            return 5;

        return 1;
    }
}
