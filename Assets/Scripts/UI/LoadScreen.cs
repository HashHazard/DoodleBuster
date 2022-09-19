using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    public string levelToLoad;

    public GameObject loadingScreen;
    public Animator transition;

    public void OnPressStart()
    {
        loadingScreen.SetActive(true);
        transition.SetTrigger("Start");
        StartCoroutine(LoadLevelAsync());
    }

    private IEnumerator LoadLevelAsync()
    {
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);
        asyncLoad.allowSceneActivation = false;
        while(asyncLoad.progress < 0.9f)
            yield return null;
        yield return new WaitForSeconds(1f);
        asyncLoad.allowSceneActivation = true;
    }
}
