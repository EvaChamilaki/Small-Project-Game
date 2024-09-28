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
    private bool charControlActive = true;
    private bool hasClicked = false;

    private Camera active_camera;
    private Camera main_camera;
    private Camera computer_camera;
    private Camera emotions_camera;

    void Start()
    {
        canvasClick.SetActive(false);
        main_camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        computer_camera = GameObject.FindWithTag("ComputerCamera").GetComponent<Camera>();
        emotions_camera = GameObject.FindWithTag("EmotionsCamera").GetComponent<Camera>();

        computer_camera.enabled = false;
        emotions_camera.enabled = false;

        active_camera = main_camera;
    }

    // Update is called once per frame
    void Update()
    {

        if(charControlActive)
        {
            float hor = Input.GetAxis("Horizontal"); //negative for A and left arrow, positive for D and right arrow
            float ver = Input.GetAxis("Vertical"); //negative for S and down arrow, positive for W and up arrow

            Vector3 direction = new Vector3(hor, 0f, ver).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                charcontroller.Move(moveDirection.normalized * speed * Time.deltaTime);

            }
        }
        
        RaycastHit hit;
        Ray ray = active_camera.ScreenPointToRay(Input.mousePosition);
        if (active_camera == main_camera && Physics.Raycast(ray.origin, ray.direction, out hit, 5.0f)) //TODO: fix raycast distance (click issue)
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
            SwitchToComputerCamera();     
            hasClicked = true;
        }

        if(!charControlActive && Input.GetKeyDown(KeyCode.Space))
        {
            SwitchToMainCamera();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            SwitchToEmotionsCamera();

        }

        if (emotions_camera.enabled && hasClicked && Input.GetKeyDown(KeyCode.B))
        {
            SwitchToComputerCamera();
        }

    }
    void SwitchToComputerCamera() 
    {
        main_camera.enabled = false;
        computer_camera.enabled = true;
        emotions_camera.enabled = false;
        canvasClick.SetActive(false);

        charcontroller.enabled = false;
        charControlActive = false;

        active_camera = computer_camera;
    }
    void SwitchToMainCamera()
    {
        computer_camera.enabled = false;
        main_camera.enabled = true;
        emotions_camera.enabled = false;

        charcontroller.enabled = true;
        charControlActive = true;

        active_camera = main_camera;

    }

    void SwitchToEmotionsCamera()
    {
        main_camera.enabled = false;
        computer_camera.enabled = false;
        emotions_camera.enabled = true;
        canvasClick.SetActive(false);

        charcontroller.enabled = false;
        charControlActive = false;

        active_camera = emotions_camera;

    }
}
