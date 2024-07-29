using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBackHandler : MonoBehaviour
{
    public void OnButtonBackClicked()
    {
        // �nceki sahne ad�n� al
        string previousScene = PlayerPrefs.GetString("PreviousScene", "");
        Debug.Log("G�KALP");
        if (!string.IsNullOrEmpty(previousScene))
        {
            // �nceki sahneye d�n
            Debug.Log("iLK");
            SceneManager.LoadScene(previousScene);
            Debug.Log("SON");
        }
    }
}
