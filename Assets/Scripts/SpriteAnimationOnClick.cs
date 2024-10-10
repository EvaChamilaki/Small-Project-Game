using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimationOnClick : MonoBehaviour
{
    private Animator animController;

    public void PlayButtonAnim()
    {
        animController = this.gameObject.GetComponent<Animator>();

        if (animController == null)
        {
            Debug.Log("Animator Controller: Not found");
            return;
        }

        StartCoroutine(WaitForAnimation());
    }

    private IEnumerator WaitForAnimation()
    {
        animController.SetTrigger("pressed_button");

        yield return new WaitForSeconds(0.5f);
        GameObject go = GameObject.Find("MainScreen");

        if (go != null)
        {   
            go.GetComponent<ComputerScreenSwitch>().SwitchScreens();
        }
    }
}
