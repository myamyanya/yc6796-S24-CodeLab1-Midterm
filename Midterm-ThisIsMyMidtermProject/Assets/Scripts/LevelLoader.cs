using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;

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
        JSONNode allLevelNode = JSONNode.Parse(fileContent);
        JSONNode levelNode = allLevelNode["lv0"];

        JSONArray levelArray = levelNode["ascii"].AsArray;

        // Horizontal
        for (int zLevelPosition = 0; zLevelPosition < levelArray.Count; zLevelPosition++)
        {
            string line = levelArray[zLevelPosition].ToString().ToUpper();

            char[] characters = line.ToCharArray();
            
            // Vertical
            for (int xLevelPosition = 0; xLevelPosition < characters.Length; xLevelPosition++)
            {
                char chara = characters[xLevelPosition];

                GameObject newObj = null;

                switch (chara)
                {
                    case 'P':
                        newObj = Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
                        break;
                    case 'B':
                        newObj = Instantiate(Resources.Load<GameObject>("Prefabs/BrickBlue"));
                        break;
                    case '-':
                        newObj = Instantiate(Resources.Load<GameObject>("Prefabs/BrickFloor"));
                        break;
                    default:
                        break;
                }

                if (newObj != null)
                {
                    newObj.transform.position = new Vector3(xLevelPosition, 0, -zLevelPosition);
                }
            }
        }
    }
}
