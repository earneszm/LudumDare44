using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Investment : MonoBehaviour
{
    public InvestmentType InvestmentType;
    public int Cost;
    public float AnnualRateOfReturn;

    private int currentValue;
    public int CurrentValue { get { return currentValue; } private set { currentValue = value; currentValueText.UpdateText(currentValue.ToString()); } }

    [SerializeField]
    private TextMeshProUGUI buttonText;
    [SerializeField]
    private TextMeshProUGUI costText;
    [SerializeField]
    private DynamicText currentValueText;

    private Button buyButton;

    private void Awake()
    {
        buyButton = GetComponentInChildren<Button>();
    }

    private void Start()
    {        
        buttonText.text = InvestmentType.ToDescription();
        costText.text = "$" + Cost.ToString();
        CurrentValue = 0;

        buyButton.onClick.AddListener(() => UIManager.Instance.OnInvestmentClicked(this));
    }

    public void ToggleButton(bool isEnabled)
    {
        buyButton.interactable = isEnabled;
    }

    public void AddShare()
    {
        CurrentValue += Cost;
    }

    public void DoCompounding(int compoundsPerAnnum)
    {
        CurrentValue = Mathf.RoundToInt(CurrentValue * (1 + ((AnnualRateOfReturn / 100) / compoundsPerAnnum)));
    }
}
