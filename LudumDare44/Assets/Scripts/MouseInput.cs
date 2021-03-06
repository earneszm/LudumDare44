﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    [SerializeField]
    private float throwSpeedModifier = 5f;

    private ClickableJob currentHoldObject;

    private Vector3 target;

    private Vector3 lastPos = Vector3.zero;
    private Vector3 delta = Vector3.zero;

    private int heldSortingOrder = 100;
    private int defaultSortingOrder = 5;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
            GameManager.Instance.TogglePause();

        // if the game is paused, do not allow any interaction
        if (Time.timeScale == 0)
            return;

        if (Input.GetMouseButton(0))
        {
            if (currentHoldObject == null)
                CheckHit();
            else
                UpdatePositions();
        }
        else
            ReleaseObject();

        
    }

    private void LateUpdate()
    {
        if (currentHoldObject != null)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            currentHoldObject.transform.position = target;
        }
    }

    public void CheckHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            var item = hit.collider.gameObject.GetComponent<ClickableJob>();

            if (item != null)
            {
                //item.Clicked();
                if (item.IsDraggable)
                {
                    currentHoldObject = item;
                    currentHoldObject.ClearVelocity();
                    currentHoldObject.forgroundSprite.sortingOrder = heldSortingOrder;
                    lastPos = Input.mousePosition;
                }
                else if(item.IsWorkerOnItem == false && GameManager.Instance.TryConsumeWorker())
                {
                    // add worker to item
                    UIManager.Instance.AddFloatingText("+$" + item.moneyForStartingJob, item.transform);
                    GameManager.Instance.OnCashChanged(item.moneyForStartingJob);
                    item.StartWorkOnItem();
                }
                else
                {
                    // no available workers, create alert?
                }
            }
        }
    }

    private void ReleaseObject()
    {
        if (currentHoldObject != null)
        {
            currentHoldObject.forgroundSprite.sortingOrder = defaultSortingOrder;
            delta = delta.normalized;
            var force = new Vector2(delta.x * throwSpeedModifier, delta.y * throwSpeedModifier);
            currentHoldObject.Launch(force);
            currentHoldObject = null;
        }
    }

    private void UpdatePositions()
    {
        delta = Input.mousePosition - lastPos;

        lastPos = Input.mousePosition;
    }
}
