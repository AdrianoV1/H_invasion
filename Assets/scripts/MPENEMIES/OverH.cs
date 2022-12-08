using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class OverH : MonoBehaviour
{
    [Header("Range")]
    [SerializeField] private float StartRange;
    [SerializeField] private GameObject Slider;

    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private float Break_Distance;
    [SerializeField] private float Back_Distance;
    [SerializeField] private Transform PlayerT;

    [Header("Life")]
    [SerializeField] private Slider OSliderL;
    private int ChangeFormLife = 2;
    [SerializeField] private int LifeFirstForm = 200;
    [SerializeField] private int LifeSecondForm = 300;

    [Header("MecanicaBoss")]
    private Animator anim;
    [HideInInspector]public Rigidbody2D rb;
    private Transform playerT;

    [Header("Attack")]
    private float timer;
    [SerializeField]private float tempo;
    [SerializeField]private Transform point;
    [SerializeField] private GameObject Bullet;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
        anim= GetComponent<Animator>();
    }

    void Start()
    {
        OSliderL.maxValue = LifeFirstForm;
        OSliderL.value = OSliderL.maxValue;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, PlayerT.position) < StartRange)
        {
            Slider.SetActive(true);
            Movement();
            Flip();
        }

        if (ChangeFormLife == 1)
        {

        }

    }

    public void ReciveDamage(int Damage)
    {
        if (ChangeFormLife == 2)
        {
            LifeFirstForm -= Damage;
            OSliderL.value = LifeFirstForm;
            if (LifeFirstForm <= 0)
            {
                ChangeFormLife -= 1;
                OSliderL.maxValue = LifeSecondForm;
            }
        }else if (ChangeFormLife == 1)
        {
            LifeSecondForm -= Damage;
            OSliderL.value = LifeSecondForm;
            if (LifeSecondForm <= 0)
            {
                Destroy(gameObject);
                OSliderL.gameObject.SetActive(false);
            }
        }
    }

    public void Movement()
    {
        if (Vector2.Distance(transform.position, PlayerT.position) > Break_Distance)
        {
            transform.position = Vector2.MoveTowards(transform.position,playerT.position, _speed*Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, PlayerT.position) < Back_Distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerT.position, -_speed * Time.deltaTime);
            Attack();                      
        }if (Vector2.Distance(transform.position, PlayerT.position) > Back_Distance && Vector2.Distance(transform.position, PlayerT.position) < Break_Distance)
        {
            transform.position = transform.position;
            Attack();
        }
    }

    private void Flip()
    {
        if (PlayerT.position.x > this.transform.position.x)
        {
            this.transform.localScale = new Vector2(1,1);
        }
        else
        {
            this.transform.localScale = new Vector2(-1, 1);
        }
    }
        
    private void Attack()
    {
        timer += Time.deltaTime;
        if (timer >= tempo)
        {
            Instantiate(Bullet,point.position,point.rotation);
            timer = 0;
        }
    }
  
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Break_Distance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Back_Distance);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, StartRange);
    }

}
