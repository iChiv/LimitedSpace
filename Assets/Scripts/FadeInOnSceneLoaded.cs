using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOnSceneLoaded : MonoBehaviour
{
    public CanvasGroup fadePanel;
    public float fadeDuration = 1f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadePanel.alpha = 1 - Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }
        
        // gameObject.SetActive(false);
    }
}
