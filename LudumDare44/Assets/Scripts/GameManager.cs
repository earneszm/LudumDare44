using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int cash;
    public int Cash { get { return cash; } private set { cash = value; UIManager.Instance.UpdateCashText(cash); } }

    private int netWorth;
    public int NetWorth { get { return netWorth; } private set { netWorth = value; UIManager.Instance.UpdateNetWorthText(netWorth); } }

    private List<Investment> investments;


    // other systems
    private CompoundController tc;
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        tc = GetComponent<CompoundController>();
    }

    private void Start()
    {
        Cash = 0;
        RefreshNetWorth();
        investments = UIManager.Instance.investments;
    }

    public void OnJobGained(ClickableJob job)
    {
        Cash += job.Value;
        RefreshNetWorth();

        UIManager.Instance.RefreshUIButtons();
    }

    public void OnInvestmentPurchased(Investment investment)
    {
        Cash -= investment.Cost;
        investment.AddShare();
    }

    public void GameOver()
    {
        Debug.LogError("Game Over");
    }

    public void DoCompounding()
    {
        foreach (var investment in investments)
        {
            investment.DoCompounding(tc.CompoundsPerYear);
        }

        RefreshNetWorth();
    }

    public void RefreshNetWorth()
    {
        NetWorth = Cash + investments.Sum(x => x.CurrentValue);
    }

    
}
