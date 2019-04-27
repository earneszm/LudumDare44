using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float cash;
    public float Cash { get { return cash; } private set { cash = value; UIManager.Instance.UpdateCashText(cash); } }

    private float netWorth;
    public float NetWorth { get { return netWorth; } private set { netWorth = value; UIManager.Instance.UpdateNetWorthText(netWorth); } }

    //private List<Investment> investments;
    private bool isGameMainGamePlayActive = true;
    public bool IsGamePlayActive { get { return isGameMainGamePlayActive; } }


    // other systems
    private CompoundController tc;
    private StockController sc;
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        tc = GetComponent<CompoundController>();
        sc = GetComponent<StockController>();
    }

    private void Start()
    {
        Cash = 0;        
      //  investments = UIManager.Instance.investments;
        RefreshNetWorth();
    }

    public void OnJobGained(ClickableJob job)
    {
        sc.BuyStock(job.stockType, 1, false);
        RefreshNetWorth();

        UIManager.Instance.RefreshUIButtons();
    }

    public void OnInvestmentPurchased(Investment investment)
    {
        Cash -= investment.Cost;
        investment.AddShare();
    }

    public void OnCashChanged(float amount)
    {
        Cash += amount;
        RefreshNetWorth();
    }

    public void SellAllShares()
    {
        sc.SellAllStock();
    }

    public void GameOver()
    {
        Debug.LogError("Game Over");
    }

    public void DoCompounding()
    {
        //foreach (var investment in investments)
        //{
        //    investment.DoCompounding(tc.CompoundsPerYear);
        //}

        RefreshNetWorth();
        sc.RefreshStockListUI();
    }

    public void RefreshNetWorth()
    {
        //  NetWorth = Cash + investments.Sum(x => x.CurrentValue);
        NetWorth = Cash + sc.stockDataList.Sum(x => x.OwnedValue);
    }

    public void ToggleGamePlayActive(bool isActive)
    {
        isGameMainGamePlayActive = isActive;
    }
    
}
