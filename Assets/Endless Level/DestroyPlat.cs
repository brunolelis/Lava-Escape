using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlat : MonoBehaviour
{
    private float secondsToDestroy = 40f;
    void Start()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(gameObject);
    }
}
