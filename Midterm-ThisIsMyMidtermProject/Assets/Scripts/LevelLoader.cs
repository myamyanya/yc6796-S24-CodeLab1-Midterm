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
    
    // For parenting and organizing all Instantiated objects
    private GameObject level;
    
    public int currenLevel = 0;

    public int CurrentLevel
    {
        get
        {
            return currenLevel;
        }
        set
        {
            currenLevel = value;
            LoadLevel();
        }
    }

    private int maxLevel = 3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
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
        // If there's no mound left in the level, load the next level
        GameObject[] moundLeft = GameObject.FindGameObjectsWithTag("Mound");

        Debug.Log(moundLeft.Length);
        
        if (moundLeft.Length <= 0)
        {
            if (currenLevel < maxLevel)
            {
                Debug.Log("Loading next level");

                CurrentLevel++;
            }
            else if (currenLevel >= maxLevel)
            {
                GameManager.instance.isInGame = false;
            }
        }
        
    }

    public void LoadLevel()
    {
        // Reset Level
        Destroy(level);
        level = new GameObject("Level Objects");
        
        JSONNode allLevelNode = JSONNode.Parse(fileContent);
        JSONNode levelNode = allLevelNode["lv" + currenLevel + ""];

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
                    case 'G':
                        newObj = Instantiate(Resources.Load<GameObject>("Prefabs/BrickGreen"));
                        break;
                    case 'I':
                        newObj = Instantiate(Resources.Load<GameObject>("Prefabs/BrickPink"));
                        break;
                    case 'Y':
                        newObj = Instantiate(Resources.Load<GameObject>("Prefabs/BrickYellow"));
                        break;
                    case 'M':
                        newObj = Instantiate(Resources.Load<GameObject>("Prefabs/Mound"));
                        break;
                    case '-':
                        newObj = Instantiate(Resources.Load<GameObject>("Prefabs/BrickFloor"));
                        break;
                    default:
                        break;
                }

                if (newObj != null)
                {
                    // Adding newObj to parent
                    newObj.transform.parent = level.transform;
                    
                    newObj.transform.position = new Vector3(xLevelPosition, 0, -zLevelPosition);
                }
            }
        }
    }
}
