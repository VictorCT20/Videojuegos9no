using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ManagerController : MonoBehaviour
{
    public Text ZombiesText;
    public Text LlaveText;
    public Text VidasText;
    public float vidas = 2;
    public int llave = 0;
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
    public void Escribir(){
        PrintInScreenZombie();
        PrintInScreenLife();
        PrintInScreenKey();
    }
    public void GanarPuntos(){
        zombies ++;
        Escribir();
    }
    public void PerderVidas(){
        vidas -= 1f;
        Escribir();
    }
    public void CogerLlave(){
        llave += 1;
        Escribir();
    }
    private void PrintInScreenZombie(){
        ZombiesText.text = "Zombies: " + zombies;
    }
    private void PrintInScreenLife(){
        VidasText.text = "Vidas: " + vidas;
    }
    private void PrintInScreenKey(){
        if(llave==0) LlaveText.text = "Lave: No";
        else LlaveText.text = "Lave: Si";
    }
}
