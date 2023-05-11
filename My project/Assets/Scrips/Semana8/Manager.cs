using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;

public class Manager : MonoBehaviour
{
    public Text puntosText;
    public int per;
    public int score;
    public int vidas;

    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
        per = 0;
        PrintPuntosInScreen();
    }

    private void Update()
    {
        
    }

    public void PrintPuntosInScreen()
    {
        puntosText.text = "Puntos: " + score;
    }
    public void Save()
    {
        var filePath = Application.persistentDataPath + "/per.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenWrite(filePath);
        else file = File.Create(filePath);

        Data data = new Data();
        data.Per = per;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
    }
    public void SaveGame()
    {
        var filePath = Application.persistentDataPath + "/per1.dat";
        FileStream file;
        if (File.Exists(filePath)) file = File.OpenWrite(filePath);
        else file = File.Create(filePath);

        GameData data = new GameData();
        data.Vidas = vidas;
        data.Score = score;
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
}
