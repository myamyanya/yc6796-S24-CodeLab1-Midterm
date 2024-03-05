using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        displayCarrot.text = "Carrot: " + CarrotAmt;
        displayEgg.text = "Egg: " + EggAmt;
    }
}
