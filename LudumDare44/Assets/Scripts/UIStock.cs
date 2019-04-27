using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIStock : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI priceText;
    [SerializeField]
    private TextMeshProUGUI quantityOwnedText;

    private Image backgroundImage;
    private StockData stockData;

    public float CurrentValue { get { return stockData.SharePrice; } }

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }

    public void SetUpStock(StockData stockData)
    {
        this.stockData = stockData;
        backgroundImage.color = stockData.StockType.Color;
        SetStartingValue();        
    }

    private void SetStartingValue()
    {
        nameText.text = stockData.StockType.name;        
        RefreshText();
    }

    public void RefreshText()
    {
        priceText.text = stockData.SharePrice.ToString("N2");
        quantityOwnedText.text = stockData.QuantityOwned.ToString();
    }

    public void DoStockTick()
    {
        RefreshText();
    }    
}
