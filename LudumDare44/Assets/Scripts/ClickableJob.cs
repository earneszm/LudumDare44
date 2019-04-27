using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickableJob : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI healthText;
    [SerializeField]
    private TextMeshProUGUI valueText;

    private int currentHealth;

    
    private Collider2D clickable;

    private JobType jobType;
    private JobSpawnController jobController;

    public int Value { get { return jobType.Value; } }

    private void Awake()
    {
        clickable = GetComponent<Collider2D>();
    }

    public void Clicked()
    {
        currentHealth--;

        if (currentHealth <= 0)
            PickUp();
        else
            UpdateHealthText();
    }

    public void SetupJob(JobSpawnController jobController, JobType jobType)
    {
        this.jobController = jobController;
        this.jobType = jobType;
        this.currentHealth = jobType.Health;

        nameText.text = jobType.name;
        UpdateHealthText();
    }

    private void UpdateHealthText()
    {
        healthText.text = currentHealth.ToString();
    }

    private void PickUp()
    {
        GameManager.Instance.OnJobGained(this);
        jobController.ReportJobDestroyed(this);
        Destroy(gameObject);
    }
}
