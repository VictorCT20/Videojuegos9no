using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    private GameManager gameManager;
    const int Ani_quieto = 0;
    const int Ani_correr = 1;
    const int Ani_ataque = 2;
    const int Ani_Slide = 3;
    const int Ani_lanzar = 4;
    const int Ani_muerte = 5;
    const int Ani_salto = 6;
    const int Ani_disparo = 7;
    public float velo = 5;

    [HideInInspector]
    public bool isGrounded = true;
    int cont;
    bool muerto = false;
    public GameObject bullet;
    int direction = 1;
    public int velocity = 0, velSalto = 5, salto = 2;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(muerto){
            ChangeAnimation(Ani_muerte);
            if(Input.GetKeyDown("c")) muerto = false;
        }
        else{
            if(Input.GetKey(KeyCode.RightArrow)){
                if(Input.GetKey(KeyCode.DownArrow)) ChangeAnimation(Ani_Slide);
                else ChangeAnimation(Ani_correr);
                rb.velocity = new Vector2(velo, rb.velocity.y);
                sr.flipX = false;
                direction = 1;
            }
            else if(Input.GetKey(KeyCode.LeftArrow)){
                if(Input.GetKey(KeyCode.DownArrow)) ChangeAnimation(Ani_Slide);
                else ChangeAnimation(Ani_correr);
                rb.velocity = new Vector2(-velo, rb.velocity.y);
                sr.flipX = true;
                direction = -1;
            }
            else if(Input.GetKey("z")) ChangeAnimation(Ani_ataque);
            else if(Input.GetKey("x")) ChangeAnimation(Ani_lanzar);
            else if(Input.GetKeyDown("c")) muerto = true;
            else {
                ChangeAnimation(Ani_quieto);
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            if(Input.GetKeyDown(KeyCode.Space)){
                if(cont>0){
                    isGrounded = false;
                    rb.AddForce(new Vector2(0, velSalto), ForceMode2D.Impulse);
                    ChangeAnimation(Ani_salto);
                    cont--;
                }
            }
            if(Input.GetKeyDown("x")){
                var bulletPosition = transform.position + new Vector3(direction,0,0);
                var o = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                var c = o.GetComponent<BalaController>();
                ChangeAnimation(Ani_disparo);
                if(direction==-1) c.SetLeftDirection();
                else c.SetRightDirection();
            }
            //Debug.Log(puededobleSalto + "-" + dobleSalto + "-" + salto);
            if(gameManager.vidas<=0){
                Debug.Log("mori");
                muerto = true;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        cont=salto;
        if(other.gameObject.tag == "Zombie"){
            gameManager.PerderVidas();
        }
        
    } 
    void OnTriggerEnter2D(Collider2D other){
        
    }

    private void ChangeAnimation(int a){
        animator.SetInteger("Estado",a);
    }
}
