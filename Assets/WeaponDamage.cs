using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public float damage = 25f;
    public bool ignoresDefense = false;

    public float GetDamage()
    {
        return damage;
    }

    public bool IgnoresDefense()
    {
        return ignoresDefense;
    }
}
