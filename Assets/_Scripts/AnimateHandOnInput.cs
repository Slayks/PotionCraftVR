using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public Animator handAnimator;

    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;

    void Update()
    {
        handAnimator.SetFloat("Trigger", pinchAnimationAction.action.ReadValue<float>());
        handAnimator.SetFloat("Grip", gripAnimationAction.action.ReadValue<float>());
    }
}
