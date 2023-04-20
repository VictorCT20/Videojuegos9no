using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaController : MonoBehaviour
{
    private int velocity = 5;
    public int dire = 1;
    private int veloY = 0;
    public bool primera = true;
    Rigidbody2D rb;
    Collider2D cl;
    public GameObject bullet;

    // Start is called before the first frame update
    public void SetRightDirection(){
        velocity = 5;
        veloY = 0;
        primera = true;
        dire = 1;
    }
    public void SetLeftDirection(){
        velocity = -5;
        veloY = 0;
        primera = true;
        dire = -1;
    }
    public void SetRightDirectionU(){
        velocity = 5;
        veloY = 1;
        primera = false;
    }
    public void SetRightDirectionD(){
        velocity = 5;
        veloY = -1;
        primera = false;
    }
    public void SetLeftDirectionU(){
        velocity = -5;
        primera = false;
        veloY = 1;
    }
    public void SetLeftDirectionD(){
        velocity = -5;
        primera = false;
        veloY = -1;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cl = GetComponent<Collider2D>();
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocity, veloY);
        explotar();
    } 
    public void explotar(){
        if(primera){
            if(Input.GetKeyDown("f")){
                var bullet1Position = transform.position + new Vector3(0,1,0);
                var o = Instantiate(bullet, bullet1Position, Quaternion.identity) as GameObject;
                var c = o.GetComponent<BalaController>();
                if(dire==-1) c.SetLeftDirectionU();
                else c.SetRightDirectionU();
                var bullet2Position = transform.position + new Vector3(0,-1,0);
                var o2 = Instantiate(bullet, bullet2Position, Quaternion.identity) as GameObject;
                var c2 = o2.GetComponent<BalaController>();
                if(dire==-1) c2.SetLeftDirectionD();
                else c2.SetRightDirectionD();
            }
        }
        
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.name !="Ninja") 
        {
            Destroy(this.gameObject);
        } 
    }
}
