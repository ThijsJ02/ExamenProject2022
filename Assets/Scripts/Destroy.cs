using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float timeToDestroy;

    private void Awake()
    {
        StartCoroutine(destroyObject());   
    }

    IEnumerator destroyObject()
    {
        yield return new WaitForSeconds((float)timeToDestroy);
        Object.Destroy(this.gameObject);
    }
}
