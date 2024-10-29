using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDoor : MonoBehaviour
{
    public AnimationCurve openCurve;    // Animation curve for opening
    public AnimationCurve closeCurve;   // Animation curve for closing
    public float openDuration = 2.0f;   // Duration to open
    public float closeDuration = 2.0f;  // Duration to close
    public float returnSpeed = 1.0f;    // Speed for returning to last position

    private Coroutine currentCoroutine;
    public Vector3 initialPosition;
    public Vector3 targetPosition;
    public Vector3 multiplier = new Vector3(1, 0, 0); // Default to open along X axis
    public bool doorIsOpen = false;

    void Start()
    {
        initialPosition = transform.localPosition;
        targetPosition = initialPosition + multiplier; // Set the target position
    }

    public void OpenDoor()
    {
        Debug.Log("Attempting to open door");
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(DoorAction(targetPosition, openCurve, openDuration));
    }

    public void CloseDoor()
    {
        Debug.Log("Attempting to close door");
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(DoorAction(initialPosition, closeCurve, closeDuration));
    }

    public void InterruptDoor()
    {
        if (currentCoroutine != null) StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ReturnToLastPosition());
    }

    private IEnumerator DoorAction(Vector3 target, AnimationCurve curve, float duration)
    {
        Debug.Log($"Moving door to target position: {target}");
        float elapsedTime = 0f;
        Vector3 startPosition = transform.localPosition;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float curveValue = curve.Evaluate(t);
            transform.localPosition = Vector3.Lerp(startPosition, target, curveValue);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = target; // Ensure it reaches the target
        doorIsOpen = target == targetPosition; // Update door state
        Debug.Log("Door action completed");
    }

    private IEnumerator ReturnToLastPosition()
    {
        Debug.Log("Returning door to last position");
        Vector3 startPosition = transform.localPosition;
        Vector3 target = doorIsOpen ? initialPosition : targetPosition; // Determine target based on current state
        float elapsedTime = 0f;

        while (elapsedTime < returnSpeed)
        {
            float t = elapsedTime / returnSpeed;
            transform.localPosition = Vector3.Lerp(startPosition, target, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = target; // Ensure it reaches the target
        doorIsOpen = false; // Reset door state
        Debug.Log("Door returned to last position");
    }
}
