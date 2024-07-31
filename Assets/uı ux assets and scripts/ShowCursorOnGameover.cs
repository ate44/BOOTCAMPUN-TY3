using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowCursorOnGameover : MonoBehaviour
{
    // AhmetGameover sahnesine geçtiðinde çalýþacak olan metod
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Sahne yüklendiðinde çalýþacak olan metod
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "AhmetGameover")
        {
            Cursor.visible = true; // Mouse imlecini görünür yapar
            Cursor.lockState = CursorLockMode.None; // Mouse imlecinin kilidini açar
        }
    }

    // Bu script devre dýþý býrakýldýðýnda çalýþacak olan metod
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}


