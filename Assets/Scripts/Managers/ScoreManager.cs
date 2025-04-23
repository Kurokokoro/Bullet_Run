using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float initialVelocity;
    [SerializeField] private float minSlowDown;

    private int _score;
    private int _bestScore;
    private string _bestScoreKey;

    private float _currentVelocity;

    public int Score => _score;
    public int BestScore => _bestScore;
    public float VelocityFraction => (_currentVelocity / initialVelocity);
    public float VelocityFractionRestricted => (((minSlowDown * initialVelocity) + (_currentVelocity * minSlowDown)) / initialVelocity);


    private void Awake()
    {
        LoadBestScore();
    }

    public void InitializeScores()
    {
        GameManager.Instance.UIManager.HUD.UpdateScore(_score);
        GameManager.Instance.UIManager.HUD.UpdateBestScore(_bestScore);
    }

    public void IncreaseScore(int value)
    {
        _score += value;
        GameManager.Instance.UIManager.HUD.UpdateScore(_score, value);
    }

    public void FinalizeScore()
    {
        if (_score > _bestScore)
        {
            _bestScore = _score;
            GameManager.Instance.UIManager.HUD.UpdateBestScore(_bestScore);
            SaveBestScore();
        }
    }

    public void ResetCurrentScore()
    {
        _score = 0;
    }

    public void SetInitialVelocity()
    {
        _currentVelocity = initialVelocity;
        GameManager.Instance.UIManager.HUD.UpdateVelocity(_currentVelocity, initialVelocity);
    }

    public void ChangeVelocity(float value)
    {
        _currentVelocity += value;
        _currentVelocity = (_currentVelocity <= 0) ? 0 : _currentVelocity;

        GameManager.Instance.UIManager.HUD.UpdateVelocity(_currentVelocity, initialVelocity);

        if (_currentVelocity == 0f)
        {
            GameManager.Instance.FinishRound();
        }
    }

    private void LoadBestScore()
    {
        _bestScore = PlayerPrefs.GetInt(_bestScoreKey, 0);
    }

    private void SaveBestScore()
    {
        PlayerPrefs.SetInt(_bestScoreKey, _bestScore);
        PlayerPrefs.Save();
    }

}
