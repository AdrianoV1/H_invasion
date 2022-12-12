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
        if (collision.CompareTag("enemy"))
        {
            playerScript.TakeDamage(40);
            //Destroy(collision.gameObject);
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
        if (collision.CompareTag("wall") && playerScript.isJumping)
        {
            //Debug.Log("landed");
            playerScript.animator.SetBool("Jump", false);
            playerScript.isJumping = false;
        }
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall") && playerScript.isJumping)
        {
            Debug.Log("landed");
            playerScript.animator.SetBool("Jump", false);
        }
    }*/
}
