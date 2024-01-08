using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public STARTPosition[] letters; // 字母脚本的数组
    private int _nextSceneIndex;

    public CanvasGroup fadePanel;
    public float fadeTime = 1f;
    private bool _isTransitioning = false;

    private void Start()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        _nextSceneIndex = activeScene + 1 ;
    }

    void Update()
    {
        if (AreAllLettersInPlace() && !_isTransitioning)
        {
            StartCoroutine(FadeAndLoadScene());
            _isTransitioning = true;
        }
    }

    bool AreAllLettersInPlace()
    {
        foreach (var letter in letters)
        {
            if (!letter.IsAtTargetPosition())
            {
                return false;
            }
        }
        return true; 
    }

    IEnumerator FadeAndLoadScene()
    {
        float elapsed = 0f;
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            fadePanel.alpha = Mathf.Clamp01(elapsed / fadeTime);
            yield return null;
        }

        SceneManager.LoadScene(_nextSceneIndex);
    }
}
