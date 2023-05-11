using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelPerController : MonoBehaviour
{
    public const int monedas = 2;
    public Sprite Per1;
    public Sprite Per2;
    SpriteRenderer sr;
    int selec = 0;
    private Manager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<Manager>();
        sr = GetComponent<SpriteRenderer>();
        gameManager.LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangePer(){
        if(selec==0) {
            sr.sprite = Per1;
            selec = 1;
            gameManager.LoadGame2();
        } else {
            sr.sprite = Per2;
            selec = 0;
            gameManager.LoadGame();
        }
        gameManager.PrintPuntosInScreen();
    }
    public void SelecPer1(){
        Debug.Log("Selec: - "+selec);
        gameManager.per = selec;
        gameManager.Save();
        SceneManager.LoadScene(monedas);
    }
    public void SelecPer2(){
        if(gameManager.score>=5){
            gameManager.per = selec;
            gameManager.Save();
            SceneManager.LoadScene(monedas);
        }
    }
    public void SelecPer3(){
        if(gameManager.score>=10){
            gameManager.per = selec;
            gameManager.Save();
            SceneManager.LoadScene(monedas);
        }
    }
}
