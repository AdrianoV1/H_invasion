using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialog : MonoBehaviour
{
/*
    [Header("Texto")]
    [SerializeField] private GameObject text;
    [SerializeField] private TextMeshProUGUI DialogueText;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed = 0.5f;
    private int index;
    private Eve eve;
    


    // Start is called before the first frame update
    void Start()
    {
        DialogueText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (DialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                DialogueText.text = lines[index];
                eve.enabled = true;
            }
        }
    }

    //TEXTOS

    private void StarDialogue()
    {
        StartCoroutine(WriteText());
    }

    IEnumerator WriteText()
    {

        foreach (char letter in lines[index].ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }

    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            DialogueText.text = string.Empty;
            StartCoroutine(WriteText());
        }
        else
        {
            text.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            text.SetActive(true);
            StarDialogue();
            
        }
    }
*/
}
