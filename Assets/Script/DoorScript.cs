using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{
    private bool isOpen = false; 
    private bool isAnimating = false;
    public float rotationAngle = 90f; 
    public float duration = 1f; 

    public void OpenDoor(bool open)
    {
        if (isAnimating) return;

        if (open && !isOpen)
        {
            StartCoroutine(RotateDoor(rotationAngle, duration));
            isOpen = true;
        }
        else if (!open && isOpen)
        {
            StartCoroutine(RotateDoor(-rotationAngle, duration));
            isOpen = false;
        }
    }

    private IEnumerator RotateDoor(float angle, float dur)
    {
        isAnimating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, angle, 0f);
        float time = 0f;

        while (time < dur)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time / dur);
            yield return null;
        }

        transform.rotation = endRotation;
        isAnimating = false;
    }
}