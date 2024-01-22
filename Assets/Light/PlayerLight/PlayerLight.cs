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
    public Boolean infog;
    
    [Header("Debuggage")] public VisualElement v;
    [SerializeField] private Label l;

    private void Awake()
    {
        v = GameObject.FindWithTag("UI").GetComponent<UIDocument>().rootVisualElement;
        l = v.Q<Label>("Light");
        l.text = lightlife.ToString();
    }

    private void OnEnable()
    {
        t = timetick;
        radius = vfxRenderer.GetFloat("RadiusLight");
        infog = true;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (infog)
        {
            //Decrease the light by the time passing
            t -= Time.deltaTime;
            if (t <= 0f)
            {
                lightlife -= overtimedmg;
                //Debug.Log("Lum tick");
                t = timetick;
                l.text = lightlife.ToString();
                
            }
        }
        l.text = lightlife.ToString();
        if (lightlife == 0)
        {
            //reset temporaire jusqu'a l'ajout des 3 mobs
            lightlife = 100f;
            Debug.Log("Plus de lumière");
        }
        Radiuscalculation();
    }

    // Recalcule le cercle de protection en fonction du pourcentage de lumière restant
    void Radiuscalculation()
    {
        vfxRenderer.SetFloat("RadiusLight",radius*(lightlife/100));
    }

    
    //Player Empower his light consume light faster but radius is stronger and don't decrease can't be activated under 25 % light on/off toggle action
    void Empower()
    {
        vfxRenderer.SetFloat("RadiusLight",8f);
        //vfxRenderer.CreateVFXEventAttribute()
        overtimedmg = overtimedmg * 2;
        // attendre 6 secondes  et remettre le cercle à son ancienne valeur plus baiser les dmg par seconde
    }

    public void Heal(float healvalue)
    {
        lightlife += healvalue;
    }

}
