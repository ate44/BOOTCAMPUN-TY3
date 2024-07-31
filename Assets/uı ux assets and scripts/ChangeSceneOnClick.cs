using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneOnClick : MonoBehaviour
{
    
    public Button Buttonhowtoplay;

    void Start()
    {
        
        Buttonhowtoplay.onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        
        SceneManager.LoadScene(1);
    }
}
