using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI; // Pour le Button

public class EndLevelObject : MonoBehaviour
{
    [SerializeField] private TMP_Text endLevelText;
    [SerializeField] private Button returnButton;
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private MeshRenderer meshRenderer;
    private Collider colliderTrigger;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        colliderTrigger = GetComponent<Collider>();

        if (meshRenderer != null) meshRenderer.enabled = false;
        if (colliderTrigger != null) colliderTrigger.enabled = false;

        if (CollectibleManager.Instance != null)
        {
            CollectibleManager.Instance.OnRequiredCollectiblesReached.AddListener(ActivateEndObject);
        }
        else
        {
            Debug.LogError("CollectibleManager.Instance est null !");
        }

        if (endLevelText != null) endLevelText.gameObject.SetActive(false);
        if (returnButton != null)
        {
            returnButton.gameObject.SetActive(false);
            returnButton.onClick.AddListener(ReturnToMainMenu);
        }
    }

    private void ActivateEndObject()
    {
        if (meshRenderer != null) meshRenderer.enabled = true;
        if (colliderTrigger != null) colliderTrigger.enabled = true;
        Debug.Log("Objet de fin de niveau activé !");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (endLevelText != null)
            {
                endLevelText.text = "Niveau terminé !";
                endLevelText.gameObject.SetActive(true);
            }

            if (returnButton != null)
            {
                returnButton.gameObject.SetActive(true);
            }

            Time.timeScale = 0f;
        }
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
        if (CollectibleManager.Instance != null)
        {
            CollectibleManager.Instance.ResetCollectibles();
        }
    }
}