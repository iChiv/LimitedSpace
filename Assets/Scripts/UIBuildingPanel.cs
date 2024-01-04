using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBuildingPanel : MonoBehaviour
{
    public GameObject buildingPanel;

    private CanvasGroup _panelCanvasGroup;

    public float fadeDuration = 0.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        _panelCanvasGroup = buildingPanel.GetComponent<CanvasGroup>();
        _panelCanvasGroup.alpha = 0f;
    }

    public void ShowPanel()
    {
        StartCoroutine(FadeCanvsGroup(_panelCanvasGroup, _panelCanvasGroup.alpha, 1, fadeDuration));
    }
    
    public void HidePanel()
    {
        StartCoroutine(FadeCanvsGroup(_panelCanvasGroup, _panelCanvasGroup.alpha, 0, fadeDuration));
    }

    private IEnumerator FadeCanvsGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsed / duration);
            yield return null;
        }

        cg.alpha = end;
    }
}
