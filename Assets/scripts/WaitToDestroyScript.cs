using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitToDestroyScript : MonoBehaviour
{
    public float timeToDestroy;

    void Start()
    {
        StartCoroutine("WaitToDestroy");
    }

    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
