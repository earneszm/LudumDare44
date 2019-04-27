using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            CheckHit();
    }

    public void CheckHit()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            var item = hit.collider.gameObject.GetComponent<ClickableJob>();

            if (item != null)
            {
                item.Clicked();
            }
        }
    }
}
