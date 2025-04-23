using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HUDPanel : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [Header("Elements")]
    [SerializeField] private TMPro.TextMeshProUGUI speedText;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private AnimatedText animatedText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreValueText;
    [SerializeField] private TMPro.TextMeshProUGUI bestScoreValueText;

    [Header("Parameters")]
    [SerializeField] private string speedUnit;

    public void UpdateScore(int score)
    {
        scoreValueText.text = score.ToString();
    }

    public void UpdateScore(int total, int value)
    {
        UpdateScore(total);
        animatedText.StartScoreAnimation(value);
    }

    public void UpdateBestScore(int score)
    {
        bestScoreValueText.text = score.ToString();
    }

    public void UpdateVelocity(float value, float maxValue)
    {
        speedText.text = string.Format("{0} {1}", (int)value, speedUnit);
        speedSlider.value = value / maxValue;
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
}
