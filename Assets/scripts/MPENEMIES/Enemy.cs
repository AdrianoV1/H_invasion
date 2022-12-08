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
    public int life;
    [SerializeField]private Slider Sliderlife;
    private bool Death = false;

    [Header("Attack")]
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform point;

    public bool jugadorVisto;
    public bool ataqueIniciado;

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
        Moviment();
        MirarJugador();
    }

    public void ReciveDamage(int Damage)
    {
        life -= Damage;
        Sliderlife.value = life;
        if (life <=0)
        {
            Destroy(gameObject);
        }
    }
        
    private void Moviment()
    {

        if (Vector2.Distance(transform.position,PlayerT.position) > DistanceBreak)
        {
            if (!ataqueIniciado)
            {
                counter += Time.deltaTime * Speed;

                transform.position = new Vector2(Mathf.PingPong(counter, legth) + StarPosition, transform.position.y);
                ActualPosition = transform.position.x;
                if (ActualPosition <= LastrPosition) transform.localScale = new Vector3(-45, 51, 1);
                if (ActualPosition >= LastrPosition) transform.localScale = new Vector3(45, 51, 1);
                LastrPosition = transform.position.x;
            }

            if (jugadorVisto)
            {
                jugadorVisto = false;
                CancelInvoke("Shoot");
            }
        }
        else
        {
            if (!jugadorVisto)
            {
                jugadorVisto = true;
                InvokeRepeating("Shoot",0,1);
            }

            ataqueIniciado = true;
        }
        if (Vector2.Distance(transform.position, PlayerT.position) < DistanceBack)
        {
            //transform.position = Vector2.MoveTowards(transform.position, PlayerT.position, -Speed*Time.deltaTime);
            //Flip();
            //Shoot();
        }
        if (Vector2.Distance(transform.position, PlayerT.position) < DistanceBreak && Vector2.Distance(transform.position, PlayerT.position) > DistanceBack)
        {
            //transform.position = transform.position;
            //Flip();
            //Shoot();
        }                
    }

    /*private void Flip()
    {
        if (PlayerT.position.x > this.transform.position.x)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }*/

    void MirarJugador()
    {
        if (jugadorVisto)
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
        /*time += Time.deltaTime;
        if (time >=1)
        {
            Instantiate(Bullet,point.position, point.rotation);
            time= 0;
        }*/
        Instantiate(Bullet, point.position, point.rotation);
    }

    /*void IniciarAtaque()
    {
        if ()
        {

        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().ReciveDamage(giveDamage);
        }
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
            ReciveDamage(10);
            Destroy(collision.gameObject);
        }
        //Debug.Log("a");
    }
}
