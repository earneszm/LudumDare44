using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStock : MonoBehaviour
{
    [SerializeField]
    private DynamicText label;

    private StockType stockType;
    private float currentValue;

    public void SetUpStock(StockType stockType)
    {
        this.stockType = stockType;
        SetStartingValue();
        label.SetStaticText(stockType.name + ": ");
    }

    private void SetStartingValue()
    {
        currentValue = Random.Range(stockType.MinStartingValue, stockType.MaxStartingValue);
        label.UpdateText(currentValue);
    }
}
