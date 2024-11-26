using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingPanel : MonoBehaviour
{
    private Animator animController;

    void Awake()
    {
        animController = this.gameObject.GetComponent<Animator>();
        if (animController == null)
        {
            Debug.Log("Animator Controller: Not found");
            return;
        }
        else
        {
            if (this.gameObject.name == "End Scene Final")
            {
                animController.SetTrigger("finalendPanel");
            }
            else
            {
                animController.SetTrigger("endPanel");
            }
        }
    }
}
