using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject _gameMenu, _pauseMenu, _scoreboardMenu, _gameoverMenu;
    [SerializeField] private TextMeshProUGUI _score, _gems, _totalScore;
    private GameObject _currentPanel;

    [SerializeField] private GameObject _highscore;

    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _runtimeObjects;

    private static PauseHandler instance;
    public static PauseHandler Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<PauseHandler>();
            return instance;
        }
    }

    private void Awake()
    {
        _currentPanel = _gameMenu;
        _currentPanel.SetActive(true);
        _pauseMenu.SetActive(false);
        _gameoverMenu.SetActive(false);
        _scoreboardMenu.SetActive(false);
        _highscore.SetActive(false);
    }

    private void CalculateScore()
    {
        int s, g;
        s = ScoreManager.Instance.GetScore();
        g = ScoreManager.Instance.GetGems();
        _score.SetText("+" + s);
        _gems.SetText("+" + g);
        _totalScore.SetText((s + g).ToString());
    }

    public void Scoreboard()
    {
        CalculateScore();
        switchPanel(_scoreboardMenu);
    }

    public void PauseGame()
    {
        switchPanel(_pauseMenu);
        StartCoroutine(freeze());
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        switchPanel(_gameMenu);
    }

    IEnumerator freeze()
    {
        yield return new WaitForSeconds(0.252f);
        Time.timeScale = 0f;

    }

    //Reloads the Level
    public void Reload()
    {
        Time.timeScale = 1f;
        DOTween.Clear();
        DOTween.KillAll();
        SceneManager.LoadScene("SampleScene");
    }
    public void LoadHome()
    {
        Time.timeScale = 1f;
        DOTween.Clear();
        DOTween.KillAll();
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator resetFreeze()
    {

        yield return new WaitForSecondsRealtime(3f);
        DOTween.Clear();
        DOTween.KillAll();
        //Scoreboard();
    }

    IEnumerator SlowMo()
    {
        Time.timeScale = 0.25f;
        yield return new WaitForSecondsRealtime(2f);
        _runtimeObjects.SetActive(false);
        _player.SetActive(false);
        Time.timeScale = 1f;
        HealthBar.Instance.gameObject.SetActive(false);
        DOTween.Clear();
        DOTween.KillAll();
        Scoreboard();
    }

    public void OnDeath()
    {
        if (PlayerController.Instance != null)
            PlayerController.Instance.isPlayerDead = true;
        if (PlayerPrefs.GetInt("HighScore") < ScoreManager.Instance.GetScore())
        {
            PlayerPrefs.SetInt("HighScore", ScoreManager.Instance.GetScore());
            _highscore.SetActive(true);
        }
        PlayerPrefs.SetInt("TotalGems", PlayerPrefs.GetInt("TotalGems") + ScoreManager.Instance.GetGems());
        PlayerPrefs.Save();

        StartCoroutine(SlowMo());

        //Scoreboard();
        
        //StartCoroutine(freeze());
        //StartCoroutine(resetFreeze());
    }

    /// <summary>
    /// Switching panel with simple scaling animation
    /// </summary>
    /// <param name="panel"></param>
    void ResetPanel(Transform panel)
    {
        if (panel != null)
        {
            panel.DOScale(1f, 0f);

        }
        panel.gameObject.SetActive(false);
        _currentPanel.SetActive(true);
    }

    public void switchPanel(GameObject panel)
    {
        _currentPanel.transform.DOScale(0f, 0.25f).SetEase(Ease.InOutCirc).OnComplete(() => ResetPanel(_currentPanel.transform));
        _currentPanel = panel;
        //panel.SetActive(true);
    }
}
