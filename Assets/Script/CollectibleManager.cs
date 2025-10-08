using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    [SerializeField] private TMP_Text collectibleText;
    [SerializeField] private int requiredCollectibles = 5;

    private int collectibleCount = 0;

    public UnityEvent OnRequiredCollectiblesReached;

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
        FindCollectibleText();
        UpdateUI();
    }

    void Start()
    {
        UpdateUI();
    }

    public void CollectItem()
    {
        collectibleCount++;
        UpdateUI();

        if (collectibleCount >= requiredCollectibles)
        {
            OnRequiredCollectiblesReached?.Invoke();
        }
    }

    private void UpdateUI()
    {
        if (collectibleText != null)
        {
            collectibleText.text = "Collectibles : " + collectibleCount + " / " + requiredCollectibles;
        }
        else
        {
            Debug.LogWarning("CollectibleText n'est pas assigné ! Tentative de recherche automatique...");
            FindCollectibleText();
        }
    }

    private void FindCollectibleText()
    {
        GameObject hudText = GameObject.FindWithTag("CollectibleUI");
        if (hudText != null)
        {
            collectibleText = hudText.GetComponent<TMP_Text>();
            if (collectibleText != null)
            {
                Debug.Log("CollectibleText trouvé et assigné automatiquement.");
                UpdateUI();
            }
            else
            {
                Debug.LogWarning("Aucun composant TMP_Text trouvé sur l'objet avec le tag 'CollectibleUI'.");
            }
        }
        else
        {
            Debug.LogWarning("Aucun GameObject avec le tag 'CollectibleUI' trouvé dans la scène.");
        }
    }

    public void ResetCollectibles()
    {
        collectibleCount = 0;
        UpdateUI();
    }
}