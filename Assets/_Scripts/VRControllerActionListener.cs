using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class VRControllerActionListener : MonoBehaviour
{
    private ActionBasedController xrController;

    private InputAction primaryButton;
    private bool primaryButtonPressed;

    public delegate void PrimaryButtonPressed();
    public static event PrimaryButtonPressed OnPrimaryButtonPressed;

    // Start is called before the first frame update
    void Start()
    {
        xrController = GetComponent<ActionBasedController>();
        primaryButton = xrController.selectAction.action.actionMap.FindAction("Primary Button");
    }

    // Update is called once per frame
    void Update()
    {
        if (primaryButton.WasPressedThisFrame() && primaryButtonPressed == false)
        {
            primaryButtonPressed = true;
            VRControllerActionListener.OnPrimaryButtonPressed.Invoke();
        }
        else if (primaryButton.WasReleasedThisFrame())
        {
            primaryButtonPressed = false;
        }
    }
}
