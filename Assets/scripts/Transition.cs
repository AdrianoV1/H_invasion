using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AnimationClip final;
    [SerializeField] private int SceneNum;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Transitions());
        }
    }

    IEnumerator Transitions()
    {
        anim.SetTrigger("Final");

        yield return new WaitForSeconds(final.length);

        SceneManager.LoadScene(SceneNum);
    }
}
