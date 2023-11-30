using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    private Transform targetTransform; // Sadece bu scriptin etkileyeceği child objenin transform'u
    private Vector3 startPosition;
    private bool isDragging = false;
    private Vector2 offset;
    public float moveRange = 1.0f; // Hareket aralığı

    void Start()
    {
        // Target objeyi parent içindeki sırasına göre al
        targetTransform = transform.parent.GetChild(0); // 0, hedef child objenin sıra indeksi olabilir, uygun indeksi belirtin
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
                float clampedX = Mathf.Clamp(mousePosition.x + offset.x, startPosition.x - moveRange, startPosition.x + moveRange);
                targetTransform.position = new Vector3(clampedX, startPosition.y, startPosition.z);
            }

            if (transform.CompareTag("SlideableY"))
            {
                float clampedY = Mathf.Clamp(mousePosition.y + offset.y, startPosition.y - moveRange, startPosition.y + moveRange);
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
        // Mouse bırakıldığında sınırları sıfırla
        if (transform.CompareTag("SlideableX"))
        {
            float clampedX = Mathf.Clamp(targetTransform.position.x, -moveRange, moveRange);
            targetTransform.position = new Vector3(clampedX, startPosition.y, startPosition.z);
        }

        if (transform.CompareTag("SlideableY"))
        {
            float clampedY = Mathf.Clamp(targetTransform.position.y, -moveRange, moveRange);
            targetTransform.position = new Vector3(startPosition.x, clampedY, startPosition.z);
        }
    }
}
