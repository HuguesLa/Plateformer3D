using UnityEngine;
using TMPro; // Pour TextMeshPro

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance; // Singleton pour accès global

    [SerializeField] private TMP_Text collectibleText; // Référence à l'élément UI TextMeshPro
    private int collectibleCount = 0; // Compteur de collectibles

    void Awake()
    {
        // Implémente le singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre les scènes
        }
        else
        {
            Destroy(gameObject); // Évite les doublons
        }
    }

    void Start()
    {
        // Initialise l'UI au démarrage
        UpdateUI();
    }

    // Fonction appelée quand un collectible est ramassé
    public void CollectItem()
    {
        collectibleCount++; // Incrémente le compteur
        UpdateUI(); // Met à jour l'UI
    }

    // Met à jour le texte de l'UI
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