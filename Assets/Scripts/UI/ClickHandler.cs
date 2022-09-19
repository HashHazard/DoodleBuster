using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using DG.Tweening;


public class ClickHandler : MonoBehaviour
{
    public AudioMixer soundMixer;
    public AudioMixer musicMixer;

    [SerializeField] private GameObject _titlePanel;
    private GameObject _currentPanel;
    private GameObject _nextPanel;
    
    public void SetSoundVolume(float volume)
    {
        soundMixer.SetFloat("Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicMixer.SetFloat("Volume", volume);
    }

    private void Awake()
    {
        _currentPanel = _titlePanel;
        //_currentPanel.SetActive(true);
    }

    void ResetPanel(Transform panel)
    {
        panel.gameObject.SetActive(false);
        panel.DOScale(1f, 0f);
        _nextPanel.SetActive(true);
        _currentPanel = _nextPanel;
    }

    public void switchPanel(GameObject panel)
    {
        _nextPanel = panel;
        _currentPanel.transform.DOScale(0f, 0.25f).SetEase(Ease.InOutCirc).OnComplete(() => ResetPanel(_currentPanel.transform));
        //_currentPanel = panel;
    }
}
