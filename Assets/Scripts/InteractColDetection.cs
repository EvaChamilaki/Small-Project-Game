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
    public GameObject computerCollider;

    void Start()
    {
        outline = interactableGO.GetComponent<Outline>();
        DisableOutline();
        inspectText.SetActive(false);
        isInRange = false;
    }

    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                inspectText.SetActive(false);
                inspectCamera.SetActive(true);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("InteractablePlayer"))
        {
            isInRange = true;
            EnableOutline();
            inspectText.SetActive(true);
            if (computerCollider != null)
            {
                Physics.IgnoreCollision(other, computerCollider.GetComponent<Collider>(), true);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("InteractablePlayer"))
        {
            isInRange = false;
            DisableOutline();
            inspectText.SetActive(false);
            if (computerCollider != null)
            {
                Physics.IgnoreCollision(other, computerCollider.GetComponent<Collider>(), false);
            }
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
