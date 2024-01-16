using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.VFX;

public class Movement : MonoBehaviour
{

    [Header("Controle")]
    private PlayerControls playerControls;
    
    
    [Header("Parametre du Player")]
    public float movespeed = 8f;

    public VisualEffect vfxRenderer;
    public float lightcd = 1f; 
    public GameObject torch;
    public float candrop;
    
    
    [Header(("Physics et armes"))]
    public Rigidbody _rb;
    public Vector2 moveDirection;
    public PlayerLight Pl;
    

    private Boolean canDash;
    private Boolean isDashing;
    private Boolean isAttacking;
    private Boolean isMoving;
    private Boolean canMove;
    private Boolean canRest;


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

    void Start()
    {
        canMove = true;
        candrop = lightcd;
    }

    private void Update()
    {
        if (canMove)
        {
            moveDirection = playerControls.Land.Move.ReadValue<Vector2>();
        }
        vfxRenderer.SetVector3("PlayerPosition",gameObject.transform.position);
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

    // Update is called once per frame
    void FixedUpdate()
    {
            _rb.velocity = new Vector3(moveDirection.x * movespeed,0f,moveDirection.y * movespeed);
    }

    void droplight()
    {
        Quaternion q = new Quaternion(0f,90f,90f,0f);
        Instantiate(torch,transform.position,q);
        candrop = lightcd;
    }

    void ReloadLight()
    {
        
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
