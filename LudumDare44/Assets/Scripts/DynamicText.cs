﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DynamicText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI tmpText;
    [SerializeField]
    private string staticText;

    public void UpdateText(string dynamicText)
    {
        tmpText.text = string.Format("{0}{1}", staticText, dynamicText);
    }

    public void UpdateText(int dynamicText)
    {
        UpdateText(dynamicText.ToString());
    }
}