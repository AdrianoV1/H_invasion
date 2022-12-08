using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MobilePlatform : MonoBehaviour
{
    public Transform platform;
    public Transform targetPosition;
    bool platformActivated;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !platformActivated)
        {
            platformActivated = true;
            StartCoroutine("WaitToMove");
        }
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(1);
        MovePlatform();
    }

    private void MovePlatform()
    {
        platform.DOMove(targetPosition.position, 10).SetEase(Ease.Flash);
    }
}
