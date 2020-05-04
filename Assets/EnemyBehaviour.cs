using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject laser;
    public float Health = 150;
    public float LaserPerSec = 4f;
    public float LaserSpeed = 10;

    public int scoreValue = 150;
    private Score showScore;

    void Start()
    {
        showScore = GameObject.Find("Score").GetComponent<Score>();        
    }

    void Update()
    {
        float probabillity = LaserPerSec * Time.deltaTime;
        if(Random.value < probabillity)
        {
            Fire();
        }
    }

    void Fire()
    {
        Vector3 offset = new Vector3(0, -1.0f, 0);
        Vector3 ShotPosition = transform.position + offset;
        GameObject Laser = Instantiate(laser, ShotPosition, Quaternion.identity) as GameObject;
        Laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -LaserSpeed);
    }

    void Die()
    {
        showScore.ScoreBoard(scoreValue);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Laser laser = collider.gameObject.GetComponent<Laser>();
        if (laser)
        {
            laser.Hit();
            Health -= laser.GetDamage();
            if (Health <= 0)
            {
                Die();
            }
        }
    }
}
