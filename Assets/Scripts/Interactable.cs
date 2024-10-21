using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    //Outline outline;
    public bool isInRange;
    public UnityEvent onInteraction;
    public GameObject inspectText;


    //void Start()
    //{
    //    //outline = GetComponent<Outline>();
    //    DisableOutline();
    //}

    void Update()
    {
        if (isInRange)
        {
            Debug.Log("edw mwre");
            if (Input.GetMouseButtonDown(0))
            {
                Interact();
            }
        }
    }

    public void Interact()
    {
        onInteraction.Invoke();
    }

    public void EnableInspectText()
    {
        inspectText.SetActive(true);
    }

    //public void DisableOutline()
    //{
    //    outline.enabled = false;
    //}

    //public void EnableOutline()
    //{
    //    outline.enabled = true;
    //}

    private void onTriggerEnter(Collider other)
    {
        Debug.Log("object in range" + other.gameObject.tag);
        if (other.gameObject.tag == ("InteractablePlayer"))
        {
            isInRange = true;
            Debug.Log("Player in range");
            //EnableOutline();
        }
    }

    private void onTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("InteractablePlayer"))
        {
            isInRange = false;
            Debug.Log("Player out of range");
            //DisableOutline();
        }
    }
}
