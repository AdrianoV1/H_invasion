using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public float Speed = 100;
    [SerializeField] private int Damage = 9;
    public GameObject collisionParticles;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("WaitToDestroy");
    }

    void Update()
    {
        rb.velocity = transform.right*-1 * Speed;
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(2);
        Instantiate(collisionParticles, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall"))
        {
            Instantiate(collisionParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            Instantiate(collisionParticles, transform.position, Quaternion.identity);
        }
    }
}
