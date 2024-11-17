using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ButtonTracker : MonoBehaviour
{
    public static ButtonTracker Instance; // Singleton instance
    public GameObject plane;
    private Animator animController;
    private string trigger_button;

    [HideInInspector]
    public Button lastPressedButton;

    private void Awake()
    {
        animController = plane.GetComponent<Animator>();

        if (animController == null)
        {
            Debug.Log("Animator Controller: Not found");
            return;
        }

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterButtonPress(Button button)
    {
        lastPressedButton = button;
        Debug.Log("Last pressed button: " + button.name);
    }

    public void PlayAnimOnButtonClick()
    {
        if (lastPressedButton.name == "Char1Button")
        {
            trigger_button = "char1";
        }
        else if (lastPressedButton.name == "Char2Button")
        {
            trigger_button = "char2";
        }

        StartCoroutine(WaitForAnimation(trigger_button));
    }

    private IEnumerator WaitForAnimation(string _trbut_name)
    {
        animController.SetTrigger(_trbut_name);

        yield return new WaitForSeconds(0.5f);

        if (plane != null)
        {
            plane.GetComponent<ComputerScreenSwitch>().SwitchScreens();
        }
    }
}
