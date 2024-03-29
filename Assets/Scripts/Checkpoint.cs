using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckPointManager _checkPointManager;

    private void Start()
    {
        _checkPointManager = GameObject.FindGameObjectWithTag("CheckPointManager").GetComponent<CheckPointManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _checkPointManager.lastCheckPointPos = transform.position;
        }
    }
}
