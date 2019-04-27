using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public List<Investment> investments;
    [SerializeField]
    private DynamicText cashText;
    [SerializeField]
    private DynamicText netWorthText;
    [SerializeField]
    private DynamicText yearText;
    [SerializeField]
    private DynamicText totalYearText;

    [Header("Dialogs")]
    [SerializeField]
    private UIMarketPanel marketItemPanel;

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

    public void RefreshUIButtons()
    {
        //foreach (var investment in investments)
        //{
        //    investment.ToggleButton(investment.Cost <= GameManager.Instance.Cash);
        //}
    }

    public void UpdateCashText(float cash)
    {
        cashText.UpdateText(cash.ToString("N2"));
    }

    public void UpdateNetWorthText(float netWorth)
    {
        netWorthText.UpdateText(netWorth.ToString("N2"));
    }

    public void UpdateCurrentYear(int currentYear, int currentCompound)
    {
        yearText.UpdateText(string.Format("{0} {1}", currentYear, GameUtils.GetMonthFromInteger(currentCompound + 1)));
    }

    public void UpdateTotalYearsText(int totalYears)
    {
        totalYearText.UpdateText(totalYears);
    }

    public void InitializeStockContainers(StockController sc, List<StockData> stockDataList)
    {
        marketItemPanel.Initialize(sc, stockDataList);
    }

    public void ToggleMarketScreen(Action onCloseCallback = null)
    {
        marketItemPanel.Show();
    }
}
