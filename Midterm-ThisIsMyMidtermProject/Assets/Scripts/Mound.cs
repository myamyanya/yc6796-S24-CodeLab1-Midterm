using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Mound : MonoBehaviour
{
    // For the time delay before the sprite changing
    public float changeSpriteDelay = 2.0f;

    public Sprite carrotSprite;
    public Sprite[] eggSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        carrotSprite = Resources.Load<Sprite>("Sprites/carrot");
        eggSprite = Resources.LoadAll<Sprite>("Sprites/EasterEggs");

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Is the dirt digged?
    private bool isDigged = false;
    
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log(isDigged);
        
        if (other.transform.gameObject.tag == "Player")
        {
            //Debug.Log("Dig?");

            if (!isDigged && Input.GetKey(KeyCode.F))
            {
                isDigged = true;
                audioSource.PlayOneShot(soundDigging);
                
                Invoke("ChangeSprite", changeSpriteDelay);
            }

            if (isDigged && Input.GetKey(KeyCode.G))
            {
                Debug.Log("Collected");
                audioSource.PlayOneShot(soundCollecting);

                if (isCarrot)
                {
                    GameManager.instance.CarrotAmt++;
                }
                else if (isEgg)
                {
                    GameManager.instance.EggAmt++;
                }
                
                Destroy(gameObject); 
            }
        }
    }
    
    // For score
    private bool isCarrot = false;
    private bool isEgg = false;
    
    // For playing sound
    public AudioClip soundDigging;
    public AudioClip soundCollecting;
    private AudioSource audioSource;
    
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
            
            isCarrot = true;
        }
        else
        {
            int egg = Random.Range(0, eggSprite.Length);
            GetComponent<SpriteRenderer>().sprite = eggSprite[egg];
            
            isEgg = true;
        }
        
        isDigged = true;
    }
}
