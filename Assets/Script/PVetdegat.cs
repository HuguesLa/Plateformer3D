using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PVetdegat : MonoBehaviour {

    public static PVetdegat Instance;

    [SerializeField] private TMP_Text PVtext;
    private int PV = 3;

    void Awake()
    {
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
    public void DmgSubit()
    {
        PV--;
        UpdateUI();
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
        if (PV <= 0){
            EndLevelObject.Instance.GameOver();
            Debug.LogWarning("PV = 0");
        }

    }
}
