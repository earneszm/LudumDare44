using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    [SerializeField]
    private DynamicText retireText;
    [SerializeField]
    private DynamicText scoreText;
    [SerializeField]
    private DynamicText resultText;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button quitButton;

    public void Show(float goalAmount, float scoreAmount)
    {
        retireText.UpdateText(goalAmount);
        scoreText.UpdateText(scoreAmount);

        if (scoreAmount >= goalAmount)
            resultText.UpdateText("You Win!");
        else
            resultText.UpdateText("You lost.");

        gameObject.SetActive(true);
    }

    public void OnRestartButtonClicked()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnQuitButtonClicked()
    {
        GameManager.Instance.QuitToMainMenu();
    }
}

