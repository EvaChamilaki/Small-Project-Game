using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationOnClick : MonoBehaviour
{
    private Animator animController;
    private string trigger_button = "pressed_button";

    public void PlayButtonAnim()
    {
        animController = this.gameObject.GetComponent<Animator>();

        if (animController == null)
        {
            Debug.Log("Animator Controller: Not found");
            return;
        }

        if(this.gameObject.tag == "toxic")
        {
            trigger_button = "toxic_pressed_button";
        }

        StartCoroutine(WaitForAnimation(trigger_button));
    }

    private IEnumerator WaitForAnimation(string _trbut_name)
    {
        animController.SetTrigger(_trbut_name);

        yield return new WaitForSeconds(0.5f);
        GameObject go = GameObject.Find("MainScreen");

        if (go != null)
        {   
            go.GetComponent<ComputerScreenSwitch>().SwitchScreens();
        }
    }
}
