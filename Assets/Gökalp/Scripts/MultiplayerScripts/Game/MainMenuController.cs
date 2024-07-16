using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;

    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject joinScreen;

    [SerializeField] private Button submitCodeButton;
    [SerializeField] private TextMeshProUGUI codeText;


    // Start is called before the first frame update
    void OnEnable()
    {
        hostButton.onClick.AddListener(OnHostClicked);
        joinButton.onClick.AddListener(OnJoinClicked);
        submitCodeButton.onClick.AddListener(OnSubmitCodeClicked);
    }

    private void OnDisable()
    {
        hostButton.onClick.RemoveListener(OnHostClicked);
        joinButton.onClick.RemoveListener(OnJoinClicked);
        submitCodeButton.onClick.RemoveListener(OnSubmitCodeClicked);
    }

    private async void OnHostClicked()
    {
        bool succeeded = await GameLobbyManager.Instance.CreateLobby();
        if(succeeded)
        {
            SceneManager.LoadSceneAsync("Lobby");
        }
    }

    private void OnJoinClicked()
    {
        mainScreen.SetActive(false);
        joinScreen.SetActive(true);

    }

    private async void OnSubmitCodeClicked()
    {
        string code = codeText.text;
        code = code.Substring(0,code.Length - 1);

        bool succeeded = await GameLobbyManager.Instance.JoinLobby(code);

        if (succeeded)
        {
            SceneManager.LoadSceneAsync("Lobby");
        }
    }
}
