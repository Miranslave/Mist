using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerAction : MonoBehaviour
{

    [Header("Controle")] private PlayerControls playerControls;
    public Animator anim;
    [Header("Drop Light action")] 
    public PlayerLight Pl;
    public GameObject torch;
    public float lightcd = 1f;
    public bool candrop;
   
    [Header("Heal action")]
    [SerializeField] private float heal;
    [SerializeField] private float healtickrate;

    public float Heal
    {
        get => heal;
        set => heal = value;
    }

    public float Healtickrate
    {
        get => healtickrate;
        set => healtickrate = value;
    }

    [SerializeField] private Boolean canRest;
    [SerializeField] private bool canheal;

    [Header("Debuggage")] 
    public VisualElement v;
    [SerializeField] private Label l;
    private void Awake()
    {

        playerControls = new PlayerControls();
        anim = GetComponentInChildren<Animator>();
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
        canheal = true;
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

        if (playerControls.Land.Reload.IsPressed()&& canRest && Pl.lightlife <100f&& canheal)
        {
            // Test light heal 
            canheal = false;
            Pl.Heal(Heal);
            Invoke(nameof(resetheal),Healtickrate);
        }

        if (playerControls.Land.Attack.IsPressed())
        {
            anim.SetBool("Attack",true);
        }
        else
        {
            anim.SetBool("Attack",false);
        }
    }
    void droplight()
    {
        Quaternion q = new Quaternion(0f,90f,90f,0f);
        Instantiate(torch,transform.position,q);
        //  negate 1 from player Lightsource might change it to a variable later on 
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
