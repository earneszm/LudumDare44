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
    private DynamicText earnedFromjobsText;
    [SerializeField]
    private DynamicText earnedFromInvestingText;
    [SerializeField]
    private DynamicText resultText;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button quitButton;

    public void Show(float goalAmount, float scoreAmount, float amountFromJobs)
    {
        retireText.UpdateText(goalAmount);
        scoreText.UpdateText(scoreAmount);
        earnedFromjobsText.UpdateText(amountFromJobs);
        earnedFromInvestingText.UpdateText(scoreAmount - amountFromJobs);

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

