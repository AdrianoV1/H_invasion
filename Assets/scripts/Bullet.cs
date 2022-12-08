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
        /*if (collision.CompareTag("enemy"))
        {
            //collision.GetComponent<Enemy>().ReciveDamage(Damage);
            Destroy(gameObject);
        }else if (collision.CompareTag("OverH"))
        {
            //collision.GetComponent<OverH>().ReciveDamage(Damage);
            Destroy(gameObject);
        }else if (collision.CompareTag("Eve"))
        {
            //collision.GetComponent<Eve>().ReciveDamage(Damage);
            Destroy(gameObject);
        }
        Destroy(gameObject);*/
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(.6f);
        Destroy(gameObject);
    }
}