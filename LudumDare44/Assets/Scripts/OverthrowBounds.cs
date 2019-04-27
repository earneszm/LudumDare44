using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverthrowBounds : MonoBehaviour
{
    private BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var job = collision.gameObject.GetComponent<ClickableJob>();

        if (job != null)
        {
            job.PickUp(false);
        }
    }
}
