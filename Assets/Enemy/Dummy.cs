using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public int health;
    public Animator anim;
    
    public float respawntime;
    
    
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        anim = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Damage(int dmg)
    {
        Debug.Log("Hit");
            anim.SetTrigger("Hit");
            health = health - dmg;
            if (health <= 0)
            {
                this.gameObject.SetActive(false);
                Invoke(nameof(Respawn), respawntime);
            }
        
    }

    private void Respawn()
    {
        health = 3;
        this.gameObject.SetActive(true);
    }
    
 
}
