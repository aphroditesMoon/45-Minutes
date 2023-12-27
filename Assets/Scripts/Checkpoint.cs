using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public PlayerNewController PlayerNewController;
    [NonSerialized] public Vector2 lastPosition;
    [NonSerialized] public Vector2[] checkPoints;

    private void Start()
    {
        lastPosition = PlayerNewController.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            lastPosition = other.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            lastPosition = other.transform.position;
        }
    }
}
