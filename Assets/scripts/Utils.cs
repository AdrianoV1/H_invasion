using UnityEngine;

public static class Utils
{
    public static void Movement(Rigidbody2D rb, float r,float h, Transform t, Animator a)
    {
        h = Input.GetAxisRaw("Horizontal");

        if (h >= 1)
        {
            rb.velocity = new Vector2(r, rb.velocity.y);
            t.eulerAngles = new Vector3(0,0,0);
            a.SetBool("Run", true);
        }
        else if (h <= -1)
        {
            rb.velocity = new Vector2(-r, rb.velocity.y);
            t.eulerAngles = new Vector3(0, 180, 0);
            a.SetBool("Run", true);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            a.SetBool("Run", false);
        }
    }

}
