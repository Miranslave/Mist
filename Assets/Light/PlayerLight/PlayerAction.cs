using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerAction : MonoBehaviour
{
    
    [Header("Controle")]
    private PlayerControls playerControls;

    [Header("Light actions")]
    public PlayerLight Pl;
    private Boolean canRest;
    public GameObject torch;
    public float lightcd = 1f;
    public float healtickrate = 2f;
    public bool canheal;
    public bool candrop;
    public float heal;

    [Header("Debuggage")] public VisualElement v;
    [SerializeField] private Label l;
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
        //candrop = lightcd;
        canheal =false;
        candrop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControls.Land.LightOn.IsPressed()&&candrop)
        {
            candrop = false;
            droplight();
            Invoke(nameof(resetlight),lightcd);
        }

        if (playerControls.Land.Reload.IsPressed()&& canRest && Pl.lightlife <100f)
        {
            Pl.lightlife += heal;
            canheal = false;
            Invoke(nameof(resetheal),healtickrate);
        }
        

    }
    void droplight()
    {
        Quaternion q = new Quaternion(0f,90f,90f,0f);
        Instantiate(torch,transform.position,q);
        Pl.lightlife -= 1f;
    }


    void resetlight()
    {
        candrop = true;
    }

    void resetheal()
    {
        canheal = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LightRefil"))
        {
            Pl.infog = false;
            canRest = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LightRefil"))
        {
            Pl.infog = true;
            canRest = false;
        }
    }
}
