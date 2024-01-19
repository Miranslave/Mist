using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{

    
    public float healtickrate = .5f;
    public float heal;
    public GameObject player;
    public bool init;
    
    
    // Start is called before the first frame update
    void Start()
    {
        init = false;
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
        if (p)
        {
            PlayerLight pLight = p.GetComponent<PlayerLight>();
            PlayerAction pAction = p.GetComponent<PlayerAction>();
            if (pLight && pAction)
            {
                pLight.infog = false;
                pAction.Healtickrate = healtickrate;
                pAction.Heal = heal;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
            init = false;
        }
    }
}
