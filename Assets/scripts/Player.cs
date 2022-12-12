using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public TarodevController.PlayerController playerControllerScript;

    float directionalInput;

    public GameObject[] bulletsPrefabs;
    public Transform bulletOrigin;
    public float currentCharge;
    public float maxCharge;

    public Slider SliderLife;
    private int maxLife = 200;

    public int kitAmount;
    public Text kitAmountText;

    public Animator animator;
    public Transform sprite;

    public ParticleSystem chargingParticles;
    public bool isJumping;
    bool chargedsSfx;

    void Start()
    {
        SliderLife.maxValue= maxLife;
        SliderLife.value = SliderLife.maxValue;
    }

    void Update()
    {
        if (playerControllerScript.controlsEnabled)
        {
            Shoot();
            Recover();
            SwitchAnims();
        }
        
        SliderLife.value = maxLife;
        kitAmountText.text = "x" + kitAmount;

        if (currentCharge >= 1 && !chargedsSfx)
        {
            chargedsSfx = true;
            FindObjectOfType<AudioManager>().Play("Charge");
        }
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.Return))
        {
            currentCharge = 0;
            chargingParticles.Play();
        }
        if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.Return))
        {
            //InstantiateBullet(1);
            if (currentCharge<1)
            {
                currentCharge += Time.deltaTime * .5f;
            }
        }
        if (Input.GetKeyUp(KeyCode.L) || Input.GetKeyUp(KeyCode.Return))
        {
            if (currentCharge < .5f)
            {
                FindObjectOfType<AudioManager>().Play("Shot3");
                Instantiate(bulletsPrefabs[0], bulletOrigin.transform.position, bulletOrigin.rotation);
            }
            else if (currentCharge >=.5f && currentCharge < .9f)
            {
                FindObjectOfType<AudioManager>().Play("Shot2");
                Instantiate(bulletsPrefabs[1], bulletOrigin.transform.position, bulletOrigin.rotation);
            }
            else if (currentCharge >=.9f)
            {
                FindObjectOfType<AudioManager>().Play("Shot1");
                Instantiate(bulletsPrefabs[2], bulletOrigin.transform.position, bulletOrigin.rotation);
                currentCharge = 0;
                chargedsSfx = false;
            }
            chargingParticles.Stop();
        }
    }

    private void InstantiateBullet(int chargeTime)
    {
        
    }

    public void Healthlife(int healthLife)
    {
        SliderLife.value += healthLife;
    }

    public void TakeDamage(int damage)
    {
        maxLife -= damage;
        if (maxLife <= 0)
        {

            Die();
        }
    }
    
    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Die");
    }

    void Recover()
    {
        if (Input.GetKeyDown(KeyCode.Q) && kitAmount>0)
        {
            kitAmount--;
            maxLife += 50; 
        }
    }    

    void SwitchAnims()
    {
        directionalInput = Input.GetAxisRaw("Horizontal");

        if (directionalInput >= 1)
        {
            sprite.eulerAngles = new Vector3(0, 0, 0);
            if (!isJumping)
            {
                animator.SetBool("Run", true);
            }
        }
        else if (directionalInput <= -1)
        {
            sprite.eulerAngles = new Vector3(0, 180, 0);
            if (!isJumping)
            {
                animator.SetBool("Run", true);
            }
        }
        else
        {
            animator.SetBool("Run", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Run", false);
            animator.SetBool("Jump", true);
            isJumping = true;
        }
    }
}