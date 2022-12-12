using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ControlPrologo : MonoBehaviour
{
    public Text splashText;
    public int textIndex = 0;
    bool canPress;

    void Start()
    {
        splashText.DOFade(1, 1).OnComplete(EnableCanPress);
    }

    void EnableCanPress()
    {
        canPress = true;
    }

    private void Update()
    {
        if (canPress) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                textIndex++;
                canPress = false;
                NextText();
            }
        }
        
    }

    void NextText()
    {
        splashText.DOFade(0, .5f).OnComplete(A);
    }

    void A()
    {
        ChangeText();
        TextReappear();
    }

    void ChangeText()
    {
        switch (textIndex)
        {
            case 1:
                splashText.text = "La vida en el planeta se ha vuelto insostenible debido al desgaste de los recursos\ny contaminación provocados por el hombre.";
                break;
            case 2:
                splashText.text = "El reconocido científico y doctor, Over H y su alumna Elaine Leinheart,\nbuscan incansablemente el modo de regresar el planeta a su antiguo esplendor.";
                break;
            case 3:
                splashText.text = "Sin embargo, actitudes sospechosas del doctor hacen dudar a Elaine sobre quien es realmente su mentor.";
                break;
            case 4:
                splashText.text = "Elaine termina descubriendo que Over H en realidad es un alien que\nbusca terminar con la raza humana para traer a los suyos a poblar el planeta.";
                break;
            case 5:
                splashText.text = "Elaine se ha propuesto acabar con los planes de Over H,\npara lo cual se deberá enfrentarse tanto a él como a su hija androide Eve.";
                break;
            case 6:
                SceneManager.LoadScene("Tutorial");
                break;
        }
    }
    void TextReappear()
    {
        splashText.DOFade(1, .5f).OnComplete(EnableCanPress);
    }
}
