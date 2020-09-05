﻿using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    protected Slider slider => GetComponent<Slider>();

    protected virtual void Start()
    {
        // Debug.LogWarning("Slider: " + slider + ", exists: " + (bool)slider);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
}
