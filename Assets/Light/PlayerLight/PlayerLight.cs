using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class PlayerLight : MonoBehaviour
{
    
    public VisualEffect vfxRenderer;
    public float lightlife = 100f;
    public float overtimedmg;
    public float timetick;
    public float radius;
    private float t;
    

    private void OnEnable()
    {
        t = timetick;
        radius = vfxRenderer.GetFloat("RadiusLight");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t -=Time.deltaTime;
        if (t <=0f)
        {
            lightlife-= overtimedmg;
            //Debug.Log("Lum tick");
            t = timetick;
            Radiuscalculation();
        }
        if (lightlife == 0)
        {
            //reset temporaire jusqu'a l'ajout des 3 mobs
            lightlife = 100f;
            Debug.Log("Plus de lumière");
        }
        
    }

    // Recalcule le cercle de protection en fonction du pourcentage de lumière restant
    void Radiuscalculation()
    {
        vfxRenderer.SetFloat("RadiusLight",radius*(lightlife/100));
    }

    
    //Player Empower his light consum light faster but radius is stronger
    void Empower()
    {
        vfxRenderer.SetFloat("RadiusLight",8f);
        overtimedmg = overtimedmg * 2;
        // attendre 6 secondes  et remettre le cercle à son ancienne valeur plus baiser les dmg par seconde
    }


}
