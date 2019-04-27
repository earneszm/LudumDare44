using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickableJob : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int value;

    [SerializeField]
    private TextMeshProUGUI text;
    private Collider2D clickable;

    public int Value { get { return value; } }

    private void Awake()
    {
        clickable = GetComponent<Collider2D>();
    }

    private void Start()
    {
        UpdateText();
    }

    public void Clicked()
    {
        health--;

        if (health <= 0)
            PickUp();
        else
            UpdateText();
    }

    private void UpdateText()
    {
        text.text = health.ToString();
    }

    private void PickUp()
    {
        GameManager.Instance.OnJobGained(this);
        Destroy(gameObject);
    }
}
