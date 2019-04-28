using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClickableJob : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private float maxHealth = 10;
    [SerializeField]
    private float healthWorkFactor = 5;
    [SerializeField]
    private Image concealmentImage;
    [SerializeField]
    private Image healthImage;
    [SerializeField]
    private TextMeshProUGUI jobTypeText;
    [SerializeField]
    public SpriteRenderer forgroundSprite;

    private float currentHealth;

    public bool IsDraggable { get { return currentHealth <= 0; } }
    private bool isWorkerOnItem = false;
    public bool IsWorkerOnItem { get { return isWorkerOnItem; } }

    private JobStage stage;
    private bool hasTransitionedStageVisual = false;
    
    private Collider2D clickable;
    private Rigidbody2D rb;    
    private JobSpawnController jobController;

    
    [HideInInspector]
    public StockType stockType;
    [HideInInspector]
    public JobType jobType;

    private void Awake()
    {
        clickable = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(stage == JobStage.Open && hasTransitionedStageVisual == false)
        {
            // switch to open 
            hasTransitionedStageVisual = true;
            concealmentImage.gameObject.SetActive(false);
        }
        else if(stage == JobStage.Closed && isWorkerOnItem)
        {
            // show graphic for working
            currentHealth -= healthWorkFactor * Time.deltaTime;
            UpdateHealthImage();

            if (currentHealth <= 0)
            {
                isWorkerOnItem = false;
                stage = JobStage.Open;
                GameManager.Instance.ReleaseWorker();
            }
        }

        // else nothing is happneing?
    }


    public void SetupJob(JobSpawnController jobController, StockType stockType, JobType jobType)
    {
        this.jobController = jobController;
        this.stockType = stockType;
        this.jobType = jobType;

        currentHealth = maxHealth = (int)jobType.difficulty;
        nameText.text = stockType.name;
        forgroundSprite.color = stockType.Color;
        concealmentImage.sprite = jobType.sprite;
        jobTypeText.text = jobType.name;
    }

    public void PickUp(bool isGainValue)
    {
        if(isGainValue)
            GameManager.Instance.OnJobGained(this);

        jobController.ReportJobDestroyed(this);
        Destroy(gameObject);
    }

    public void Launch(Vector2 direction)
    {
        rb.AddForce(direction);
    }

    public void ClearVelocity()
    {
        rb.velocity = Vector2.zero;
    }

    public void StartWorkOnItem()
    {
        isWorkerOnItem = true;
        healthImage.gameObject.SetActive(true);
    }

    private void UpdateHealthImage()
    {
        healthImage.fillAmount = currentHealth / maxHealth;
    }

    public void RemoveWorker()
    {
        isWorkerOnItem = false;
    }
}
