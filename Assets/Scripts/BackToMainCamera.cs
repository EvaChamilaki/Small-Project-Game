using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackToMainCamera : MonoBehaviour
{
    public GameObject inspectCamera;
    public GameObject mainCamera;

    public void BackToMainCam()
    {
        mainCamera.SetActive(true);
        inspectCamera.SetActive(false);
    }
}
