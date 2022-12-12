using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerScene : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private AnimationClip clip;
    [SerializeField] private int scene;

    public GameObject transitionCanvas;

    private void OnEnable()
    {
        transitionCanvas.SetActive(true);
    }

    public void ChangeScene()
    {
        FindObjectOfType<AudioManager>().Play("UIButton");
        FindObjectOfType<AudioManager>().FadeOutBGM();
        StartCoroutine(Change());
    }

    public void Exit()
    {
        FindObjectOfType<AudioManager>().Play("UIButton");
        FindObjectOfType<AudioManager>().FadeOutBGM();
        StartCoroutine(Quiting());
    }

    private IEnumerator Change()
    {
        anim.SetTrigger("End");

        yield return new WaitForSeconds(clip.length);
        SceneManager.LoadScene(scene);
    }

    private IEnumerator Quiting()
    {
        anim.SetTrigger("End");

        yield return new WaitForSeconds(clip.length);
        Application.Quit();
    }
}
