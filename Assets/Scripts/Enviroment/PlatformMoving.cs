using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    private Vector3 startPosition;

    Vector2 difference = Vector2.zero;

    private void OnMouseDown()
    {
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (transform.CompareTag("SlideableX"))
        {
            mousePosition.z = 0f;

            Vector3 newPosition = new Vector3(mousePosition.x, transform.position.y, transform.position.z);

            newPosition.x = Mathf.Clamp(newPosition.x, -1, 1);

            transform.position = newPosition;
        }

        if (transform.CompareTag("SlideableY"))
        {
            mousePosition.z = 0f;

            Vector3 newPosition = new Vector3(transform.position.x, mousePosition.y, transform.position.z);

            newPosition.y = Mathf.Clamp(newPosition.y, transform.position.y - newPosition.y, transform.position.y + newPosition.y);

            transform.position = newPosition;
        }
    }
}
