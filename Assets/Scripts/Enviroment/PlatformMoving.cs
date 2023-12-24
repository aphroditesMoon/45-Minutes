using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    private Transform targetTransform;
    private Vector3 startPosition;
    private bool isDragging = false;
    private bool canMoveX = false;
    private bool canMoveY = false;
    public float moveRange = 1.0f;
    public CinemachineVirtualCamera virtualCamera;
    public Camera mainCamera; // Normal kamera referansı

    void Start()
    {
        targetTransform = transform.parent.GetChild(0);
        startPosition = targetTransform.position;

        // Tag'e göre hareket yeteneğini belirle
        if (transform.CompareTag("SlideableX"))
        {
            canMoveX = true;
        }
        else if (transform.CompareTag("SlideableY"))
        {
            canMoveY = true;
        }

        // Cinemachine Virtual Camera referansını otomatik olarak al
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

        // Eğer normal kamera kullanılıyorsa, referansını al
        mainCamera = GameObject.FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            if (IsMouseOverTarget(mousePosition))
            {
                isDragging = true;
            }
        }
        else if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            Vector3 clampedPosition = targetTransform.position;

            if (canMoveX)
            {
                clampedPosition.x = Mathf.Clamp(mousePosition.x, startPosition.x - moveRange, startPosition.x + moveRange);
            }
            else if (canMoveY)
            {
                clampedPosition.y = Mathf.Clamp(mousePosition.y, startPosition.y - moveRange, startPosition.y + moveRange);
            }

            targetTransform.position = clampedPosition;
            UpdateVirtualCameraPosition(clampedPosition); // Kamera pozisyonunu güncelle
        }
        else if (isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void UpdateVirtualCameraPosition(Vector3 position)
    {
        // Eğer Cinemachine Virtual Camera kullanılıyorsa, kamera pozisyonunu güncelle
        if (virtualCamera != null)
        {
            virtualCamera.transform.position = new Vector3(position.x, position.y, virtualCamera.transform.position.z);
        }
    }

    private bool IsMouseOverTarget(Vector3 mousePosition)
    {
        Collider2D collider = targetTransform.GetComponent<Collider2D>();
        return collider.bounds.Contains(mousePosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.transform.SetParent(null);
    }
}
