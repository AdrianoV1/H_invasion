using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletEnemy : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField, Range(100, 1000)] private float Speed = 100;
    [SerializeField] private int Damage = 9;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right*-1 * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.gameObject.GetComponent<Player>().ReciveDamage(Damage);
            //collision.GetComponent<Player>().ReciveDamage(Damage,pos);
            Destroy(gameObject);
        }
        //Destroy(gameObject,0.1f);
    }
}
