using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public static class UtilsEnemy
{
    public static void MovementEnemy(float s, float l, float c, float f , Transform t, float fp, float lp)
    {        
        t.position = new Vector2(Mathf.PingPong(c,l) + s, t.position.y);
        fp = t.position.x;

        if(fp < lp) t.localScale = new Vector3(-1, 1, 1);
        if (fp > lp) t.localScale = new Vector3(1, 1, 1);

        fp = t.position.x;
    } 
    private static void lookPlayer(Transform Player, float s, float db, float dr, Transform p)
    {

    }

    private static void Flip(Transform t,float s,float db, float dr,Transform p)
    {
        if (t.position.x > p.position.x)
        {
            p.localScale = new Vector2(1,1);
        }
        else
        {
            p.localScale = new Vector2(-1, 1);
        }
    }

    public static void Attack(Transform Control,float Radious,int Attack)
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(Control.position, Radious);
        foreach (Collider2D collision in objects)
        {
            collision.GetComponent<Player>().TakeDamage(Attack);
        }
    }
}
