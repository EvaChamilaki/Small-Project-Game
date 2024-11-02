using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPerspectiveAnim : MonoBehaviour
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

        if(this.gameObject.tag == "victim")
        {
            animController.SetTrigger("victim_camera");
        }
        else if(this.gameObject.tag == "toxic")
        {
            animController.SetTrigger("toxic_camera");
        }
    }
}
