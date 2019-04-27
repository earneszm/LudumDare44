using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIWorkerController : MonoBehaviour
{
    private List<UIWorkerIndicator> workers;

    public int AvailableWorkers { get { return workers.Count(x => x.IsAvailable); } }

    private void Awake()
    {
        workers = GetComponentsInChildren<UIWorkerIndicator>().ToList();

        int workerNumber = 1;

        foreach (var worker in workers)
        {
            worker.SetWorkerNumber(workerNumber);
            workerNumber++;
        }
    }

    public void InitializeStartingWorkers(int number)
    {
        SetActiveWorkers(number);

        for (int i = 0; i < number; i++)
        {
            workers[i].SetAvailable(true);
        }
    }

    public void SetActiveWorkers(int numActive)
    {
        // deactivate any workers with a number higher than the starting number
        workers.ForEach(x => x.SetActiveWorker(x.WorkerNumber <= numActive));
    }

    public bool TryConsumeWorker()
    {
        for (int i = workers.Count - 1; i >= 0; i--)
        {
            if (workers[i].IsActive && workers[i].IsAvailable)
            {
                workers[i].SetAvailable(false);
                return true;
            }
        }

        // no workers available
        return false;
    }

    public void ReleaseWorker()
    {
        // release first active but non available worker
        for (int i = workers.Count - 1; i >= 0; i--)
        {
            if (workers[i].IsActive && workers[i].IsAvailable == false)
            {
                workers[i].SetAvailable(true);
                return;
            }
        }
    }
}
