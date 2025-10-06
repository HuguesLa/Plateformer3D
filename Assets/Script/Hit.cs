using UnityEngine;

public class Hit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            PVetdegat.Instance.DmgSubit();
            gameObject.SetActive(false);
            // Destroy(gameObject);
        }
    }
}



