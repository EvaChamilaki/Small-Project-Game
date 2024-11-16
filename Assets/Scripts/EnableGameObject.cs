using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableGameObject : MonoBehaviour
{
    public GameObject enableGO;

    void OnEnable()
    {
        StartCoroutine(enableSelfAfterSeconds(3.0f));
    }

    IEnumerator enableSelfAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        enableGO.SetActive(true);
    }
}
