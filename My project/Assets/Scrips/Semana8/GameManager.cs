using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Text puntosText;
    public Text vidasText;
    public int score = 0;
    public int vidas = 3;
    public int per;
    public GameObject per1;
    public GameObject per2;
    public GameObject zombie;
    private float timer = 0.0f;
    private float timer2 = 3.0f;
    bool nu = true;

    // Start is called before the first frame update
    void Start()
    {
        Load();
        Debug.Log("Hola" + per);
        if(per==0) {
            LoadGame();
            per1.transform.position = new Vector3(-10,-3.5f,0);
            Destroy(per2);
        }
        else {
            LoadGame2();
            per2.transform.position = new Vector3(-10,-3.5f,0);
            Destroy(per1);
        }
        PrintPuntosInScreen();
        PrintVidasInScreen();
    }

    private void Update()
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
        PrintPuntosInScreen();
        PrintVidasInScreen();
    }
    public void Atras()
    {
        if(per==0) SaveGame();
        else SaveGame2();
        SceneManager.LoadScene(3);
    }
    public void SaveGame()
    {
        var filePath = Application.persistentDataPath + "/per1.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenWrite(filePath);
        else file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = score;
        data.Vidas = vidas;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadGame()
    {
        var filePath = Application.persistentDataPath + "/per1.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenRead(filePath);
        else
        {
            UnityEngine.Debug.LogError("No se encontro el archivo");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        UnityEngine.Debug.Log(data.Score);
        score = data.Score;
        vidas = data.Vidas;
        //PrintPuntosInScreen();
        UnityEngine.Debug.Log("Carga1");

    }
    public void SaveGame2()
    {
        var filePath = Application.persistentDataPath + "/per2.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenWrite(filePath);
        else file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = score;
        data.Vidas = vidas;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
        var filePath = Application.persistentDataPath + "/per.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenRead(filePath);
        else
        {
            UnityEngine.Debug.LogError("No se encontro el archivo");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        Data data = (Data)bf.Deserialize(file);
        file.Close();

        per = data.Per;
        //PrintPuntosInScreen();
        UnityEngine.Debug.Log("Carga2");

    }
    public void LoadGame2()
    {
        var filePath = Application.persistentDataPath + "/per2.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenRead(filePath);
        else
        {
            UnityEngine.Debug.LogError("No se encontro el archivo");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData)bf.Deserialize(file);
        file.Close();

        UnityEngine.Debug.Log(data.Score);
        score = data.Score;
        vidas = data.Vidas;
        //PrintPuntosInScreen();
        UnityEngine.Debug.Log("Carga2");

    }

    public void empezarCero()
    {
        var filePath = Application.persistentDataPath + "/per1.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenWrite(filePath);
        else file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = 0;
        data.Vidas = 3;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public void empezarCero2()
    {
        var filePath = Application.persistentDataPath + "/per2.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenWrite(filePath);
        else file = File.Create(filePath);

        GameData data = new GameData();
        data.Score = 0;
        data.Vidas = 3;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public void GanarPuntos()
    {
        score += 1;
        PrintPuntosInScreen();
    }

    private void PrintPuntosInScreen()
    {
        puntosText.text = "Puntos: " + score;
    }
    public void PerderVidas()
    {
        score += 1;
        PrintPuntosInScreen();
    }

    private void PrintVidasInScreen()
    {
        vidasText.text = "Vidas: " + vidas;
    }
}
