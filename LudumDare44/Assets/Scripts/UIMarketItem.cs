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
        this.buttonPriceModifier = buttonPriceModifier;

        nameText.text = stockData.StockType.name;
        priceText.text = stockData.SharePrice.ToString("N2");
        quantityOwnedText.text = stockData.QuantityOwned.ToString();

        var sellTextModifier = "";
        var buyTextModifier = "";

        if (buttonPriceModifier == 0)
        {
            sellTextModifier = buyTextModifier = "MAX";
        }
        else
        {
            sellTextModifier = buttonPriceModifier > 1 ? "x" + Mathf.Min(buttonPriceModifier, stockData.QuantityOwned) : "";
            buyTextModifier = buttonPriceModifier > 1 ? "x" + buttonPriceModifier : "";
        }

        sellButtonText.text = string.Format("SELL {0}", sellTextModifier);
        buyButtonText.text = string.Format("BUY {0}", buyTextModifier);

        sellButton.interactable = stockData.QuantityOwned > 0;

        // when the modifier is 0, just check if we can buy any (this is the trigger for max buy)
        buyButton.interactable = GameManager.Instance.Cash >= (stockData.SharePrice * (buttonPriceModifier > 0 ? buttonPriceModifier : 1));
        
    }

    public void OnSellButtonClick()
    {
        GameManager.Instance.OnCashChanged(stockData.SellShares(buttonPriceModifier == 0 ? stockData.QuantityOwned : buttonPriceModifier));
        marketPanel.OnMarketRowChanged();
    }

    public void OnBuyButtonClick()
    {
        GameManager.Instance.OnCashChanged(-stockData.BuyShares(buttonPriceModifier == 0 ? stockData.MaxSharesForPrice(GameManager.Instance.Cash) : buttonPriceModifier));
        marketPanel.OnMarketRowChanged();
    }
}
