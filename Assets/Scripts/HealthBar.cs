﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    const string HP_CANVAS = "HP Canvas";
    Slider slider;
    Unit unit;
    
    private void Awake()
    {
        slider = GetComponent<Slider>();
       // unit = GetComponent<Unit>();
      
    }
    /*
    private void Update()
    {
        if(!unit)
        {
            Destroy(gameObject);
            return;
        }
        slider.value = unit.HealthPercent;
    }
    */
}
