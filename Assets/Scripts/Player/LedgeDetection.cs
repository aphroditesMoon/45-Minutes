using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] LayerMask mask;
    public bool ledgeDetect;

    private bool _canDetectable;

    private void Update()
    {
        if (_canDetectable)
            ledgeDetect = Physics2D.OverlapCircle(transform.position, radius, mask);

        Debug.Log(ledgeDetect);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            _canDetectable = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            _canDetectable = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
