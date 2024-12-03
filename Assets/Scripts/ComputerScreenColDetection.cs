using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerScreenColDetection : MonoBehaviour
{
    public GameObject interactableGO;
    public Outline outline;
    public GameObject inspectText;
    public bool isInRange;

    void Start()
    {
        outline = interactableGO.GetComponent<Outline>();
        DisableOutline();
        inspectText.SetActive(false);
        isInRange = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("InteractablePlayer"))
        {
            isInRange = true;
            EnableOutline();
            inspectText.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == ("InteractablePlayer"))
        {
            isInRange = false;
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
