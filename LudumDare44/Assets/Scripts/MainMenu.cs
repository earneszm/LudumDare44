using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform yearSelectPanel;

    public void OnPlayButtonClicked()
    {
        yearSelectPanel.gameObject.SetActive(true);
    }



    public void OnExitButtonClicked()
    {
        Application.Quit();
    }


    public void OnYearSelected(int year)
    {
        GameUtils.GameLength = year;

        SceneManager.LoadScene(1);
    }

    public void OnCancelYearSelect()
    {
        yearSelectPanel.gameObject.SetActive(false);
    }
}
