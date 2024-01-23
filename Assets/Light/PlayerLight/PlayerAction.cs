using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;


public class PlayerAction : MonoBehaviour
{

    [Header("Controle")] private PlayerControls playerControls;
    public Animator anim;
    [Header("Drop Light action")] 
    public PlayerLight Pl;
    public GameObject torch;
    public float lightcd = 1f;
    public bool canDrop;
    public Movement playermovement;
   
    [Header("Heal action")]
    [SerializeField] private float heal;
    [SerializeField] private float healtickrate;
    
    [Header("Interraction")]
    [SerializeField] private float interractRange;
    [SerializeField] private float interractCD;
    [SerializeField] private bool canInterract;
    [SerializeField] private LayerMask interractMask;
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

    [SerializeField] public bool canRest;
    [SerializeField] private bool canHeal;
    

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
        //Debug.Log(playerControls!=null);
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        //candrop = lightcd;
        canHeal = true;
        canDrop = true;
        canInterract = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerControls.Land.LightOn.IsPressed()&&canDrop)
        {
            canDrop = false;
            droplight();
            Invoke(nameof(resetlight),lightcd);
        }

        if (playerControls.Land.Reload.IsPressed()&& canRest && Pl.lightlife <100f && canHeal)
        {
            // Test light heal 
            canHeal = false;
            Pl.Heal(Heal);
            Invoke(nameof(resetheal),Healtickrate);
        }

        if (playerControls.Land.Interract.IsPressed()&&canInterract)
        {
           Collider[] colliders = Physics.OverlapSphere(transform.position, interractRange,interractMask);
            foreach(var collider in colliders)
            {
                Beacon b = collider.gameObject.GetComponent<Beacon>();
                b.setActive(this.gameObject);
            }
            canInterract = false;
            Invoke(nameof(resetInterract),interractCD);
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
        canDrop = true;
    }

    void resetheal()
    {
        anim.SetBool("Attack",false);
        playermovement.canMove = true;
        canHeal = true;
    }

    void resetInterract()
    {
        canInterract = true;
    }


    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LightRefil"))
        {
            //Pl.infog = false;
            //canRest = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LightRefil"))
        {
            //Pl.infog = true;
            //canRest = false;
        }
    }
}
