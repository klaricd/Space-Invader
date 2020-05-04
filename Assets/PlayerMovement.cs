using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject laser;
    public float laserSpeed;

    public float speed = 15.0f;
    public float health = 250;
    float xMin;
    float xMax;

    private void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 LeftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 RightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xMin = LeftBorder.x;
        xMax = RightBorder.x;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject Laser = Instantiate(laser, transform.position, Quaternion.identity) as GameObject;
        }

        // limits movement of space ship out of scene
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Laser laser = collider.gameObject.GetComponent<Laser>();
        if (laser)
        {
            health -= laser.GetDamage();
            laser.Hit();
            if(health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Lose Screen");
    }
}
