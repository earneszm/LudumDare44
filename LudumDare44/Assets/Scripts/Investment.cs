using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Investment : MonoBehaviour
{
    public InvestmentType InvestmentType;
    public int Cost;

    [SerializeField]
    private TextMeshProUGUI buttonText;
    [SerializeField]
    private TextMeshProUGUI costText;

    private Button buyButton;

    private void Start()
    {
        buttonText.text = InvestmentType.ToString();
        costText.text = Cost.ToString();
    }

    public void ToggleButton(bool isEnabled)
    {
        buyButton.enabled = isEnabled;
    }
}
