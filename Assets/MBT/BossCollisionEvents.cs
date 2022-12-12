using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCollisionEvents : MonoBehaviour
{
    public BossBattleController bossBattleController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall") && bossBattleController.isJumping)
        {
            bossBattleController.isJumping = false;
            bossBattleController.animatorOverH.SetBool("Jump", false);
            //Debug.Log("land");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            bossBattleController.TakeDamage(collision.GetComponent<Bullet>().damage);
            Debug.Log("Damage:" + collision.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject);
        }
    }
}
