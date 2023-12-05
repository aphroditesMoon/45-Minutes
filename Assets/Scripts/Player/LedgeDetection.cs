using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] LayerMask mask;
    [SerializeField] private bool ledgeDetect;

    private void Update()
    {
        ledgeDetect = Physics2D.OverlapCircle(transform.position, radius, mask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
