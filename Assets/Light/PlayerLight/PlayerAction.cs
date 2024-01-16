using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAction : MonoBehaviour
{
    
    [Header("Controle")]
    private PlayerControls playerControls;

    [Header("Light actions")]
    public PlayerLight Pl;
    private Boolean canRest;
    public GameObject torch;
    public float lightcd = 1f;
    public float candrop;
    
    
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        playerControls.Enable();
        Debug.Log(playerControls!=null);
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        candrop = lightcd;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControls.Land.LightOn.IsPressed()&&candrop<=0)
        {
            droplight();
        }

        if (playerControls.Land.Reload.IsPressed()&& canRest)
        {
            Debug.Log("Rechargement de la lanterne");
        }

        if (candrop > 0)
        {
            candrop -= Time.deltaTime;
        }
    }
    void droplight()
    {
        Quaternion q = new Quaternion(0f,90f,90f,0f);
        Instantiate(torch,transform.position,q);
        candrop = lightcd;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LightRefil"))
        {
            canRest = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LightRefil"))
        {
            canRest = false;
        }
    }
}
