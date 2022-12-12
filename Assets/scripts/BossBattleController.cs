using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BossBattleController : MonoBehaviour
{
    public Transform bossTr;
    public Rigidbody2D bossRb;

    public bool lockedBarrier;

    public Transform barrier;
    public Transform barrierLockPosition;

    public float life;
    public Slider sliderLife;

    public Transform attackPointLeft;
    public Transform attackPointRight;

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
    public bool isJumping;

    private void Start()
    {
        dialogPhase = 0;
        bossPossition = "right";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Jump();
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogBoxControl();
        }
    }

    public void LockBarrier()
    {
        if (!lockedBarrier)
        {
            barrier.DOMove(barrierLockPosition.position, .5f).SetEase(Ease.Flash);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && dialogPhase == 0)
        {
            LockBarrier();
            playerControllerScript.controlsEnabled = false;
            //playerControllerScript.StopMovement();
            StartCoroutine("WaitToStartConversation");
        }
            }

    

    IEnumerator WaitToStartConversation()
    {
        yield return new WaitForSeconds(1);
        BackgroundConversationAppears();
    }

    void MoveLeftRight()
    {
        switch (bossPossition)
        {
            case "left":
                RunToRightPoint();
                break;
            case "right":
                RunToLeftPoint();
                break;
        }
    }

    public void RunToLeftPoint()
    {
        CancelInvoke("Shoot");
        CancelInvoke("Jump");
        animatorOverH.SetBool("Run", true);
        bossTr.DOMoveX(attackPointLeft.position.x,3).SetEase(Ease.Flash).OnComplete(PointToRight);
    }

    void PointToRight()
    {
        bossPossition = "left";
        animatorOverH.SetBool("Run", false);
        bossTr.rotation = Quaternion.Euler(0, 0, 0);
        InvokeRepeating("Shoot",.5f,1.5f);
        InvokeRepeating("Jump", 1, 1.5f);
    }

    public void RunToRightPoint()
    {
        CancelInvoke("Shoot");
        CancelInvoke("Jump");
        animatorOverH.SetBool("Run", true);
        bossTr.DOMoveX(attackPointRight.position.x, 3).SetEase(Ease.Flash).OnComplete(PointToLeft);
    }

    void PointToLeft()
    {
        bossPossition = "right";
        animatorOverH.SetBool("Run", false);
        bossTr.rotation = Quaternion.Euler(0, 180, 0);
        InvokeRepeating("Shoot", .5f, 1.5f);
        InvokeRepeating("Jump", 1, 1.5f);
    }

    public void Jump()
    {
        bossRb.AddForce(Vector2.up*7000, ForceMode2D.Impulse);
        animatorOverH.SetBool("Jump", true);
        isJumping = true;
    }

    private void Shoot()
    {
        Instantiate(bossBulletPrefab, bulletOrigin.position, bulletOrigin.rotation);
    }

    public void TakeDamage(int Damage)
    {
        life -= Damage;
        sliderLife.value = life;
        if (life <= 0)
        {
            Die();
        }
    }

    void Die()
    {

    }

    void ShowElaineImage()
    {
        elaineImage.rectTransform.DOMove(elaineImageShowPos.position,.5f);
    }

    void HideElaineImage()
    {
        elaineImage.rectTransform.DOMove(elaineImageHidePos.position, .5f);
    }

    void BackgroundConversationAppears()
    {
        backgroundConversation.DOFade(.7f, 1).OnComplete(ShowOverHImage0);
    }

    void BackgroundConversationDisappears()
    {
        backgroundConversation.DOFade(0, 1);
    }

    void ShowOverHImage0()
    {
        overHimage.rectTransform.DOMove(overHImageShowPos.position, .5f).OnComplete(ShowDialogBox0);
    }

    void ShowOverHImage()
    {
        overHimage.rectTransform.DOMove(overHImageShowPos.position, .5f);
    }

    void HideOverHImage()
    {
        overHimage.rectTransform.DOMove(overHImageHidePos.position, .5f);
    }

    void ShowDialogBox0()
    {
        dialogBox.DOMove(dialogBoxShowPos.position, .5f).OnComplete(SetDialogPhase1);
    }

    void ShowDialogBox()
    {
        dialogBox.DOMove(dialogBoxShowPos.position, .5f);
    }

    void SetDialogPhase1()
    {
        dialogPhase = 1;
    }

    void DialogBoxControl()
    {
        switch (dialogPhase)
        {
            case 1:
                SwitchDialog();
                nextDialogToShow = "Pronto la humanidad me agradecerá haberme desecho de ti...\nTe respetaba tanto... Es increíble que planearas\nextinguir a los que te dieron todo...";
                HideOverHImage();
                ShowElaineImage();
                dialogPhase = 2;
                break;
            case 2:
                SwitchDialog();
                nextDialogToShow = "Es por un bien mayor, los humanos destruyen el planeta y se niegan\na reconocerlo, son un cáncer para su mundo, y acabarán con él...\na menos que yo lo impida.";
                HideElaineImage();
                ShowOverHImage();
                dialogPhase = 3;
                break;
            case 3:
                SwitchDialog();
                nextDialogToShow = "Puede que tangas razón... pero es nuestro planeta,\ny si es nuestro destino, moriremos con él.";
                HideOverHImage();
                ShowElaineImage();
                dialogPhase = 4;
                break;
            case 4:
                SwitchDialog();
                nextDialogToShow = "Vaya desperdicio de talento...\nParecía ser un verdadero prodigio, señorita Leinheart,\nque lástima que deba convertirla en fertilizante para plantas...";
                HideElaineImage();
                ShowOverHImage();
                dialogPhase = 5;
                break;
            case 5:
                HideDialogBox0();
                HideOverHImage();
                BackgroundConversationDisappears();
                dialogPhase = 6;
                StartCoroutine("WaitToStartBattle");
                break;
        }

    }

    void SwitchDialog()
    {
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

    IEnumerator WaitToStartBattle()
    {
        yield return new WaitForSeconds(.5f);
        playerControllerScript.controlsEnabled = true;
        StartMoving();
    }

    void StartMoving()
    {
        InvokeRepeating("MoveLeftRight", 0,15);
    }
}
