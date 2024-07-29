using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBackHandler : MonoBehaviour
{
    public void OnButtonBackClicked()
    {
        // Önceki sahne adýný al
        string previousScene = PlayerPrefs.GetString("PreviousScene", "");
        Debug.Log("GÖKALP");
        if (!string.IsNullOrEmpty(previousScene))
        {
            // Önceki sahneye dön
            Debug.Log("iLK");
            SceneManager.LoadScene(previousScene);
            Debug.Log("SON");
        }
    }
}
