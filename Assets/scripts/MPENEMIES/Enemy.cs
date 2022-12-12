using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private float Speed;
    [SerializeField] private float legth;
    [SerializeField] private float counter;
    [SerializeField] private float StarPosition;
    private float ActualPosition;
    private float LastrPosition;
    private float time;

    [SerializeField] private float DistanceBreak;
    [SerializeField] private float DistanceBack;

    [Header("Seguir enemigo")]
    [SerializeField]private float VisionRange;
    private Transform PlayerT;

    [Header("Damage")]
    private bool Attacking;
    [SerializeField] private int giveDamage;

    [Header("Life Enemy")]
    public float life;
    [SerializeField]private Slider Sliderlife;
    private bool Death = false;

    [Header("Attack")]
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform point;

    public bool playerSighted;
    public bool atackStarted;
    public Animator animator;

    void Awake()
    {       
        //anim= GetComponent<Animator>();
        PlayerT = GameObject.FindWithTag("Player").transform;
    }
    void Start()
    {
        StarPosition = transform.position.x;
        Sliderlife.maxValue = life;
        Sliderlife.value = Sliderlife.maxValue;        
    }

    void Update()
    {        
        Movement();
        FollowPlayer();
    }

    public void TakeDamage(float Damage)
    {
        life -= Damage;
        Sliderlife.value = life;
        if (life <=0)
        {
            Destroy(gameObject);
        }
    }
        
    private void Movement()
    {

        if (Vector2.Distance(transform.position,PlayerT.position) > DistanceBreak)
        {
            if (!atackStarted)
            {
                counter += Time.deltaTime * Speed;

                transform.position = new Vector2(Mathf.PingPong(counter, legth) + StarPosition, transform.position.y);
                ActualPosition = transform.position.x;
                if (ActualPosition <= LastrPosition) this.transform.eulerAngles = new Vector3(0, 0, 0);;
                    if (ActualPosition >= LastrPosition) this.transform.eulerAngles = new Vector3(0, 180, 0);
                LastrPosition = transform.position.x;
            }

            if (playerSighted)
            {
                playerSighted = false;
                CancelInvoke("Shoot");
            }
        }
        else
        {
            if (!playerSighted)
            {
                playerSighted = true;
                InvokeRepeating("Shoot",0,2);
                animator.SetBool("Correr", false);
                FindObjectOfType<AudioManager>().Play("Detected");
            }

            atackStarted = true;
        }
        if (Vector2.Distance(transform.position, PlayerT.position) < DistanceBack)
        {
            //transform.position = Vector2.MoveTowards(transform.position, PlayerT.position, -Speed*Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, PlayerT.position) < DistanceBreak && Vector2.Distance(transform.position, PlayerT.position) > DistanceBack)
        {
            //transform.position = transform.position;
        }                
    }

    void FollowPlayer()
    {
        if (playerSighted)
        {
            if (PlayerT.position.x > this.transform.position.x)
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if(PlayerT.position.x < this.transform.position.x)
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    private void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Shot2");
        Instantiate(Bullet, point.position, point.rotation);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, DistanceBreak);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, DistanceBack);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            TakeDamage(collision.GetComponent<Bullet>().damage);
            Debug.Log("Damage:" + collision.GetComponent<Bullet>().damage);
            Destroy(collision.gameObject);
        }
    }
}
