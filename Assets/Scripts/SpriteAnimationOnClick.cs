using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteAnimationOnClick : MonoBehaviour
{
    private Animator animController;
    private string trigger_button;
    private Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void PlayButtonAnim()
    {
        animController = this.gameObject.GetComponent<Animator>();

        if (animController == null)
        {
            Debug.Log("Animator Controller: Not found");
            return;
        }

        if (this.gameObject.tag == "victim" && scene.name == "Scene1 - 2")
        {
            trigger_button = "vic2_pressed_button";
        }
        else if (this.gameObject.tag == "toxic" && scene.name == "Scene2")
        {
            trigger_button = "toxic_pressed_button";
        }
        else if (this.gameObject.tag == "victim" && scene.name == "Scene1")
        {
            trigger_button = "pressed_button";
        }
        else
        {
            trigger_button = "username_button";
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
