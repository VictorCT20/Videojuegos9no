using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    private GameManagerController gameManager;
    int vida = 2;
    Rigidbody2D rb;
    public int velo = 2;
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(-velo, rb.velocity.y);//hace que el zombie camine
        if(vida==0) {
            Destroy(this.gameObject);
            gameManager.GanarPuntos();
        }
    }
    void OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.tag == "Bala"){
            vida--;
        }

    }
}
