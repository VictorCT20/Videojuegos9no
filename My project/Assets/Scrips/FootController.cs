using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootController : MonoBehaviour
{
    private bool onGround = false;
    public int currentJumps = 0;

    public bool CanJump() {
        return onGround;
    }

    public void Jump() {
        currentJumps++;
        onGround = false;
            
    }
    

    void OnCollisionEnter2D(Collision2D other) {
        onGround = true;
        currentJumps = 0;
    }
}
