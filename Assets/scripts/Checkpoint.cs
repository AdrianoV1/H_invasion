using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AnimationClip clip;
    [SerializeField] private int scene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(Change());
        }
    }

    private IEnumerator Change()
    {
        anim.SetTrigger("End");

        yield return new WaitForSeconds(clip.length);
        SceneManager.LoadScene(scene);
    }
}
