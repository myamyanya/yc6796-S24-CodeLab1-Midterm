using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
