using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompoundController : MonoBehaviour
{
    [SerializeField]
    private float secondsPerCompound;
    [SerializeField]
    private int compoundsPerYear;
    public int CompoundsPerYear { get { return compoundsPerYear; } }
    [SerializeField]
    private int totalYears;
    public int TotalYears { get { return totalYears; } }

    [Header("DEBUG")]
    [SerializeField]
    private int startingCompound = 0;
    [SerializeField]
    private int startingYear = 1;

    private float lastCompound;
    private int currentCompound;
    public int CurrentCompound { get { return currentCompound; } private set { currentCompound = value; UIManager.Instance.UpdateCurrentYear(CurrentYear, currentCompound); } }

    private int currentYear;
    public int CurrentYear { get { return currentYear; } private set { currentYear = value; UIManager.Instance.UpdateCurrentYear(currentYear, CurrentCompound); } }
    
    // other systems
    private JobSpawnController jobSpawner;

    private void Awake()
    {
        jobSpawner = GetComponent<JobSpawnController>();
    }

    private void Start()
    {
        CurrentCompound = startingCompound;
        CurrentYear = startingYear;
        UIManager.Instance.UpdateTotalYearsText(totalYears);
    }

    public void Update()
    {
        if (GameManager.Instance.IsGamePlayActive == false)
            return;

        lastCompound += Time.deltaTime;

        if (lastCompound >= secondsPerCompound)
            DoCompound();
    }

    private void DoCompound()
    {
        if(CurrentYear >= totalYears && CurrentCompound >= compoundsPerYear)
        {
            // game over condition
            GameManager.Instance.GameOver();
            return;
        }        

        lastCompound = 0f;
        CurrentCompound++;

        GameManager.Instance.DoCompounding();

        if (currentCompound >= compoundsPerYear)
        {
            CurrentYear++;
            CurrentCompound = 0;

            // year is over, show market screen, pause game
            GameManager.Instance.ToggleGamePlayActive(false);
            jobSpawner.KillAllActiveJobs();

            UIManager.Instance.ToggleMarketScreen(() => { GameManager.Instance.ToggleGamePlayActive(true); jobSpawner.SetNextSpawnAsFirstGroup(); });
        }        
    }
}
