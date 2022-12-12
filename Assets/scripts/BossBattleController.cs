using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BossBattleController : MonoBehaviour
{
    public Transform bossTr;
    public Rigidbody2D bossRb;

    public bool lockedBarrier;

    public Transform barrier;
    public Transform barrierLockPosition;

    public float life;
    public Slider sliderLife;
    private float maxLife = 200;

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
    public Animator animatorOverH2;
    public bool isJumping;
    public int bossPhase;

    public Transform playerTransform;
    public Transform playerPhase2Pos;
    public Transform bossPhase2Pos;
    public Player playerScript;

    public SpriteRenderer bossSprite1;
    public SpriteRenderer bossSprite2;

    public GameObject bossInfoSprite1;
    public GameObject bossInfoSprite2;

    public bool canBeDamaged;
    public bool bossSequenceStarted;
    public Image sceneTransition;
    public CanvasGroup bossInfo;
    bool canPressDialog;
    bool followPlayer;

    private void Start()
    {
        dialogPhase = 0;
        bossPossition = "right";

        sliderLife.maxValue = maxLife;
        sliderLife.value = sliderLife.maxValue;
        bossPhase = 1;
    }

    private void Update()
    {
        sliderLife.value = maxLife;
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SuperHit();
        }*/
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DialogBoxControl();
        }
        //FollowPlayer();
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
        if (collision.CompareTag("Player") && !bossSequenceStarted)
        {
            bossSequenceStarted = true;
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
        bossTr.DOMoveX(attackPointLeft.position.x,2).SetEase(Ease.Flash).OnComplete(PointToRight);
    }

    void PointToRight()
    {
        if (bossPhase == 1)
        {
            bossPossition = "left";
            animatorOverH.SetBool("Run", false);
            bossTr.rotation = Quaternion.Euler(0, 0, 0);
            InvokeRepeating("Shoot", .5f, 1f);
            InvokeRepeating("Jump", 1, 1.5f);
        }
        
    }

    public void RunToRightPoint()
    {
        CancelInvoke("Shoot");
        CancelInvoke("Jump");
        animatorOverH.SetBool("Run", true);
        bossTr.DOMoveX(attackPointRight.position.x, 2).SetEase(Ease.Flash).OnComplete(PointToLeft);
    }

    void PointToLeft()
    {
        if (bossPhase == 1)
        {
            bossPossition = "right";
            animatorOverH.SetBool("Run", false);
            bossTr.rotation = Quaternion.Euler(0, 180, 0);
            InvokeRepeating("Shoot", .5f, 1f);
            InvokeRepeating("Jump", 1, 1.5f);
        }
        
    }

    public void Jump()
    {
        bossRb.AddForce(Vector2.up*8000, ForceMode2D.Impulse);
        animatorOverH.SetBool("Jump", true);
        isJumping = true;
    }

    private void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Shot3");
        Instantiate(bossBulletPrefab, bulletOrigin.position, bulletOrigin.rotation);
    }

    public void TakeDamage(float Damage)
    {
        if (canBeDamaged)
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

    void Die()
    {
        if (bossPhase == 1)
        {
            animatorOverH.Play("HUMANOHURT");
            CancelInvoke("Shoot");
            CancelInvoke("Jump");
            CancelInvoke("MoveLeftRight");
            canBeDamaged = false;
            bossPhase = 2;
            PhaseTransitionStart();
        }
        else if (bossPhase == 2)
        {
            CancelInvoke("SuperHit");
            animatorOverH2.Play("FINALDEATH 0");
            playerControllerScript.controlsEnabled = false;
            StartCoroutine("WaitForDeathAnim");
        }
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
        dialogBox.DOMove(dialogBoxShowPos.position, .5f).OnComplete(EnablePressDialog);
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
                    nextDialogToShow = "Pronto la humanidad me agradecerá haberme desecho de ti...\nTe respetaba tanto... Es increíble que planearas\nextinguir a los que te dieron todo...";
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
                    nextDialogToShow = "Es por un bien mayor, los humanos destruyen el planeta y se niegan\na reconocerlo, son un cáncer para su mundo, y acabarán con él...\na menos que yo lo impida.";
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
                    nextDialogToShow = "Puede que tangas razón... pero es nuestro planeta,\ny si es nuestro destino, moriremos con él.";
                    HideOverHImage();
                    ShowElaineImage();
                    dialogPhase = 4;
                }
                
                break;
            case 4:
                if (canPressDialog)
                {
                    canPressDialog = false;
                    SwitchDialog();
                    nextDialogToShow = "Vaya desperdicio de talento...\nParecía ser un verdadero prodigio, señorita Leinheart,\nque lástima que deba convertirla en fertilizante para plantas...";
                    HideElaineImage();
                    ShowOverHImage();
                    dialogPhase = 5;
                }
                
                break;
            case 5:
                if (canPressDialog)
                {
                    canPressDialog = false;
                    HideDialogBox0();
                    nextDialogToShow = "Niña egoísta... e ingenua, mi Eve\nacabará contigo... en un instante...";
                    HideOverHImage();
                    BackgroundConversationDisappears();
                    canBeDamaged = true;
                    dialogPhase = 0;
                    StartCoroutine("WaitToStartBattle");
                }
                
                break;
            case 6:
                if (canPressDialog)
                {
                    canPressDialog = false;
                    SwitchDialog();
                    nextDialogToShow = "Ella no vendrá Dr. H, nunca más va a venir.\nPara quitar el mal de raíz es necesario cortar las ramas también.";
                    HideOverHImage();
                    ShowElaineImage();
                    dialogPhase = 7;
                }
                
                break;
            case 7:
                if (canPressDialog)
                {
                    canPressDialog = false;
                    SwitchDialog();
                    nextDialogToShow = "¿¡QUE!? ¿¡QUÉ HAS HECHO MALDITA HUMANA!?\nTEN POR SEGURO QUE MORIRÁS... Y DE LA FORMA\nMAS DOLOROSA QUE PUEDAS IMAGINAR";
                    HideElaineImage();
                    ShowOverHImage();
                    dialogPhase = 8;
                }
                
                break;
            case 8:
                if (canPressDialog)
                {
                    canPressDialog = false;
                    HideDialogBox0();
                    nextDialogToShow = "...";
                    HideOverHImage();
                    BackgroundConversationDisappears();
                    dialogPhase = 0; InvokeRepeating("IntercalarSprites", 1, .1f);
                    FindObjectOfType<AudioManager>().Play("Transform");
                    StartCoroutine("WaitForTransformation");
                }
                
                break;
            case 9:
                if (canPressDialog)
                {
                    canPressDialog = false;
                    SwitchDialog();
                    nextDialogToShow = "Finalmente... Todo ha terminado.";
                    dialogPhase = 10;
                }
                
                break;
            case 10:
                if (canPressDialog)
                {
                    canPressDialog = false;
                    sceneTransition.DOFade(1, 1).OnComplete(GoToMenu);
                }
                
                break;
        }

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

    IEnumerator WaitToStartBattle()
    {
        yield return new WaitForSeconds(.5f);
        bossInfo.DOFade(1, 1f);
        playerControllerScript.controlsEnabled = true;
        StartMoving();
    }

    void StartMoving()
    {
        InvokeRepeating("MoveLeftRight", 0,15);
    }

    void PhaseTransitionStart()
    {
        playerControllerScript.controlsEnabled = false;
        backgroundConversation.DOFade(1,1).OnComplete(A);
    }
    private void A()
    {
        StartCoroutine("WaitOnBlack");
    }
    IEnumerator WaitOnBlack()
    {
        yield return new WaitForSeconds(2);
        MoveCharactersToPhase2Pos();
        PhaseTransitionEnd();
    }
    void MoveCharactersToPhase2Pos()
    {
        bossTr.position = bossPhase2Pos.position;
        playerTransform.position = playerPhase2Pos.position;
        playerScript.sprite.eulerAngles = new Vector3(0, 0, 0);
        bossTr.rotation = Quaternion.Euler(0, 180, 0);
        //PhaseTransitionEnd();
    }

    void PhaseTransitionEnd()
    {
        backgroundConversation.DOFade(.7f, 1)/*.OnComplete(MoveCharactersToPhase2Pos)*/.OnComplete(StartConversationAgain);
        dialogPhase = 6;
    }

    void StartConversationAgain()
    {
        ShowOverHImage();
        SwitchDialog();
    }

    IEnumerator WaitForTransformation()
    {
        yield return new WaitForSeconds(3f);
        CancelInvoke("IntercalarSprites");
        bossSprite1.enabled = false;
        bossSprite2.enabled = true;
        bossInfo.DOFade(0, .5f).OnComplete(SwitchSpritesInfo);
        animatorOverH2.enabled = true;
        StartCoroutine("WaitToStarBattle2");
    }

    void SwitchSpritesInfo()
    {
        FindObjectOfType<AudioManager>().Play("Alien");
        maxLife = 200;
        bossInfoSprite1.SetActive(false);
        bossInfoSprite2.SetActive(true);
        bossInfo.DOFade(1, .5f);
    }

    IEnumerator WaitToStarBattle2()
    {
        yield return new WaitForSeconds(2);
        
        canBeDamaged = true;
        playerControllerScript.controlsEnabled = true;
        InvokeRepeating("SuperHit", 2f, 1.5f);
        followPlayer = true;
    }

    void IntercalarSprites()
    {
        bossSprite1.enabled = !bossSprite1.enabled;
        bossSprite2.enabled = !bossSprite2.enabled;
    }

    void SuperHit()
    {
        animatorOverH2.Play("FINALJUMP");
        Jump();
        bossTr.DOMoveX(playerTransform.position.x, 1).SetEase(Ease.OutSine).OnComplete(B);
    }

    private void B()
    {
        FindObjectOfType<AudioManager>().Play("Stomp");
    }

    IEnumerator WaitForDeathAnim()
    {
        yield return new WaitForSeconds(5);
        backgroundConversation.DOFade(.7f, 1).OnComplete(StarFinalWords);
    }

    void StarFinalWords()
    {
        ShowElaineImage();
        SwitchDialog();
        dialogPhase = 9;
    }

    void FollowPlayer()
    {
        if (followPlayer)
        {
            if (playerTransform.position.x > bossTr.transform.position.x)
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (playerTransform.position.x < bossTr.transform.position.x)
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
