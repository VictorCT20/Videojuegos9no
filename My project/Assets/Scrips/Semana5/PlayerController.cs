using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    AudioSource audioSource;
    public AudioClip bala;
    public AudioClip saltar;
    private GameManagerController gameManager;

    const int Ani_quieto = 0;
    const int Ani_correr = 1;
    const int Ani_ataque = 2;
    const int Ani_Slide = 3;
    const int Ani_lanzar = 4;
    const int Ani_muerte = 5;
    const int Ani_salto = 6;
    const int Ani_disparo = 7;
    public float velo = 5;
    int cont;
    bool muerto = false, salto = true, dobleSalto = false, puededobleSalto = false;
    public FootController pie;
    public FootController manod; 
    public FootController manoi;
    public GameObject bullet;
    int direction = 1;
    public float jumpForce = 3000f;
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerController>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            puedeSaltar();
            if(Input.GetKeyDown(KeyCode.Space) && salto){
                rb.AddForce(transform.up * jumpForce);
                pie.Jump(); manod.Jump(); manoi.Jump();
                ChangeAnimation(Ani_salto);
                
            }
            else if(Input.GetKeyDown(KeyCode.UpArrow) && puededobleSalto && dobleSalto){
                rb.AddForce(transform.up * jumpForce * 1.2f);
                audioSource.PlayOneShot(saltar);
                pie.Jump(); manod.Jump(); manoi.Jump();
                ChangeAnimation(Ani_salto);
                dobleSalto = false;
            }
            if(gameManager.balas > 0){
                if(Input.GetKeyDown("x")){
                    var bulletPosition = transform.position + new Vector3(direction,0,0);
                    var o = Instantiate(bullet, bulletPosition, Quaternion.identity) as GameObject;
                    var c = o.GetComponent<BalaController>();
                    ChangeAnimation(Ani_disparo);
                    if(direction==-1) c.SetLeftDirection();
                    else c.SetRightDirection();
                    gameManager.PerderBalas();
                }
            }
            else Debug.Log("No tienes balas");
            //Debug.Log(puededobleSalto + "-" + dobleSalto + "-" + salto);
            if(gameManager.vidas<=0){
                Debug.Log("mori");
                muerto = true;
            }
        }
        
    }
    private void puedeSaltar(){
        if(pie.CanJump() || manod.CanJump() || manoi.CanJump()) salto = true;
        else salto = false;

        if(pie.currentJumps==1 || manod.currentJumps==1 || manoi.currentJumps==1) puededobleSalto = true;
        else puededobleSalto = false;

    }
    void OnCollisionEnter2D(Collision2D other){
        //cont=salto;
        if(other.gameObject.tag == "Zombie"){
            gameManager.PerderVidas();
        }
        
    } 
    void OnTriggerEnter2D(Collider2D other){
        
        if(other.gameObject.tag == "LimiteZombie"){
            velo = velo + 1;
        }
        if(other.gameObject.tag == "MasBalas"){
            Debug.Log("hola");
            gameManager.GanarBalas();
            audioSource.PlayOneShot(bala);
            Destroy(other.gameObject);
        }
    }

    private void ChangeAnimation(int a){
        animator.SetInteger("Estado",a);
    }
}
