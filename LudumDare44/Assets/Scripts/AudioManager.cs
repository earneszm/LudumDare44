using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource stockGained;
    [SerializeField]
    private AudioSource workerAssigned;
    [SerializeField]
    private AudioSource buy;
    [SerializeField]
    private AudioSource sell;
    [SerializeField]
    private AudioSource crash;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StockGained()
    {
        stockGained.Play();
    }

    public void WorkerAssigned()
    {
        workerAssigned.Play();
    }

    public void Buy()
    {
        buy.Play();
    }

    public void Sell()
    {
        sell.Play();
    }

    public void Crash()
    {
        crash.Play();
    }
}
