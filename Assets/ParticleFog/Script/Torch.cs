using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Torch : MonoBehaviour
{
    public float deathtimer=10f;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        deathtimer -= Time.deltaTime;
        if (deathtimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}
