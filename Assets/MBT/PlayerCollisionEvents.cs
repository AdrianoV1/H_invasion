using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionEvents : MonoBehaviour
{
    public Player playerScript;
    public TutorialController tutorialController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyBullet"))
        {
            playerScript.TakeDamage(9);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("death"))
        {
            playerScript.Die();
        }
        if (collision.CompareTag("kit"))
        {
            Destroy(collision.gameObject);
            playerScript.kitAmount++;
        }
        if (collision.CompareTag("tuto1"))
        {
            tutorialController.ShowTuto1();
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("tuto2"))
        {
            tutorialController.ShowTuto2();
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("tutoEnd"))
        {
            tutorialController.EndTuto();
            Destroy(collision.gameObject);
        }
    }
}
