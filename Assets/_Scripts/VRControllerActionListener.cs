using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(ActionBasedController))]
public class VRControllerActionListener : MonoBehaviour
{
    [SerializeField]
    private string handSide;

    private ActionBasedController xrController;

    private InputAction primaryButton;
    private bool primaryButtonPressed;

    public delegate void RightControllerPrimaryButtonPressed();
    public static event RightControllerPrimaryButtonPressed OnRightControllerPrimaryButtonPressed;

    public delegate void LeftControllerPrimaryButtonPressed();
    public static event LeftControllerPrimaryButtonPressed OnLeftControllerPrimaryButtonPressed;

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
            if (handSide == "right")
            {
                VRControllerActionListener.OnRightControllerPrimaryButtonPressed.Invoke();
            } else
            {
                VRControllerActionListener.OnLeftControllerPrimaryButtonPressed.Invoke();
            }
        }
        else if (primaryButton.WasReleasedThisFrame())
        {
            primaryButtonPressed = false;
        }
    }
}
