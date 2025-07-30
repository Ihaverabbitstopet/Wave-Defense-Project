using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAnimationHelper : MonoBehaviour
{
    public GameObject attackHitbox;

    public void EnableHitbox()
    {
        attackHitbox.SetActive(true);
    }

    public void DisableHitbox()
    {
        attackHitbox.SetActive(false);
    }
}
