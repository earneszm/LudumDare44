using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float cash;
    public float Cash { get { return cash; } private set { cash = value; UIManager.Instance.UpdateCashText(cash); } }

    private float netWorth;
    public float NetWorth { get { return netWorth; } private set { netWorth = value; UIManager.Instance.UpdateNetWorthText(netWorth); } }

    [SerializeField]
    private int maxNumWorkers = 2;
    [SerializeField]
    private float victoryAmount = 50000;

    public int CurrentAvailableWorkers { get { return wc.AvailableWorkers; } }

    //private List<Investment> investments;
    private bool isGameMainGamePlayActive = true;
    public bool IsGamePlayActive { get { return isGameMainGamePlayActive; } }
    public bool IsGameOver { get; set; }


    // other systems
    private CompoundController tc;
    private StockController sc;
    private UIWorkerController wc;
    

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
        wc = UIManager.Instance.workerController;
        wc.InitializeStartingWorkers(maxNumWorkers);
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
        IsGameOver = true;
        UIManager.Instance.ShowGameOver(victoryAmount, NetWorth);
        Time.timeScale = 0;
    }

    public void DoCompounding()
    {
        //foreach (var investment in investments)
        //{
        //    investment.DoCompounding(tc.CompoundsPerYear);
        //}
        sc.DoStockTicks();        
        RefreshNetWorth();
        
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

    public void ReleaseWorker()
    {
        wc.ReleaseWorker();
    }

    public bool TryConsumeWorker()
    {        
        return wc.TryConsumeWorker();
    }

    public void StopAllWorkers()
    {
        wc.FreeAllWorkers();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void TogglePause()
    {
        if (IsGameOver)
            return;

        Time.timeScale = Time.timeScale == 0 ? 1 : 0;

        UIManager.Instance.TogglePauseOverlay(Time.timeScale == 0);
    }
    
}
