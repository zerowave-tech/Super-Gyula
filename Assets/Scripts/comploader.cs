using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class comploader : MonoBehaviour
{
    [Header("Настройки")]
    public Image CORPLOGO;
    public Image fadeImage;
    public float waitTime = 3f;
    public float fadeDuration = 1f;

    void Start()
    {

        if (CORPLOGO != null)
        {
            CORPLOGO.color = new Color(1, 1, 1, 0);
        }


        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 1);
            fadeImage.raycastTarget = false;
        }

        if (SceneManager.sceneCountInBuildSettings > 1)
        {
            StartCoroutine(ShowLogoSequence());
        }

    }

    IEnumerator ShowLogoSequence()
    {

        if (fadeImage != null)
        {
            yield return StartCoroutine(FadeScreen(1f, 0f, fadeDuration));
        }

        if (CORPLOGO != null)
        {
            yield return StartCoroutine(FadeLogo(0f, 1f, fadeDuration));
        }
        yield return new WaitForSeconds(waitTime);

        if (CORPLOGO != null)
        {
            yield return StartCoroutine(FadeLogo(1f, 0f, fadeDuration));
        }

        if (fadeImage != null)
        {
            yield return StartCoroutine(FadeScreen(0f, 1f, fadeDuration));
        }
        SceneManager.LoadScene(1);
    }

    IEnumerator FadeLogo(float fromAlpha, float toAlpha, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, timer / duration);
            CORPLOGO.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }

    IEnumerator FadeScreen(float fromAlpha, float toAlpha, float duration)
    {
        if (fadeImage == null) yield break;

        float timer = 0f;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, timer / duration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}