using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float width = 12f;
    public float height = 5f;
    private bool moveRight = true;
    public float speed = 5f;
    private float xMax;
    private float xMin;

    private void Start()
    {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));
        xMax = rightBorder.x;
        xMin = leftBorder.x;

        RespawnEnemy();
    }

    void RespawnEnemy()
    {
        foreach (Transform child in transform)
        {
            GameObject Enemy = Instantiate(EnemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            Enemy.transform.parent = child;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    void Update()
    {
        if (moveRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightBorderFormation = transform.position.x + (0.5f * width);
        float leftBorderFormation = transform.position.x - (0.5f * width);
        if(leftBorderFormation < xMin)
        {
            moveRight = true;
        }
        else if(rightBorderFormation > xMax)
        {
            moveRight = false;
        }
        if (AllMembersDead())
        {
            Debug.Log("Enemies died!");
            SceneManager.LoadScene("Win Screen");
        }
    }

    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform)
        {
            if(childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
}
