using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Dropdown levelDropdown; 

    void Start()
    {
    }

    public void StartGame()
    {
    
        int selectedLevelIndex = levelDropdown.value+1;
            
        SceneManager.LoadScene(selectedLevelIndex);
        
    }
}