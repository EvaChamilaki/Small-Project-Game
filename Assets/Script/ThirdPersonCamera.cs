using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField]
    public CharacterController charcontroller;
    public Transform camera;

    public float speed = 0.1f;


    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal"); //negative for A and left arrow, positive for D and right arrow
        float ver = Input.GetAxis("Vertical"); //negative for S and down arrow, positive for W and up arrow

        Vector3 direction = new Vector3(hor, 0f, ver).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            charcontroller.Move(moveDirection.normalized * speed * Time.deltaTime);
            
        }
       


    }
}
