using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobSpawnController : MonoBehaviour
{
    [SerializeField]
    private ClickableJob jobPrefab;

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

        var job = Instantiate(jobPrefab, GetRandomJobPosition(), Quaternion.identity);
        job.SetupJob(Random.Range(4, 10), Random.Range(100, 500));

        jobs.Add(job);
    }

    private Vector3 GetRandomJobPosition()
    {
        return new Vector3(Random.Range(jobSpawnLeftBound.position.x, jobSpawnRightBound.position.x),
                           Random.Range(jobSpawnBottomBound.position.y, jobSpawnTopBound.position.y),
                           0);
    }

}
