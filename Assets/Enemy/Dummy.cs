using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public int health;

    public float respawntime;
    // Start is called before the first frame update
    void Start()
    {
        health = 3;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            this.gameObject.SetActive(true);
            health = 3;
        }
    }
    
    public void Damage(int dmg)
    {

            anim.SetTrigger("Hitted");
            health = health - dmg;
            if (health <= 0)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                damaged = true;
                Invoke(nameof(resetDamaged), 1f);
            }
         
        
    }
    
    public void resetDamaged()
    {
        damaged = false;
    }
}
