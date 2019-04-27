using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class JobSpawnController : MonoBehaviour
{
    [SerializeField]
    private ClickableJob jobPrefab;
    [SerializeField]
    private List<JobType> jobTypes;

    [Header("Spawn Details")]
    [SerializeField]
    private int maxJobsPerMinute;
    [SerializeField]
    private float minSecondsBetweenJobs = 3f;
    [SerializeField]
    [Range(0, 1)]
    private float jobSpawnDecayPerMinute = 0.2f;

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

    private List<ClickableJob> jobs = new List<ClickableJob>();
    private List<Vector3> allSpawnPositions = new List<Vector3>();

    private void Start()
    {
        SetupAllSpawnLocations();
    }

    private void Update()
    {
        lastJobSpawned += Time.deltaTime;

        if (ShouldSpawnJob())
            DoJobSpawn();
    }

    private bool ShouldSpawnJob()
    {
        if (lastJobSpawned < minSecondsBetweenJobs)
            return false;

        return true;
    }

    private void DoJobSpawn()
    {
        lastJobSpawned = 0;

        var availablePositions = GetAvailableSpawnPositions();

        if (availablePositions.Count == 0)
            return;

        var job = Instantiate(jobPrefab, GetRandomJobPosition(availablePositions), Quaternion.identity);
        job.SetupJob(this, jobTypes[Random.Range(0, jobTypes.Count)]);

        jobs.Add(job);
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
            if (jobs.Any(x => x.transform.position.x == pos.x && x.transform.position.y == pos.y) == false)
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
        jobs.Remove(job);
    }

}
