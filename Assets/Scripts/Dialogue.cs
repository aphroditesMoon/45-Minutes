using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    [SerializeField] public Image porter;
    [SerializeField] public Image[] porters;

    private int index;
    
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialouge();
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialouge()
    {
        index = 0;
        StartCoroutine(Typeline());

    }

    IEnumerator Typeline()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            porter.sprite = porter.sprite == porters[0].sprite ? porters[1].sprite : porters[0].sprite;
            StartCoroutine(Typeline());
        }
        else
        {
            gameObject.SetActive(false);
            textComponent.text = lines[0];
        }
    }
}
