using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public bool breakEnemyBullets;

    public GameObject collisionParticles;

    void Start()
    {
        StartCoroutine("WaitToDestroy");
    }

    void Update()
    {
        transform.Translate(Vector2.right*speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyBullet"))
        {
            if (breakEnemyBullets)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                Instantiate(collisionParticles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        if (collision.CompareTag("wall"))
        {
            FindObjectOfType<AudioManager>().Play("CollisionProjectile");
            Instantiate(collisionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.CompareTag("enemy"))
        {
            FindObjectOfType<AudioManager>().Play("CollisionProjectile");
            Instantiate(collisionParticles, transform.position, Quaternion.identity);
        }
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(.7f);
        FindObjectOfType<AudioManager>().Play("CollisionProjectile");
        Instantiate(collisionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}