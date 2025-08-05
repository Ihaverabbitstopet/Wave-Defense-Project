using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedWorldPosition : MonoBehaviour
{
    [SerializeField] private Vector3 fixedWorldPosition;

    private void LateUpdate()
    {
        transform.position = fixedWorldPosition;
    }

    public void SetPosition(Vector3 newWorldPosition)
    {
        fixedWorldPosition = newWorldPosition;
    }
}
