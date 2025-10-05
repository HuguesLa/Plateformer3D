using TMPro;
using UnityEngine;

public class PVetdegat : MonoBehaviour {

    public static PVetdegat Instance;
    [SerializeField] private TMP_Text PVtext;
    public int PV = 3;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PV = PV - 1;
            UpdateUI();
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        UpdateUI();
    }
    private void UpdateUI()
    {
        if (PVtext != null)
        {
            PVtext.text = "PV : " + PV;
        }
        else
        {
            Debug.LogWarning("PVtext n'est pas assigné dans l'Inspecteur !");
        }
    }
}
