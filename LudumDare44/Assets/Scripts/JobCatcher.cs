using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JobCatcher : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;

    private SpriteRenderer sr;
    private BoxCollider2D col;
    private StockType stockType;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var job = collision.gameObject.GetComponent<ClickableJob>();

        if(job != null && job.stockType.name == stockType.name)
        {
            UIManager.Instance.AddFloatingText("+1 " + job.stockType.name, transform);
            job.PickUp(true);
        }
    }

    public void SetUpStockType(StockType stockType)
    {
        this.stockType = stockType;
        sr.color = stockType.Color;
        nameText.text = stockType.name;
        gameObject.SetActive(true);
    }

    public void Clear()
    {
        this.stockType = null;
        gameObject.SetActive(false);
    }
}
