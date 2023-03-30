using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;


    const int Ani_quieto = 0;
    const int Ani_correr = 1;
    const int Ani_ataque = 2;
    const int Ani_Slide = 3;
    const int Ani_lanzar = 4;
    const int Ani_muerte = 5;
    public int velo = 4;
    bool muerto = false;
    void Start()
    {
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
            }
            else if(Input.GetKey(KeyCode.LeftArrow)){
                if(Input.GetKey(KeyCode.DownArrow)) ChangeAnimation(Ani_Slide);
                else ChangeAnimation(Ani_correr);
                rb.velocity = new Vector2(-velo, rb.velocity.y);
                sr.flipX = true;
            }
            else if(Input.GetKey("z")){
                ChangeAnimation(Ani_ataque);
            }
            else if(Input.GetKey("x")){
                ChangeAnimation(Ani_lanzar);
            }
            else if(Input.GetKeyDown("c")){
                muerto = true;
            }   
            else {
                ChangeAnimation(Ani_quieto);
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        
    }

    private void ChangeAnimation(int a){
        animator.SetInteger("Estado",a);
    }
}
