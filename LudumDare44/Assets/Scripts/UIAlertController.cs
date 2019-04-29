using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIAlertController : MonoBehaviour
{
    private List<UIAlert> allAlerts;

    private int maxNumberOfAlerts;

    private void Awake()
    {
        allAlerts = GetComponentsInChildren<UIAlert>().ToList();
        maxNumberOfAlerts = allAlerts.Count;

        allAlerts.ForEach(x => x.gameObject.SetActive(false));
    }

    public UIAlert SetNewAlert(string text, Color color)
    {
        var alert = GetNextInactiveAlert();

        if (alert == null)
            Debug.LogError("No More inactive alerts");
        else
        {
            alert.gameObject.transform.SetSiblingIndex(maxNumberOfAlerts);
            alert.ShowAlert(text, color);            
        }

        return alert;
    }

    private UIAlert GetNextInactiveAlert()
    {
        return allAlerts.FirstOrDefault(x => x.gameObject.activeInHierarchy == false);
    }
}
