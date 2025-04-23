using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMPro.TextMeshProUGUI scoreValueText;
    [SerializeField] private TMPro.TextMeshProUGUI bestScoreValueText;

    public void UpdateScores(int currentScore, int bestScore)
    {
        scoreValueText.text = currentScore.ToString();
        bestScoreValueText.text = bestScore.ToString();
    }

    public void ShowPanel()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void HidePanel()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        if (canvasGroup.interactable && InputHandler.Instance.GetConfirmDown())
        {
            GameManager.Instance.StartRound();
        }
    }
}
