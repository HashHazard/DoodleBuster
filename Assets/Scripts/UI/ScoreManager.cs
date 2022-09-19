using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float transitionSpeed = 100;

    [SerializeField] private GameObject GemParent;
    [SerializeField] private TextMeshProUGUI gemText;

    private CanvasGroup gemCanvas;


    public int Score { get; private set; }
    float displayScore;

    public int gemScore { get; private set; }

    private void Awake()
    {
        Instance = this;
        gemCanvas = GemParent.GetComponent<CanvasGroup>();
        gemCanvas.alpha = 0f;
    }

    public int GetGems()
    {
        return gemScore;
    }
    public int GetScore()
    {
        return Score;
    }

    public void UpdateGemSore(int amount = 1)
    {
        gemScore += amount;
        gemText.SetText(gemScore.ToString());

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(gemCanvas.DOFade(1f, 0.4f).SetEase(Ease.InCirc))
            .AppendInterval(1.5f)
            .Append(gemCanvas.DOFade(0f, 0.8f).SetEase(Ease.OutCirc));
    }

    private void Update()
    {
        displayScore = Mathf.MoveTowards(displayScore, Score, transitionSpeed * Time.deltaTime);

        UpdateScoreDisplay();
    }

    public void IncreaseScore(int amount)
    {
        Score += amount;
        Sequence mySecquence = DOTween.Sequence();
        mySecquence.Append(transform.DOScale(1.1f, 0.3f).SetEase(Ease.OutExpo))
            .AppendInterval(0.2f)
            .Append(transform.DOScale(1f, 0.2f).SetEase(Ease.OutCirc));
    }

    public void UpdateScoreDisplay()
    {
        //scoreText.text = "Score: " + score;
        //scoreText.SetText(((int)score).ToString());
        scoreText.SetText(string.Format("{0:00000}", (int)displayScore));
    }
}
