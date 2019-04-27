using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickableJob : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI valueText;

    private SpriteRenderer sr;
    private Collider2D clickable;
    private Rigidbody2D rb;

    public StockType stockType;
    private JobSpawnController jobController;


    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        clickable = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }


    public void SetupJob(JobSpawnController jobController, StockType stockType)
    {
        this.jobController = jobController;
        this.stockType = stockType;

        nameText.text = stockType.name;
        sr.color = stockType.Color;
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
}
