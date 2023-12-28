using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{
    private CheckPointManager _checkPointManager;

    private void Start()
    {
        _checkPointManager = GameObject.FindGameObjectWithTag("CheckPointManager").GetComponent<CheckPointManager>();
        transform.position = _checkPointManager.lastCheckPointPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Death")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
