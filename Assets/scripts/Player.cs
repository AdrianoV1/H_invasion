using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Player")]
    private Rigidbody2D rb;
    [SerializeField, Range(100, 1000)] private float Speed = 100;
    private float Horizontal;
    private Animator anim;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 1;

    [Header("Bala")]
    [SerializeField] private GameObject[] Bullet;
    [SerializeField] private Transform point;
    [SerializeField] private float maxCharge;
    [SerializeField] private float timeCharge;

    [Header("Life")]
    [SerializeField] private Slider SliderLife;
    private int life = 200;

    [Header("Daño")]
    private bool CanMove = true;
    [SerializeField]private Vector2 BoundSpeed;
    [SerializeField] private float TimeLost;

    [Header("Death")]
    [SerializeField]private float deathTime;
    private Scene scenes;

    [Header("Special Shott")]
    private float timer = 5;
    private bool wait = false;

    public int kitAmount;
    public Text kitAmountText;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  
        anim= GetComponent<Animator>();
        scenes = SceneManager.GetActiveScene();
    }
    void Start()
    {
        SliderLife.maxValue= life;
        SliderLife.value = SliderLife.maxValue;
    }

    void Update()
    {
        if (CanMove)
        {
            Utils.Movement(rb, Speed, Horizontal, transform, anim);
            Jump();
            Shoot();
            Recover();
        }
        SliderLife.value = life;
        kitAmountText.text = "x" + kitAmount;
    }

   private void Jump()
    {
        if(Input.GetKeyDown("space") && CheckGround.isGrounded)
        {
            Debug.Log("jump");
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    
    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShootBullet(1);
            /*if (timeCharge <= maxCharge)
            {
                timeCharge += 0.01f;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            ShootBullet((int)timeCharge);
            timeCharge = 0;*/
        }
    }

    private void ShootBullet(int chargerTime)
    {
        Instantiate(Bullet[chargerTime], point.position,point.rotation);
    }

    public void Healthlife(int healthLife)
    {
        SliderLife.value += healthLife;
    }

    public void ReciveDamage(int damage)
    {
        life -= damage;
        Bound(new Vector2(10,10));
        StartCoroutine(LostControl());
        if (life <= 0)
        {

            StartCoroutine(Death());
        }
    }

    public void Bound(Vector2 KnockPoint)
    {
        rb.velocity = new Vector2(-BoundSpeed.x * KnockPoint.x, BoundSpeed.y);
    }

    private IEnumerator LostControl()
    {
        CanMove = false;
        yield return new WaitForSeconds(TimeLost);
        CanMove = true;
    }

    private IEnumerator Death()
    {
        anim.SetBool("Death",true);
        yield return new WaitForSeconds(deathTime);
        SceneManager.LoadScene(scenes.name);
    }

    void Recover()
    {
        if (Input.GetKeyDown(KeyCode.Q) && kitAmount>0)
        {
            kitAmount--;
            life += 50; 
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyBullet"))
        {
            ReciveDamage(9);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("death"))
        {
            StartCoroutine(Death());
        }
        if (collision.CompareTag("kit"))
        {
            Destroy(collision.gameObject);
            kitAmount++;
        }
    }
}