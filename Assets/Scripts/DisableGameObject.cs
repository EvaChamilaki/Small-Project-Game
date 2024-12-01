using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameObject : MonoBehaviour
{
    void disableSelf()
    {
        this.gameObject.SetActive(false);
    }

    void disableParent()
    {
        this.transform.parent.gameObject.SetActive(false);
    }
}
