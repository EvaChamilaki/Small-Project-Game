using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionsCamera : MonoBehaviour
{
    public Transform faceTarget;
    public float distance = 1.0f;
    public float height = 0.0f;
    public Vector3 forwardOffset = new Vector3(0, 0, -1);


    void LateUpdate()
    {
        if (faceTarget != null)
        {
            Vector3 cameraPosition = faceTarget.position + faceTarget.forward * distance;

            transform.position = new Vector3(cameraPosition.x, faceTarget.position.y + height, cameraPosition.z);
           
            transform.LookAt(faceTarget);
        }
    }
}

