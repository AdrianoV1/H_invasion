using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EveCollisionEvents : MonoBehaviour
{
    public BossEveController bossEveController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            bossEveController.TakeDamage(collision.GetComponent<Bullet>().damage);
            Debug.Log("Damage:" + collision.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject);
        }
    }
}
