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
    
    
    
    
    [Header(("Physics et armes"))]
    public Rigidbody _rb;
    public Vector2 moveDirection;
    
    

    private Boolean canDash;
    private Boolean isDashing;
    private Boolean isAttacking;
    private Boolean isMoving;
    private Boolean canMove;
    


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
    }

    private void Update()
    {
        if (canMove)
        {
            moveDirection = playerControls.Land.Move.ReadValue<Vector2>();
        }
        vfxRenderer.SetVector3("PlayerPosition",gameObject.transform.position);
       
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            _rb.velocity = new Vector3(moveDirection.x * movespeed,0f,moveDirection.y * movespeed);
    }


}
