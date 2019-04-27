using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Cash;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void OnJobGained(ClickableJob job)
    {
        Cash += job.Value;

        Debug.Log(Cash);
    }

    public void OnInvestmentPurchased(Investment investment)
    {
        Cash -= investment.Cost;
    }
    
}
