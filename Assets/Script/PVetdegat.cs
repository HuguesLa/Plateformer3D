using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PVetdegat : MonoBehaviour
{
    public static PVetdegat Instance;

    [SerializeField] private TMP_Text PVtext;
    [SerializeField] private int maxPV = 3;
    private int PV;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PV = maxPV;
        UpdateUI();
    }

    public void DmgSubit()
    {
        PV--;
        UpdateUI();
        if (PV <= 0)
        {
            if (EndLevelObject.Instance != null)
            {
                EndLevelObject.Instance.GameOver();
            }
            Debug.LogWarning("PV = 0");
        }
    }

    private void UpdateUI()
    {
        if (PVtext != null)
        {
            PVtext.text = "PV : " + PV;
        }
        else
        {
            Debug.LogWarning("PVtext n'est pas assigné dans l'Inspecteur ! Tentative de recherche automatique...");
            FindPVText();
        }
    }

    private void FindPVText()
    {
        GameObject pvTextObject = GameObject.FindWithTag("PVUI");
        if (pvTextObject != null)
        {
            PVtext = pvTextObject.GetComponent<TMP_Text>();
            if (PVtext != null)
            {
                Debug.Log("PVtext trouvé et assigné automatiquement.");
                UpdateUI();
            }
            else
            {
                Debug.LogWarning("Aucun composant TMP_Text trouvé sur l'objet avec le tag 'PVUI'.");
            }
        }
        else
        {
            Debug.LogWarning("Aucun GameObject avec le tag 'PVUI' trouvé dans la scène.");
        }
    }

    public void ResetPV()
    {
        PV = maxPV;
        UpdateUI();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindPVText();
        UpdateUI();
    }
}