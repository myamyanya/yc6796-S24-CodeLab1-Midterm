using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    
    // File path
    private string FIRE_PATH;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // For reading all text from the .txt file
    private string fileContent;
    
    // Start is called before the first frame update
    void Start()
    {
        FIRE_PATH = Application.dataPath + "/Levels/Levels.txt";
        if (File.Exists(FIRE_PATH))
        {
            fileContent = File.ReadAllText(FIRE_PATH);
        }
        
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        
    }
}
