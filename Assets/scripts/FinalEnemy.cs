using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] Lanza;
    [SerializeField] private Transform points;
    private float cronos;
    [SerializeField] private bool jump;
    private Animator anim;
    [SerializeField] private int Attack;
    [SerializeField] private bool Attacking;
    [SerializeField] private GameObject Target;
    [SerializeField] private GameObject[] Special;

    private RaycastHit2D hit;
    [SerializeField] private Vector3 v3;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask layer;

    private float gravity;
    [SerializeField] private int fases;

    void Start()
    {
        anim = GetComponent<Animator>();
        cronos = 1.5f; 
    }

    private void finalAni()
    {
        Lanza[0].SetActive(false);
        Lanza[1].SetActive(false);
        anim.SetBool("shield",false);
        anim.SetBool("gigaAttack",false);
        Attack = 0;
    }
    private void Distance()
    {
        /*Lanza[0].SetActive(true);
        Lanza[0].transform.position = points.position;
        Lanza[0].transform.rotation= transform.rotation;
        Lanza[0].GetComponent<BulletEnemy>().Speed = 500;*/
        Instantiate(Lanza[0],points.position, transform.rotation);
    }

    private bool CheckCollision
    {
        get
        {
            hit = Physics2D.Raycast(transform.position +v3, transform.up * -1 * distance);
            return hit.collider != null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position +v3, transform.up *-1*distance);
    }

    void attackJump()
    {
        if (jump)
        {
            switch (fases)
            {
                case 0:
                    gravity = 3;
                    fases++;
                    break;

                case 1:
                    if (!CheckCollision)
                    {
                        gravity -=6 * Time.deltaTime; 
                    }
                    else
                    {
                        fases++;
                    }
                    break;
                case 2:
                    jump = false;
                    anim.SetBool("Jump", false);
                    fases = 0;
                    break;
            }
        }
    }

    void Comportamiento()
    {
        switch (Attack)
        {
            case 0:
                if (!Attacking)
                {
                    cronos -= 1 * Time.deltaTime;
                }

                if (cronos <= 0)
                {
                    Attacking = true;
                    cronos= 0;

                    if (transform.position.x < Target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0,180,0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    Attack = Random.Range(1,5);
                }
                break;
            case 1:
                anim.SetBool("Bumerang",true);
                break; 
            case 2:
                anim.SetBool("Shield", true);
                break;
            case 3:
                anim.SetBool("Jump", true);
                break;
            case 4:
                anim.SetBool("Special", true);
                break;
        }
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            transform.Translate(Vector3.left*9*Time.deltaTime);
            transform.Translate(Vector3.up * gravity * Time.deltaTime);
        }
    }

    void Update()
    {
        attackJump();
        Comportamiento();
    }
}
