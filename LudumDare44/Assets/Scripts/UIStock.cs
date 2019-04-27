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

    private void Awake()
    {
        backgroundImage = GetComponent<Image>();
    }

    public void SetUpStock(StockType stockType)
    {
        this.stockType = stockType;
        backgroundImage.color = stockType.Color;
        SetStartingValue();        
    }

    private void SetStartingValue()
    {
        nameText.text = stockType.name;
        currentValue = Random.Range(stockType.MinStartingValue, stockType.MaxStartingValue);
        priceText.text = currentValue.ToString("N2");
    }
}
