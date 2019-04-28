using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIJobSpawnIndicator : MonoBehaviour
{
    private Image indicator;

    private void Awake()
    {
        indicator = GetComponent<Image>();
    }

    public void UpdateIndicator(float percentTimeLeft)
    {
        indicator.fillAmount = percentTimeLeft;
    }
}
