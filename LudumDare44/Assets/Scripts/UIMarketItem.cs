using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMarketItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI priceText;
    [SerializeField]
    private TextMeshProUGUI quantityOwnedText;

    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private TextMeshProUGUI buyButtonText;
    [SerializeField]
    private Button sellButton;
    [SerializeField]
    private TextMeshProUGUI sellButtonText;

    [SerializeField]
    private Image backgroundImage;  


    private UIMarketPanel marketPanel;

    public StockData stockData;

    private int buttonPriceModifier = 1;


    public void SetUpRow(UIMarketPanel marketPanel, StockData stockData)
    {
        this.marketPanel = marketPanel;
        this.stockData = stockData;
        backgroundImage.color = stockData.StockType.Color;
    }

    public void RefreshUI(int buttonPriceModifier)
    {
        if (buttonPriceModifier == 0)
            this.buttonPriceModifier = stockData.QuantityOwned;
        else
            this.buttonPriceModifier = buttonPriceModifier;

        nameText.text = stockData.StockType.name;
        priceText.text = stockData.SharePrice.ToString("N2");
        quantityOwnedText.text = stockData.QuantityOwned.ToString();

        sellButtonText.text = string.Format("SELL {0}", buttonPriceModifier > 1 ? "x" + Mathf.Min(buttonPriceModifier, stockData.QuantityOwned) : "");
        buyButtonText.text = string.Format("BUY {0}", buttonPriceModifier > 1 ? "x" + buttonPriceModifier : "");

        sellButton.interactable = stockData.QuantityOwned > 0;


        // when the modifier is 0, just check if we can buy any (this is the trigger for max buy)
        buyButton.interactable = GameManager.Instance.Cash >= (stockData.SharePrice * (buttonPriceModifier > 0 ? buttonPriceModifier : 1));
        
    }

    public void OnSellButtonClick()
    {
        GameManager.Instance.OnCashChanged(stockData.SellShares(buttonPriceModifier));
        marketPanel.OnMarketRowChanged();
    }

    public void OnBuyButtonClick()
    {
        GameManager.Instance.OnCashChanged(-stockData.BuyShares(buttonPriceModifier));
        marketPanel.OnMarketRowChanged();
    }
}
