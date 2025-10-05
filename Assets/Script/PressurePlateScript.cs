using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    public GameObject targetObject; 
    private bool isPressed = false;
    private int pressCount = 0; 

    private void ActivateTarget(bool activate)
    {
        if (targetObject == null) return;

        DoorScript door = targetObject.GetComponent<DoorScript>();
        if (door != null)
        {
            door.OpenDoor(activate);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Plate"))
        {
            pressCount++;
            if (pressCount > 0 && !isPressed)
            {
                isPressed = true;
                ActivateTarget(true);
                Debug.Log("Plaque pressée !");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Plate"))
        {
            pressCount--;
            if (pressCount <= 0 && isPressed)
            {
                isPressed = false;
                ActivateTarget(false);
                Debug.Log("Plaque relâchée !");
            }
        }
    }
}