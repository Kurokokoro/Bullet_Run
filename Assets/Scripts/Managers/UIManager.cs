using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HUDPanel hud;
    [SerializeField] private GameOverPanel gameOverPanel;


    public HUDPanel HUD => hud;
    public GameOverPanel GameOverPanel => gameOverPanel;

    private void Awake()
    {
        GameManager.Instance.SetUIManager(this);
    }

    public void ShowGameOverPanel()
    {
        hud.HidePanel();
        gameOverPanel.ShowPanel();
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.HidePanel();
        hud.ShowPanel();
    }
}
