using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    private static GameManager _instance;

    [Header("SubManagers")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private ObjectPoolingManager objectPoolingManager;
    [SerializeField] private SoundManager soundManager;

    [Header("Additional Parameters")]
    [SerializeField] private Vector3 playerInitialPos;

    private UIManager _uiManager;
    private PlayerController _playerController;
    private bool _isGamePaused;

    public PlayerController Player => _playerController;
    public UIManager UIManager => _uiManager;
    public ScoreManager ScoreManager => scoreManager;
    public ObjectPoolingManager ObjectPoolingManager => objectPoolingManager;
    public SoundManager SoundManager => soundManager;
    public bool IsGamePaused => _isGamePaused;


    private void Awake()
    {
        CreateInstance();
    }

    private void Start()
    {
        StartRound();
    }

    private void CreateInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetUIManager(UIManager uiManager)
    {
        _uiManager = uiManager;
    }

    public void SetPlayer(PlayerController playerController)
    {
        _playerController = playerController;
        _playerController.ResetPlayer(playerInitialPos);
    }

    public void FinishRound()
    {
        _isGamePaused = true;
        _playerController.gameObject.SetActive(false);
        ScoreManager.FinalizeScore();
        ObjectPoolingManager.ReturnAllSpawnedObjects();
        _playerController.ResetPlayer(playerInitialPos);

        UIManager.GameOverPanel.UpdateScores(ScoreManager.Score, ScoreManager.BestScore);
        UIManager.ShowGameOverPanel();
    }

    public void StartRound()
    {
        ScoreManager.ResetCurrentScore();
        ScoreManager.SetInitialVelocity();
        ScoreManager.InitializeScores();
        _playerController.gameObject.SetActive(true);
        _isGamePaused = false;

        SoundManager.PlayDefaultOST();
        UIManager.HideGameOverPanel();
    }
}
