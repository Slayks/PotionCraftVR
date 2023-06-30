using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levitate : MonoBehaviour
{
    private Vector3 basePosition;
    private bool isMoving;

    [SerializeField]
    AnimationCurve curve;

    public float upAndDownMaxDistance = 0.5f;
    public float time = 30;

    void Start()
    {
        this.basePosition = this.transform.localPosition;
    }

    void Update()
    {
        if (this.isMoving == false)
        {
            StartCoroutine(
                levitate(
                    this.transform.localPosition.y > this.basePosition.y
                    ? this.basePosition.y - this.upAndDownMaxDistance
                    : this.basePosition.y + this.upAndDownMaxDistance,
                    this.time
                )
            );
        }
    }

    IEnumerator levitate(float Yposition, float time)
    {
        Vector3 newPosition = this.transform.localPosition;
        newPosition.y = Yposition;
        this.isMoving = true;
        float _timeStartedLerping = Time.time;

        while (this.gameObject != null && this.transform.localPosition != newPosition)
        {
            float _timeSinceSTartedLerping = Time.time - _timeStartedLerping;
            float percentageComplete = _timeSinceSTartedLerping / time;
            this.transform.localPosition = Vector3.Slerp(this.transform.localPosition, newPosition, curve.Evaluate(percentageComplete));
            yield return new WaitForEndOfFrame();
        }

        this.isMoving = false;
    }
}
