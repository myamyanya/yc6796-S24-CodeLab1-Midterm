using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mound : MonoBehaviour
{
    public float changeSpriteDelay = 1.0f;

    public Sprite carrotSprite;
    public Sprite[] eggSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        carrotSprite = Resources.Load<Sprite>("Sprites/carrot");
        eggSprite = Resources.LoadAll<Sprite>("Sprites/EasterEggs");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            Debug.Log("Dig?");

            if (Input.GetKeyDown(KeyCode.F))
            {
                Invoke("ChangeSprite", changeSpriteDelay);
            }
        }
    }

    private void ChangeSprite()
    {
        Debug.Log("Sprite changed.");

        // Random dig up a carrot -OR- an easter egg
        int lottery = Random.Range(0, 2);
        //lottery = 1; // For debugging

        // 0: carrot
        // 1: easter egg
        if (lottery == 0)
        {
            GetComponent<SpriteRenderer>().sprite = carrotSprite;
        }
        else
        {
            int egg = Random.Range(0, eggSprite.Length);
            GetComponent<SpriteRenderer>().sprite = eggSprite[egg];
        }
    }
}
