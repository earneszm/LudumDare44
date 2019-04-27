using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JobSpawnController : MonoBehaviour
{
    [SerializeField]
    private ClickableJob jobPrefab;
    [SerializeField]
    private List<JobCatcher> jobCatchers;


    [Header("Spawn Details")]
    [SerializeField]
    private float spawnFirstGroupAfterSeconds = 3f;
    [SerializeField]
    private float secondsBetweenSpawns = 3f;
    [SerializeField]
    private int jobsPerSpawn = 3;

    private float lastJobSpawned;


    [Header("Job Spawn Bounds")]
    [SerializeField]
    private Transform jobSpawnLeftBound;
    [SerializeField]
    private Transform jobSpawnRightBound;
    [SerializeField]
    private Transform jobSpawnTopBound;
    [SerializeField]
    private Transform jobSpawnBottomBound;

    private List<StockType> stockTypes;
    private List<ClickableJob> activeJobs = new List<ClickableJob>();
    private List<Vector3> allSpawnPositions = new List<Vector3>();

    // other systems
    private StockController sc;

    private void Awake()
    {
        sc = GetComponent<StockController>();
        stockTypes = sc.StockTypes;
    }

    private void Start()
    {
        SetupAllSpawnLocations();
        SetNextSpawnAsFirstGroup();
    }

    private void Update()
    {       
        if (GameManager.Instance.IsGamePlayActive == false)
            return;

        lastJobSpawned += Time.deltaTime;

        if (ShouldSpawnJob())
            DoJobSpawn();
    }

    private bool ShouldSpawnJob()
    {
        if (lastJobSpawned < secondsBetweenSpawns)
            return false;

        return true;
    }

    private void DoJobSpawn()
    {
        KillAllActiveJobs();

        lastJobSpawned = 0;

        var availablePositions = GetAvailableSpawnPositions();

        if (availablePositions.Count == 0)
            return;

        var availableStocks = stockTypes.ToList();

        for (int i = 0; i < jobsPerSpawn; i++)
        {
            var pos = GetRandomJobPosition(availablePositions);
            availablePositions.Remove(pos);

            var job = Instantiate(jobPrefab, pos, Quaternion.identity);
            var stockType = availableStocks[Random.Range(0, availableStocks.Count)];
            job.SetupJob(this, stockType);

            availableStocks.Remove(stockType);

            activeJobs.Add(job);
        }

        SetUpJobCatchers();
    }

    private void SetUpJobCatchers()
    {
        foreach (var catcher in jobCatchers)
        {
            catcher.Clear();
        }

        var availableCatchers = jobCatchers.ToList();

        foreach (var job in activeJobs)
        {
            var randomCatcher = availableCatchers[Random.Range(0, availableCatchers.Count)];
            randomCatcher.SetUpStockType(job.stockType);

            availableCatchers.Remove(randomCatcher);
        }
    }

    private Vector3 GetRandomJobPosition(List<Vector3> availablePositions)
    {
        // return new Vector3(Random.Range(jobSpawnLeftBound.position.x, jobSpawnRightBound.position.x),
        //                    Random.Range(jobSpawnBottomBound.position.y, jobSpawnTopBound.position.y),
        //                    0);
        

        return availablePositions[Random.Range(0, availablePositions.Count)];
    }

    private List<Vector3> GetAvailableSpawnPositions()
    {
        var availablePositions = new List<Vector3>();

        foreach (var pos in allSpawnPositions)
        {
            if (activeJobs.Any(x => x.transform.position.x == pos.x && x.transform.position.y == pos.y) == false)
                availablePositions.Add(pos);
        }

        return availablePositions;
    }

    private void SetupAllSpawnLocations()
    {
        for (float i = jobSpawnBottomBound.position.y; i <= jobSpawnTopBound.position.y; i++)
        {
            for (float k = jobSpawnLeftBound.position.x; k <= jobSpawnRightBound.position.x; k++)
            {
                allSpawnPositions.Add(new Vector3(k, i, 0));
            }
        }
    }

    public void ReportJobDestroyed(ClickableJob job)
    {
        activeJobs.Remove(job);
    }

    public void KillAllActiveJobs()
    {
        foreach (var job in activeJobs)
        {
            Destroy(job.gameObject);
        }

        activeJobs.Clear();
    }


    /// <summary>
    ///  call this to make the next spawn behave as if it were the first spawn (spawns it earlier than normal)
    /// </summary>
    public void SetNextSpawnAsFirstGroup()
    {
        lastJobSpawned = secondsBetweenSpawns - spawnFirstGroupAfterSeconds;
    }
}
