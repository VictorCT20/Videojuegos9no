using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Caminar();
    }
    private void Caminar(){
        if(Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(3,rb.velocity.y);
        }
        else if(Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-3,rb.velocity.y);
        }
        else{
            rb.velocity = new Vector2(0,rb.velocity.y);
        }

    }
}
