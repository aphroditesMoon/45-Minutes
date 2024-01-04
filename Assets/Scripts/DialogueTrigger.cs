using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject[] dialogueObject;
    private bool _didIt0, _didIt1;

    public void StartDialogue()
    {
        if (_didIt0) return;
        dialogueObject[0].SetActive(true);
        _didIt0 = true;
    }

    public void StartDialogueTwo()
    {
        if (!_didIt1)
        {
            dialogueObject[1].SetActive(true);
            XAAXAXA();
        }
    }

    private void Update()
    {
        if (_didIt1 && !dialogueObject[1].activeInHierarchy)
        {
            endScreen.SetActive(true);
        }
    }

    private async void XAAXAXA()
    {
        await Task.Delay(1000);
        _didIt1 = true;
        await Task.Yield();
    }
}
