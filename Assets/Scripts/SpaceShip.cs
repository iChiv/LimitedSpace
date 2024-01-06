using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class SpaceShip : AsteroidImpact
{
    public float health;
    
    
    [Header("Resource Count")]
    public int resourceCountTypeA = 0;
    public int resourceCountTypeB = 0;
    public int resourceCountTypeC = 0;

    [Header("Resource Level Points")] 
    public int lv1Points = 1;
    public int lv2Points = 5;
    public int lv3Points = 30;
    
    [Header("Resource Display")]
    public string resourceNameTypeA = "Square";
    public string resourceNameTypeB = "Circle";
    public string resourceNameTypeC = "Triangle";
    public TextMeshProUGUI resourceCountTextTypeA;
    public TextMeshProUGUI resourceCountTextTypeB;
    public TextMeshProUGUI resourceCountTextTypeC;

    public Buildings[] buildings;
    public GameObject explosionVFX;
    public AudioClip spaceshipExplosion;
    public AudioClip collectSound;

    public CanvasGroup fadePanel;
    public float fadeTime = 1f;

    private void Start()
    {
        hp = health;
    }

    new void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ResourceTypeA") ||
            other.gameObject.CompareTag("ResourceTypeB") ||
            other.gameObject.CompareTag("ResourceTypeC"))
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            Resources resources = other.gameObject.GetComponent<Resources>();
            if (resources != null)
            {
                AddResource(resources.resourceLevel, other.gameObject.tag);
            }
            Destroy(other.gameObject);
        }
        
        base.OnCollisionEnter2D(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ResourceTypeA") ||
            other.gameObject.CompareTag("ResourceTypeB") ||
            other.gameObject.CompareTag("ResourceTypeC"))
        {
            GetComponent<Rigidbody2D>().isKinematic = false; // 重新启用物理影响
        }
    }

    protected override void DestoryObject()
    {
        if (explosionVFX != null)
        {
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }

        if (spaceshipExplosion != null)
        {
            AudioSource.PlayClipAtPoint(spaceshipExplosion, transform.position, volume);
        }
        StartCoroutine(FadeOutAndGameOver());
        Destroy(gameObject,2f);
    }
    
    IEnumerator FadeOutAndGameOver()
    {
        float elapsed = 0f;
        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            fadePanel.alpha = Mathf.Clamp01(elapsed / fadeTime);
            yield return null;
        }
        Invoke(nameof(GameOver),1f);
    }
    
    void AddResource(int level, string resourceType)
    {
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, volume);
        }
        
        int points = 0;
        switch (level)
        {
            case 1: points = lv1Points; break;
            case 2: points = lv2Points; break;
            case 3: points = lv3Points; break;
            default: points = 0; break;
        }

        if (resourceType == "ResourceTypeA")
        {
            resourceCountTypeA += points;
            UpdateResourceCountDisplay(resourceCountTextTypeA, resourceCountTypeA);
        }
        else if (resourceType == "ResourceTypeB")
        {
            resourceCountTypeB += points;
            UpdateResourceCountDisplay(resourceCountTextTypeB, resourceCountTypeB);
        }
        else if (resourceType == "ResourceTypeC")
        {
            resourceCountTypeC += points;
            UpdateResourceCountDisplay(resourceCountTextTypeC, resourceCountTypeC);
        }

        foreach (var t in buildings)
        {
            if (t != null)
            {
                t.UpdateResources(resourceCountTypeA,resourceCountTypeB,resourceCountTypeC);
            }
        }
    }
    
    void UpdateResourceCountDisplay(TextMeshProUGUI textMesh, int count)
    {
        if (textMesh != null)
        {
            textMesh.text = count.ToString();
        }
    }

    public void ResourceConsume()
    {
        UpdateResourceCountDisplay(resourceCountTextTypeA, resourceCountTypeA);
        UpdateResourceCountDisplay(resourceCountTextTypeB, resourceCountTypeB);
        UpdateResourceCountDisplay(resourceCountTextTypeC, resourceCountTypeC);
        foreach (var t in buildings)
        {
            if (t != null)
            {
                t.UpdateResources(resourceCountTypeA,resourceCountTypeB,resourceCountTypeC);
            }
        }
    }

    public int CalculateScore()
    {
        return resourceCountTypeA + resourceCountTypeB + resourceCountTypeC;
    }

    void GameOver()
    {
        int totalScore = CalculateScore();
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (totalScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", totalScore);
            PlayerPrefs.Save();
        }
        
        SceneManager.LoadScene("End");
    }
}
