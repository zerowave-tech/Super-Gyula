using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneTransition sceneTransition;
    [Header("Watetime")] private float waitTime = 1.5f;

    public void PlayGame()
    {
        StartCoroutine(LoadGameWithDelay());
    }

    private IEnumerator LoadGameWithDelay()
    {
        // Просто ждем перед загрузкой
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadSceneAsync(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}