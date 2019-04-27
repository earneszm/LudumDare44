using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private List<Investment> investments;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        RefreshUIButtons();
    }

    public void OnInvestmentClicked(Investment investment)
    {
        GameManager.Instance.OnInvestmentPurchased(investment);
        RefreshUIButtons();
    }

    private void RefreshUIButtons()
    {
        foreach (var investment in investments)
        {
            investment.ToggleButton(investment.Cost <= GameManager.Instance.Cash);
        }
    }
}
