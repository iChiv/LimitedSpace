using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartPrompt : MonoBehaviour
{
    public TextMeshProUGUI prompt;

    public float timer = 10f;

    public float blinkInterval = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        prompt.gameObject.SetActive(false);
        prompt.alpha = 1;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && !prompt.gameObject.activeInHierarchy)
        {
            prompt.gameObject.SetActive(true);
            StartCoroutine(BlinkPrompt());
        }
    }

    private IEnumerator BlinkPrompt()
    {
        while (true)
        {
            prompt.alpha = 0;
            yield return new WaitForSeconds(blinkInterval);
            
            prompt.alpha = 1;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
