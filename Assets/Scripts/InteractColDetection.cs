using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractColDetection : MonoBehaviour
{
    public GameObject interactableGO;
    public Outline outline;
    public GameObject inspectText;
    public bool isInRange;
    public GameObject inspectCamera;

    void Start()
    {
        outline = interactableGO.GetComponent<Outline>();
        DisableOutline();
        inspectText.SetActive(false);
    }

    void Update()
    {
        if (isInRange)
        {
            if (Input.GetMouseButtonDown(0))
            {
                inspectText.SetActive(false);
                inspectCamera.SetActive(true);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        isInRange = true;

        if (other.gameObject.tag == ("InteractablePlayer"))
        {
            Debug.Log("Player in range");
            EnableOutline();
            inspectText.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        isInRange = false;

        if (other.gameObject.tag == ("InteractablePlayer"))
        {
            Debug.Log("Player out of range");
            DisableOutline();
            inspectText.SetActive(false);
        }
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }
}
