using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class NinjaBController : MonoBehaviour
{
    public int velocity = 0, velSalto = 5, salto = 2;
    public GameObject Kunai;

    [HideInInspector]
    public bool onLadder = false;
    public float climbSpeed = 3;
    public float exitHop = 3;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    Collider2D cl;
    AudioSource audioSource;
    public ManagerController gameManager;
    
    public TilemapCollider2D plataform;

    [HideInInspector]
    public bool isGrounded = true;

    [HideInInspector]
    public bool usingLadder = false;
    public const int escena1 = 1;

    const int Ani_quieto = 0;
    const int Ani_correr = 1;
    const int Ani_ataque = 2;
    const int Ani_Slide = 3;
    const int Ani_lanzar = 4;
    const int Ani_muerte = 5;
    const int Ani_salto = 6;
    const int Ani_disparo = 7;
    int direction = 1;
    bool cambio = false;
    int cont;
    Vector3 lastCheckpointPosition;
    void Start()
    {
        Debug.Log("Iniciando script de player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cl = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-velocity, rb.velocity.y);
        Movimiento();
        if(Input.GetKeyDown("x")){
            Disparar();
        }
    }
    void Movimiento(){

        if(Input.GetKeyDown(KeyCode.Space)) 
            Jump();
        
        if(Input.GetKeyDown(KeyCode.LeftArrow))
            WalkToLeft();
        else if(Input.GetKeyUp(KeyCode.LeftArrow))
            StopWalk();
        else if(Input.GetKeyDown(KeyCode.RightArrow))
            WalkToRight();
        else if(Input.GetKeyUp(KeyCode.RightArrow))
            StopWalk();

        
    }
    public void WalkToLeft()
    {
        velocity = 5;
        sr.flipX = true;
        ChangeAnimation(Ani_correr);
        direction = -1;
    }
    public void WalkToRight()
    {
        velocity = -5;
        sr.flipX = false;
        ChangeAnimation(Ani_correr);
        direction = 1;
    }
    public void StopWalk()
    {
        velocity = 0;
        ChangeAnimation(Ani_quieto);
        rb.velocity = new Vector2(0, rb.velocity.y);
    }
    public void Jump()
    {
        if(cont>0){
            isGrounded = false;
            rb.AddForce(new Vector2(0, velSalto), ForceMode2D.Impulse);
            ChangeAnimation(Ani_salto);
            cont--;
        }

    }
    public void CambioEscena()
    {
        if(gameManager.llave >= 1 && cambio && gameManager.zombies >= 5){
            SceneManager.LoadScene(escena1);
        }
        else Debug.Log("Condiciones no conseguidas");
    }
    public void Disparar(){
        ChangeAnimation(Ani_lanzar);
        var bulletPosition = transform.position + new Vector3(direction,0,0);
        var o = Instantiate(Kunai, bulletPosition, Quaternion.identity) as GameObject;
        var c = o.GetComponent<BalaController>();
        var t = c.GetComponent<SpriteRenderer>();
        if(direction==-1){
            t.transform.Rotate(0, 0, 90); 
            c.SetLeftDirection();
        } 
        else {
            t.transform.Rotate(0, 0, -90); 
            c.SetRightDirection();
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        cont=salto;
        if(other.gameObject.name == "Llave"){
            Debug.Log("ctmre");
            gameManager.CogerLlave();
            gameManager.Escribir();
            Destroy(other.gameObject);
        }
    } 
    void OnTriggerEnter2D(Collider2D other)//para reconocer el checkponit(transparente)
    {
        
    }
    private void ChangeAnimation(int a){
        animator.SetInteger("Estado", a);
    }
    private void OnTriggerStay2D(Collider2D other){
        /*if(other.CompareTag("escalera")){
            if(Input.GetAxisRaw("Vertical") != 0){
                rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed);
                rb.gravityScale = 0;
                onLadder = true;
                plataform.enabled = false;
            }
            else if(Input.GetAxisRaw("Vertical") == 0 && onLadder){
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }*/
        if(other.gameObject.name == "portal"){
            cambio = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        /*if(other.CompareTag("escalera") && onLadder){
            rb.gravityScale = 1;
            onLadder = false;
            plataform.enabled = true;

            if(!isGrounded) 
                rb.velocity = new Vector2(rb.velocity.x, exitHop); 
        }*/
        if(other.gameObject.name == "portal"){
            cambio = false;
        }
    }
}
