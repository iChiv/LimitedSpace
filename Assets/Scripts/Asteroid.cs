using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Rigidbody2D rigidbody;

    [Range(0,50)]
    public float TorqueMax = 1.0f;

    [Range(0, 5)]
    public float SpeedMax = 5.0f;

    //public float speedMin = 1.0f; 
    //public float speedMax = 5.0f;
    //private float speed;

    public Sprite[] asteroidSprites;

    private Vector2 movementDirection;
    public Vector3 centerAreaPoint;
    private AsteroidGenerater asteroidGenerater;

    private bool hasAppearedOnScreen = false;

    void Start()
    {
        asteroidGenerater = GameObject.FindObjectOfType<AsteroidGenerater>();

        GetComponent<SpriteRenderer>().sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];

        transform.position = asteroidGenerater.GetRandomSpawnPositionOutsideCameraView();

        // ���㳯����Ļ���������һ�������ķ���
        Vector3 centerAreaPoint = GetRandomPointInScreenCenterArea();
        movementDirection = (centerAreaPoint - transform.position).normalized;

        // ���ø����ٶ�Ϊ������Ļ���ĵķ���
        rigidbody.velocity = movementDirection * SpeedMax;
        rigidbody.angularVelocity = Random.Range(-TorqueMax, TorqueMax);

    }

    Vector3 GetRandomPointInScreenCenterArea()
    {
        // ������Ļ��������ķ�Χ
        float minX = 0.2f; // ��Ļ��ȵİٷֱ�
        float maxX = 0.2f;
        float minY = 0.2f; // ��Ļ�߶ȵİٷֱ�
        float maxY = 0.2f;

        // ����Ļ�ٷֱ�ת��Ϊ��������
        Vector3 minScreenPoint = Camera.main.ViewportToWorldPoint(new Vector3(minX, minY, 0));
        Vector3 maxScreenPoint = Camera.main.ViewportToWorldPoint(new Vector3(maxX, maxY, 0));

        // �ڶ�������������������
        float randomX = Random.Range(minScreenPoint.x, maxScreenPoint.x);
        float randomY = Random.Range(minScreenPoint.y, maxScreenPoint.y);

        return new Vector3(randomX, randomY, 0);
    }

    private void Update()
    {
        if (IsVisibleFromCamera(Camera.main))
        {
            hasAppearedOnScreen = true;
        }
        else if (hasAppearedOnScreen)
        {
            // ����Ѿ���������Ļ�Ϲ����������ڲ�����Ļ�ϣ�������
            Destroy(gameObject);
        }
    }

    bool IsVisibleFromCamera(Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider2D>().bounds);
    }

    public void Destroy()
    {
        //play sound
        //spawn samall astroids
        //particles

        Destroy(gameObject);
    }


}
