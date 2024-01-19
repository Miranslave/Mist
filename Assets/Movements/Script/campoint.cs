using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class campoint : MonoBehaviour
{
    public Transform whattofollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new  Vector3(whattofollow.position.x*Time.deltaTime,gameObject.transform.position.y*Time.deltaTime,whattofollow.transform.position.z*Time.deltaTime);
    }
}
