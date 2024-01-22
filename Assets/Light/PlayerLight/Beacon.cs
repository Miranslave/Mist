using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{

    
    public float healtickrate = .5f;
    public float heal;
    public GameObject player;
    public GameObject fire;

    public bool Activated;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.CompareTag("Player"))
            {
                SetPlayerValue(other.gameObject);
            }
    }
    

    private void SetPlayerValue(GameObject p)
    {
        if (Activated)
        {
            if (p)
            {
                PlayerLight pLight = p.GetComponent<PlayerLight>();
                PlayerAction pAction = p.GetComponent<PlayerAction>();
                if (pLight && pAction)
                {
                    pLight.infog = false;
                    pAction.canRest = true;
                    pAction.Healtickrate = healtickrate;
                    pAction.Heal = heal;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerAction pA = other.gameObject.GetComponent<PlayerAction>();
                PlayerLight pLight = other.gameObject.GetComponent<PlayerLight>();
                pA.canRest = false;
                pLight.infog = true;
                player = null;
            }
    }

    public void setActive(GameObject g)
    {
        Activated = true;
        if (g.CompareTag("Player"))
        {
            player = g;
            PlayerLight pLight = player.gameObject.GetComponent<PlayerLight>();
            pLight.infog = false;
        }
        fire.SetActive(true);
        
    }

 
    
}
