using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public class GameManagerController : MonoBehaviour
{
    public Text ZombiesText;
    public Text BalasText;
    public Text VidasText;
    public float vidas = 2;
    public float balas = 5;
    public int zombies = 0;
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
        Escribir();
    }
    private void Escribir(){
        PrintInScreenZombie();
        PrintInScreenLife();
        PrintInScreenMonedas();
    }
    public void GanarPuntos(){
        zombies ++;
        Escribir();
    }
    public void PerderVidas(){
        vidas -= 1f;
        Escribir();
    }
    public void GanarBalas(){
        balas += 5f;
        Escribir();
    }
    public void PerderBalas(){
        balas --;
        Escribir();
    }
    private void PrintInScreenZombie(){
        ZombiesText.text = "Zombies: " + zombies;
    }
    private void PrintInScreenLife(){
        VidasText.text = "Vidas: " + vidas;
    }
    private void PrintInScreenMonedas(){
        BalasText.text = "Balas: " + balas;
    }
}
