using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class health : MonoBehaviour
{
    private int healthLevel = 10;
    [SerializeField] private Player player;
    private GameObject objPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        objPlayer = GameObject.FindGameObjectWithTag("Player");
        player= objPlayer.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.Healthlife(healthLevel);            
            Destroy(gameObject);
        }
    }
}
