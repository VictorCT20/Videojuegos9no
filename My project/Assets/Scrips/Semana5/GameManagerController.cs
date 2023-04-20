using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    public GameObject zombie;
    private float timer = 0.0f;
    private float timer2 = 3.0f;
    bool nu = true;
    void Update()
    {
        crearZombie();
    }
    void crearZombie(){
        if(nu){
            timer2 = 3.0f;
            nu = false;
        }
        
        timer += Time.deltaTime;
        //Debug.Log(timer);
        if(timer >= timer2){
            timer = 0.0f;
            var zombiePosition = new Vector3(10,-2.25f,0);
            var o = Instantiate(zombie, zombiePosition, Quaternion.identity) as GameObject;
            var c = o.GetComponent<ZombieController>();
            nu=true;
        }
    }
}
