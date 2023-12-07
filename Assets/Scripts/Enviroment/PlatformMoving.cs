using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    private Transform targetTransform;
    private Vector3 startPosition;
    private bool isDragging = false;
    private Vector2 offset;
    public float moveRange = 1.0f;

    void Start()
    {
        targetTransform = transform.parent.GetChild(0);
    }

    private void OnMouseDown()
    {
        if (!isDragging)
        {
            startPosition = targetTransform.position;
            offset = (Vector2)targetTransform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            if (transform.CompareTag("SlideableX"))
            {
                // Calculate the new X position within the range of -1 to 1 relative to the parent's position
                float clampedX = Mathf.Clamp(mousePosition.x + offset.x, targetTransform.parent.position.x - moveRange, targetTransform.parent.position.x + moveRange);
                targetTransform.position = new Vector3(clampedX, startPosition.y, startPosition.z);
            }

            if (transform.CompareTag("SlideableY"))
            {
                float clampedY = Mathf.Clamp(mousePosition.y + offset.y, targetTransform.parent.position.y - moveRange, targetTransform.parent.position.y + moveRange);
                targetTransform.position = new Vector3(startPosition.x, clampedY, startPosition.z);
            }
        }
    }   

    private void OnMouseUp()
    {
        isDragging = false;
        ResetLimits();
    }

    private void ResetLimits()
    {
        if (transform.CompareTag("SlideableX"))
        {
            float clampedX = Mathf.Clamp(targetTransform.position.x, targetTransform.parent.position.x - moveRange, targetTransform.parent.position.x + moveRange);
            targetTransform.position = new Vector3(clampedX, startPosition.y, startPosition.z);
        }

        if (transform.CompareTag("SlideableY"))
        {
            float clampedY = Mathf.Clamp(targetTransform.position.y, targetTransform.parent.position.y - moveRange, targetTransform.parent.position.y + moveRange);
            targetTransform.position = new Vector3(startPosition.x, clampedY, startPosition.z);
        }
    }
}
