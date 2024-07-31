using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowCursorOnGameover : MonoBehaviour
{
    // AhmetGameover sahnesine ge�ti�inde �al��acak olan metod
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Sahne y�klendi�inde �al��acak olan metod
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "AhmetGameover")
        {
            Cursor.visible = true; // Mouse imlecini g�r�n�r yapar
            Cursor.lockState = CursorLockMode.None; // Mouse imlecinin kilidini a�ar
        }
    }

    // Bu script devre d��� b�rak�ld���nda �al��acak olan metod
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}


