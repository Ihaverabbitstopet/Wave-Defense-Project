using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrictionChanger : MonoBehaviour
{
    public PhysicMaterial customPhysicMaterial; // Assign your Physic Material in the Inspector

    void Start()
    {
        // Get the Collider component
        Collider myCollider = GetComponent<Collider>();

        // Ensure a Collider and a PhysicMaterial are present
        if (myCollider != null && customPhysicMaterial != null)
        {
            // Assign the PhysicMaterial to the collider
            myCollider.material = customPhysicMaterial;

            // You can also directly change the friction values at runtime
            // myCollider.material.dynamicFriction = 0.5f;
            // myCollider.material.staticFriction = 0.6f;
        }
    }

    // Example of changing friction dynamically
    public void SetFriction(float newDynamicFriction, float newStaticFriction)
    {
        Collider myCollider = GetComponent<Collider>();
        if (myCollider != null && myCollider.material != null)
        {
            myCollider.material.dynamicFriction = newDynamicFriction;
            myCollider.material.staticFriction = newStaticFriction;
        }
    }
}

//Testing commit
