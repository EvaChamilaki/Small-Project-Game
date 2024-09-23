using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ThirdPersonCamera : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject canvasClick;

    [Header("Character Control")]
    public CharacterController charcontroller;
    public Transform camera;

    public float speed = 0.1f;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVel;

    private bool computerHit = false;

    private Camera main_camera;
    private Camera computer_camera;

    void Start()
    {
        canvasClick.SetActive(false);
        main_camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        computer_camera = GameObject.FindWithTag("ComputerCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Horizontal"); //negative for A and left arrow, positive for D and right arrow
        float ver = Input.GetAxis("Vertical"); //negative for S and down arrow, positive for W and up arrow

        Vector3 direction = new Vector3(hor, 0f, ver).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            charcontroller.Move(moveDirection.normalized * speed * Time.deltaTime);
            
        }


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, 4.0f))
        {
            if (hit.transform.CompareTag("ComputerScreen"))
            {
                computerHit = true;
                canvasClick.SetActive(true);

            }
        }

        else
        {
            computerHit = false;
            canvasClick.SetActive(false);
        }

        if(computerHit && Input.GetMouseButtonDown(0))
        {
            main_camera.enabled  = false;
            computer_camera.enabled = true;
            canvasClick.SetActive(false );
        }


      



    }
}
