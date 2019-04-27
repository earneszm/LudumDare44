using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWorkerIndicator : MonoBehaviour
{
    [SerializeField]
    private Image availableIndicator;

    private bool isActive = false;
    public bool IsActive { get { return isActive; } }

    private bool isAvailable;
    public bool IsAvailable { get { return isAvailable; } }

    private int workerNumber;
    public int WorkerNumber { get { return workerNumber; } }

    public void SetAvailable(bool isAvailable)
    {
        this.isAvailable = isAvailable;
        availableIndicator.gameObject.SetActive(isAvailable);
    }

    public void SetWorkerNumber(int number)
    {
        this.workerNumber = number;
    }

    public void SetActiveWorker(bool isActive)
    {
        this.isActive = isActive;
        gameObject.SetActive(isActive);
    }
}
