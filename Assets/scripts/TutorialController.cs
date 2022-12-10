using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialController : MonoBehaviour
{
    public Image tutoBackground;
    public CanvasGroup tutoText0;
    public CanvasGroup tutoText1;
    public CanvasGroup tutoText2;

    public void ShowTuto1()
    {
        tutoText0.DOFade(0, 1);
        tutoText1.DOFade(1, 1);
    }

    public void ShowTuto2()
    {
        tutoText1.DOFade(0, 1);
        tutoText2.DOFade(1, 1);
    }

    public void EndTuto()
    {
        tutoText2.DOFade(0, 1);
        tutoBackground.DOFade(0, 1);
    }
}
