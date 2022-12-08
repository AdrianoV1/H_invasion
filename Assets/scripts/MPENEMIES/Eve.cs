
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Eve : MonoBehaviour
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
    [SerializeField] private Slider Sliderlife;
    public int life = 250;

    [Header("Attack")]
    [SerializeField] private Transform point;
    [SerializeField] private GameObject bulletNormal;
    [SerializeField] private GameObject bulletSpecial;
    private float timerSpecial;
    private float timerShoot;

    void Start()
    {
        Sliderlife.maxValue = life;
        Sliderlife.value = Sliderlife.maxValue;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, PlayerT.position) < StartRange)
        {
            Slider.SetActive(true);            
            Movement(); 
            Flip();
            Attack();
        }
    }

    public void ReciveDamage(int Damage)
    {
        life -= Damage;
        Sliderlife.value = life;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Movement()
    {
        if (Vector2.Distance(transform.position, PlayerT.position) > Break_Distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerT.position, _speed);
        }
        if (Vector2.Distance(transform.position, PlayerT.position) < Back_Distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, PlayerT.position, -_speed);
            //Attack();
        }
        if (Vector2.Distance(transform.position, PlayerT.position) > Back_Distance && Vector2.Distance(transform.position, PlayerT.position) < Break_Distance)
        {
            transform.position = transform.position;
            
        }
    }
    
    private void Flip()
    {
        if (PlayerT.position.x > this.transform.position.x)
        {
            this.transform.localScale = new Vector2(43,29);
        }
        else
        {
            this.transform.localScale = new Vector2(-43, 29);
        }
    }

    private void Attack()
    {
        timerShoot += Time.deltaTime;
        timerSpecial += Time.deltaTime;

        if (timerShoot >=2)
        {
            Instantiate(bulletNormal, point.position, point.rotation);
            timerShoot= 0;
        }
        if (timerSpecial >= 10)
        {
            Instantiate(bulletSpecial, point.position, point.rotation);
            timerSpecial = 0;
            timerShoot = 0;
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
