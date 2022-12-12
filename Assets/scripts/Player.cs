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
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.L))
        {
            currentCharge = 0;
            chargingParticles.Play();
        }
        if (Input.GetKey(KeyCode.L) || Input.GetKey(KeyCode.L))
        {
            //InstantiateBullet(1);
            if (currentCharge<1)
            {
                currentCharge += Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.L) || Input.GetKeyUp(KeyCode.L))
        {
            if (currentCharge < .5f)
            {
                Instantiate(bulletsPrefabs[0], bulletOrigin.transform.position, bulletOrigin.rotation);
            }
            else if (currentCharge >=.5f && currentCharge < .9f)
            {
                Instantiate(bulletsPrefabs[1], bulletOrigin.transform.position, bulletOrigin.rotation);
            }
            else if (currentCharge >=.9f)
            {
                Instantiate(bulletsPrefabs[2], bulletOrigin.transform.position, bulletOrigin.rotation);
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
            animator.SetBool("Run", true);
        }
        else if (directionalInput <= -1)
        {
            sprite.eulerAngles = new Vector3(0, 180, 0);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }
}