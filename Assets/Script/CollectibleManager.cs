using UnityEngine;
using TMPro; // Pour TextMeshPro

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    [SerializeField] private TMP_Text collectibleText;
    private int collectibleCount = 0;

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

    void Start()
    {
        UpdateUI();
    }

    public void CollectItem()
    {
        collectibleCount++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (collectibleText != null)
        {
            collectibleText.text = "Collectibles : " + collectibleCount;
        }
        else
        {
            Debug.LogWarning("CollectibleText n'est pas assigné dans l'Inspecteur !");
        }
    }
}