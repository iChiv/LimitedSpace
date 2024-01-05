using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour
{
    public STARTPosition[] letters; // 字母脚本的数组
    public string nextSceneName = "Game"; // 要加载的下一个场景的名称

    void Update()
    {
        if (AreAllLettersInPlace())
        {
            SceneManager.LoadScene(nextSceneName); // 加载下一个场景
        }
    }

    bool AreAllLettersInPlace()
    {
        foreach (var letter in letters)
        {
            if (!letter.IsAtTargetPosition())
            {
                return false; // 如果有任何一个字母不在目标位置，返回 false
            }
        }
        return true; // 所有字母都在目标位置
    }
}
