using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CustomEventTrigger : EventTrigger
{
    public override void OnPointerEnter(PointerEventData data)
    {
        ChangeCursor.instance.ClickableCursor();
    }

    public override void OnPointerExit(PointerEventData data)
    {
        ChangeCursor.instance.DefaultCursor();
    }

    public void OnButtonClick()
    {
        ChangeCursor.instance.DefaultCursor();
    }
}
