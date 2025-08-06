using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarUI : MonoBehaviour
{
    private EnemyHealth enemyHealth;              // Reference to the enemy's health script
    public RectTransform fill;                    // The fill bar (green/red)

    private float fullWidth;

    void Start()
    {
        // Try to find the EnemyHealth component in the parent
        enemyHealth = GetComponentInParent<EnemyHealth>();
        if (enemyHealth == null)
        {
            Debug.LogError("EnemyHealth not found in parent!");
            enabled = false;
            return;
        }

        if (fill != null)
        {
            fullWidth = fill.rect.width;
        }
        else
        {
            Debug.LogError("Health bar 'fill' RectTransform is not assigned.");
            enabled = false;
        }
    }

    void Update()
    {
        if (enemyHealth == null || fill == null) return;

        float percent = Mathf.Clamp01(enemyHealth.GetCurrentHealth() / enemyHealth.GetMaxHealth());

        Vector2 size = fill.sizeDelta;
        size.x = fullWidth * percent;
        fill.sizeDelta = size;
    }
}
