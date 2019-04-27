using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class UIAlert : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private Image background;

    public void ShowAlert(string text, Color color)
    {
        titleText.text = text;
        background.color = color;

        gameObject.SetActive(true);

        StartCoroutine(FadeAfterSeconds(4));
    }

    private IEnumerator FadeAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        gameObject.SetActive(false);
    }
}
