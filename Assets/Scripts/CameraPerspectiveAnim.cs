using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPerspectiveAnim : MonoBehaviour
{
    private Animator animController;
    public GameObject _camera;
    public GameObject _character;
    public GameObject clickImg;

    void Awake()
    {
        animController = this.gameObject.GetComponent<Animator>();
        if (animController == null)
        {
            Debug.Log("Animator Controller: Not found");
            return;
        }

        if (this.gameObject.tag == "victim")
        {
            animController.SetTrigger("victim_camera");
        }
        else if (this.gameObject.tag == "toxic")
        {
            animController.SetTrigger("toxic_camera");
        }
        else
        {
            animController.SetTrigger("mw_panel");
        }
    }

    void ChangeCameras()
    {
        this.gameObject.SetActive(false);
        _camera.SetActive(true);
    }

    void ChangeCameraAfterAnim()
    {
        this.gameObject.SetActive(false);
        _camera.SetActive(true);
        _character.SetActive(true);
        clickImg.SetActive(true);
    }
}
