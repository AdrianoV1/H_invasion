using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BossEveController : MonoBehaviour
{
    public Transform bossTr;
    public Rigidbody2D bossRb;

    public float life;
    public Slider sliderLife;
    private float maxLife = 200;
    
    public GameObject bossBulletPrefab;
    public Transform bulletOrigin;

    public Image backgroundConversation;

    public TarodevController.PlayerController playerControllerScript;

    public Image elaineImage;
    public RectTransform elaineImageShowPos;
    public RectTransform elaineImageHidePos;
    public Image overHimage;
    public RectTransform overHImageShowPos;
    public RectTransform overHImageHidePos;
    public Text currentDialogText;
    string nextDialogToShow;
    public int dialogPhase;

    public RectTransform dialogBox;
    public RectTransform dialogBoxShowPos;
    public RectTransform dialogBoxHidePos;

    public string bossPossition;

    public Animator animatorOverH;

    public Transform playerTransform;
    public Player playerScript;

    public bool bossSequenceStarted;
    public Image sceneTransition;
    public CanvasGroup bossInfo;
    bool canPressDialog;

    public Transform eveTr;
    public Transform eveBattleStartPoint;
    public bool isAlive;

    private void Start()
    {
        dialogPhase = 0;
        bossPossition = "right";

        sliderLife.maxValue = maxLife;
        sliderLife.value = sliderLife.maxValue;
        isAlive = true;
    }

    private void Update()
    {
        sliderLife.value = maxLife;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogBoxControl();
        }

        FollowPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !bossSequenceStarted)
        {
            bossSequenceStarted = true;
            playerControllerScript.controlsEnabled = false;
            EveAppear();
            StartCoroutine("WaitToStartConversation");
        }
    }

    void EveAppear()
    {
        eveTr.DOMove(eveBattleStartPoint.position,1);
    }

    IEnumerator WaitToStartConversation()
    {
        yield return new WaitForSeconds(1.5f);
        BackgroundConversationAppears();
    }

    void BackgroundConversationAppears()
    {
        backgroundConversation.DOFade(.7f, 1).OnComplete(ShowEveImage0);
    }

    void ShowEveImage0()
    {
        overHimage.rectTransform.DOMove(overHImageShowPos.position, .5f).OnComplete(ShowDialogBox0);
    }

    void ShowDialogBox0()
    {
        dialogBox.DOMove(dialogBoxShowPos.position, .5f).OnComplete(SetDialogPhase1);
    }

    void EnablePressDialog()
    {
        canPressDialog = true;
    }

    void SetDialogPhase1()
    {
        EnablePressDialog();
        dialogPhase = 1;
    }

    void DialogBoxControl()
    {
        switch (dialogPhase)
        {
            case 1:
                if (canPressDialog)
                {
                    canPressDialog = false;
                    SwitchDialog();
                    nextDialogToShow = "Tu... todo el tiempo supiste la verdad...\nLos humanos confiamos en tí Eve, ¡Y ahora nos traicionas!";
                    HideOverHImage();
                    ShowElaineImage();
                    dialogPhase = 2;
                }

                break;
            case 2:
                if (canPressDialog)
                {
                    canPressDialog = false;
                    SwitchDialog();
                    nextDialogToShow = "No le debo nada a la humanidad ni a tu gente,\nsolo le debo lealtad a un ser y ese es el Dr H.";
                    HideElaineImage();
                    ShowOverHImage();
                    dialogPhase = 3;
                }

                break;
            case 3:
                if (canPressDialog)
                {
                    canPressDialog = false;
                    SwitchDialog();
                    nextDialogToShow = "Tan solo eres una fría máquina que obedece órdenes...\nCreí que podía confiar en ti...";
                    HideOverHImage();
                    ShowElaineImage();
                    dialogPhase = 4;
                }

                break;
            
            case 4:
                if (canPressDialog)
                {
                    canPressDialog = false;
                    HideDialogBox0();
                    nextDialogToShow = "Será mejor que continúe...";
                    HideElaineImage();
                    BackgroundConversationDisappears();
                    dialogPhase = 0;
                    StartCoroutine("WaitToStartBattle");
                }
                break;
            case 5:
                if (canPressDialog)
                {
                    canPressDialog = false;
                    sceneTransition.DOFade(1,1).OnComplete(GoToNextLevel);
                }
                break;
            
        }

    }

    void GoToNextLevel()
    {
        SceneManager.LoadScene("Nivel 3");
    }

    void SwitchDialog()
    {
        FindObjectOfType<AudioManager>().Play("UIButton");
        HideDialogBox();
    }

    void HideDialogBox0()
    {
        dialogBox.DOMoveY(dialogBoxHidePos.position.y, .5f);
    }

    void HideDialogBox()
    {
        dialogBox.DOMoveY(dialogBoxHidePos.position.y, .5f).OnComplete(ChangeText);
    }

    void ChangeText()
    {
        currentDialogText.text = nextDialogToShow;
        ShowDialogBox();
    }

    

    void ShowOverHImage()
    {
        overHimage.rectTransform.DOMove(overHImageShowPos.position, .5f);
    }

    void HideOverHImage()
    {
        overHimage.rectTransform.DOMove(overHImageHidePos.position, .5f);
    }

    void ShowDialogBox()
    {
        dialogBox.DOMove(dialogBoxShowPos.position, .5f).OnComplete(EnablePressDialog);
    }

    void Die()
    {
        //
        if (isAlive)
        {
            isAlive = false;
            StartCoroutine("WaitToDialogAgain");
            playerControllerScript.controlsEnabled = false;
            animatorOverH.Play("DEATHEVE");
        } 
    }

    IEnumerator WaitToDialogAgain()
    {
        yield return new WaitForSeconds(3f);
        dialogPhase = 5;
        backgroundConversation.DOFade(.7f, 1).OnComplete(ShowElaineImage);
        SwitchDialog();
    }

    void ShowElaineImage()
    {
        elaineImage.rectTransform.DOMove(elaineImageShowPos.position, .5f);
    }

    void HideElaineImage()
    {
        elaineImage.rectTransform.DOMove(elaineImageHidePos.position, .5f);
    }

    void BackgroundConversationDisappears()
    {
        backgroundConversation.DOFade(0, 1);
    }

    IEnumerator WaitToStartBattle()
    {
        yield return new WaitForSeconds(.5f);
        bossInfo.DOFade(1, 1f);
        playerControllerScript.controlsEnabled = true;
        //StartMoving();
        Rafaga();
    }

    private void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Shot3");
        Instantiate(bossBulletPrefab, bulletOrigin.position, bulletOrigin.rotation);
    }

    void Rafaga()
    {
        if (isAlive)
        {
            animatorOverH.Play("RAFAGAEVE");
            InvokeRepeating("Shoot", 0, .1f);
            StartCoroutine("WaitToSTopRafaga");
        }
        
    }

    IEnumerator WaitToSTopRafaga()
    {
        yield return new WaitForSeconds(.3f);
        CancelInvoke("Shoot");
        StartCoroutine("WaitToJump");
    }

    public void Jump()
    {
        animatorOverH.Play("PATADAEVE");
        bossRb.AddForce(Vector2.up * 10000, ForceMode2D.Impulse);
        //animatorOverH.SetBool("Jump", true);
        //isJumping = true;
    }

    IEnumerator WaitToJump()
    {
        yield return new WaitForSeconds(1);
        JumpToPlayer();
    }

    void JumpToPlayer()
    {
        if (isAlive)
        {
            Jump();
            bossTr.DOMoveX(playerTransform.position.x, 1).SetEase(Ease.OutSine).OnComplete(A);
        }
        
    }

    void A()
    {
        StartCoroutine("WaitToRafagaAgain");
    }

    IEnumerator WaitToRafagaAgain()
    {
        yield return new WaitForSeconds(2);
        Rafaga();
    }

    void FollowPlayer()
    {
        if (playerTransform.position.x > bossTr.position.x)
        {
            bossTr.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (playerTransform.position.x < bossTr.position.x)
        {
            bossTr.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void TakeDamage(float Damage)
    {
        if (maxLife > Damage)
        {
            maxLife -= Damage;
        }
        else if (maxLife <= Damage)
        {
            maxLife -= Damage;
            Die();
        }

    }
}
