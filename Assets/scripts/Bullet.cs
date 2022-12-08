using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField, Range(100,1000)]private float Speed = 100;
    [SerializeField]private int Damage;

    public GameObject collisionParticles;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("WaitToDestroy");
    }

    void Update()
    {
        rb.velocity = transform.right * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyBullet"))
        {
            Destroy(collision.gameObject);
            Instantiate(collisionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.CompareTag("wall"))
        {
            Instantiate(collisionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.CompareTag("enemy"))
        {
            Instantiate(collisionParticles, transform.position, Quaternion.identity);
        }
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(.6f);
        Instantiate(collisionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}