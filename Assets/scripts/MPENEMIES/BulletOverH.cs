using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(PolygonCollider2D))]
public class BulletOverH : MonoBehaviour
{
    private Rigidbody2D rb;
    private int Damage = 12;
    [SerializeField, Range(100, 1000)] private int Speed = 100;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        { 
            collision.GetComponent<Player>().TakeDamage(Damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject,1);
        }
        Destroy(gameObject);
    }
}
