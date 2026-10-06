using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float loadDelay = 1.7f;

    private void Start()
    {

        StartCoroutine(FadeIn());
    }

    public void LoadSceneWithTransition(int sceneIndex)
    {
        StartCoroutine(TransitionCoroutine(sceneIndex));
    }

    public void LoadSceneWithTransition(string sceneName)
    {
        StartCoroutine(TransitionCoroutine(sceneName));
    }

    private IEnumerator TransitionCoroutine(int sceneIndex)
    {

        yield return StartCoroutine(FadeOut());

        yield return new WaitForSeconds(loadDelay);


        SceneManager.LoadSceneAsync(sceneIndex);
    }

    private IEnumerator TransitionCoroutine(string sceneName)
    {
        yield return StartCoroutine(FadeOut());
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadSceneAsync(sceneName);
    }

    private IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = 1f - (timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut()
    {
        fadeImage.gameObject.SetActive(true);
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = timer / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }
}
