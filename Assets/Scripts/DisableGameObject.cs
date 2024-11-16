using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    void disableSelf()
    {
        this.gameObject.SetActive(false);
    }
}
