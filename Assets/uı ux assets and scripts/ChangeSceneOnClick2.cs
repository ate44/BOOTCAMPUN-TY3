using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneOnClick2 : MonoBehaviour
{
    
    public Button Buttonbacktomainmenu;

    void Start()
    {
        
        Buttonbacktomainmenu.onClick.AddListener(LoadScene0);
    }

    void LoadScene0()
    {
        
        SceneManager.LoadScene(0);
    }
}

