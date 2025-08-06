using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    public PlayerHealth playerHealth;                // Reference to your PlayerHealth script
    public RectTransform healthBarFillTransform;    // HealthBarFill RectTransform

    private float fullWidth;

    void Start()
    {
        if (healthBarFillTransform != null)
            fullWidth = healthBarFillTransform.rect.width;  // Use rect.width here
        else
            Debug.LogError("HealthBarFillTransform is not assigned!");
    }

    void Update()
    {
        if (playerHealth == null || healthBarFillTransform == null)
            return;

        float healthPercent = Mathf.Clamp01((float)playerHealth.GetCurrentHealth() / playerHealth.maxHealth);

        Vector2 size = healthBarFillTransform.sizeDelta;
        size.x = fullWidth * healthPercent;
        healthBarFillTransform.sizeDelta = size;
    }
}
