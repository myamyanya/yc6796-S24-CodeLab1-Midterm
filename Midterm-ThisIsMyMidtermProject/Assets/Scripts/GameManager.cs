using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using SimpleJSON;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private float timer = 0;
    public int maxTime = 10;

    public bool isInGame = true;
    
    // For record data
    private string FILE_PATH;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            
            // ... don't destroy the holder
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private int carrotAmt;

    public int CarrotAmt
    {
        get
        {
            return carrotAmt;
        }
        set
        {
            carrotAmt = value;
            Debug.Log("Carrot " + carrotAmt);
        }
    }
    
    private int eggAmt;

    public int EggAmt
    {
        get
        {
            return eggAmt;
        }
        set
        {
            eggAmt = value;
            Debug.Log("Egg " + eggAmt);
        }
    }

    public TextMeshProUGUI displayCarrot;
    public TextMeshProUGUI displayEgg;
    
    // For record
    private int eggRecord;
    private int timerRecord;
    
    // Start is called before the first frame update
    void Start()
    {
        FILE_PATH = Application.dataPath + "/DATA/Data.txt";

        if (File.Exists(FILE_PATH))
        {
            // Reading data from the json file
            string fileContent = File.ReadAllText(FILE_PATH);
            
            JSONNode record = JSONNode.Parse(fileContent);
            eggRecord = record["egg"].AsInt;
            timerRecord = record["timer"].AsInt;
        }
        else
        {
            Debug.LogError("No data record found!");
        }
    }

    private bool isEndSceneLoaded = false;
    
    // Update is called once per frame
    void Update()
    {
        if (isInGame)
        {
            displayCarrot.text = "Carrot: " + CarrotAmt;
            displayEgg.text = "Easter Egg: " + EggAmt;

            // For game timer
            timer += Time.deltaTime;
        }
        else if (!isInGame && !isEndSceneLoaded)
        {
            EndGame();

            isEndSceneLoaded = true;
            
            displayEgg.text = "You found " + EggAmt + " Easter eggs within " + (int)timer + " seconds;";
            displayCarrot.text = "Last year, you found " + eggRecord + " eggs within " + timerRecord + " seconds!";
        }
    }

    private void EndGame()
    {
        Debug.Log("Game over!");

        SceneManager.LoadScene("End");
    }

    private void OnApplicationQuit()
    {
        JSONNode record = JSONNode.Parse("{}");

        record["egg"] = eggAmt;
        record["timer"] = (int)timer;

        string fileContent = record.ToString();
        File.WriteAllText(FILE_PATH, fileContent);
    }
}
