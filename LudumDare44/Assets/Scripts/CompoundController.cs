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

    private float lastCompound;
    private int currentCompound;
    public int CurrentCompound { get { return currentCompound; } private set { currentCompound = value; UIManager.Instance.UpdateCurrentYear(CurrentYear, currentCompound); } }

    private int currentYear;
    public int CurrentYear { get { return currentYear; } private set { currentYear = value; UIManager.Instance.UpdateCurrentYear(currentYear, CurrentCompound); } }

    private void Start()
    {
        CurrentCompound = 0;
        CurrentYear = 1;
        UIManager.Instance.UpdateTotalYearsText(totalYears);
    }

    public void Update()
    {
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

        if(currentCompound > compoundsPerYear)
        {
            CurrentYear++;
            CurrentCompound = 0;
        }

        GameManager.Instance.DoCompounding();
    }
}
