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
    private Image backgroundImage;
    private StockType stockType;

    private float currentValue;
    public float CurrentValue { get { return currentValue; } }
    public bool IsIndexFund { get { return stockType.IndexFund; } }

    private VolatilityDetail volatility;

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }

    public void SetUpStock(StockType stockType)
    {
        this.stockType = stockType;
        volatility = GameUtils.GetVolatility(stockType.Riskyness);
        backgroundImage.color = stockType.Color;
        SetStartingValue();        
    }

    private void SetStartingValue()
    {
        nameText.text = stockType.name;
        currentValue = Random.Range(stockType.MinStartingValue, stockType.MaxStartingValue);
        RefreshPriceText();
    }

    private void RefreshPriceText()
    {
        priceText.text = currentValue.ToString("N2");
    }

    public void DoStockTick()
    {
        currentValue = currentValue * (1 + (GameUtils.GetStockChangePercent(volatility) / 100));
        RefreshPriceText();
    }

    public void SetCurrentValue(float value)
    {
        currentValue = value;
        RefreshPriceText();
    }
}
