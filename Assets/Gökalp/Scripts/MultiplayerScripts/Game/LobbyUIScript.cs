using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lobbyCodeText;
    [SerializeField] private Button readyButton;

    // Start is called before the first frame update
    void Start()
    {
        lobbyCodeText.text = $"Lobby code: {GameLobbyManager.Instance.GetLobbyCode()}";
    }

    private void OnEnable()
    {
        readyButton.onClick.AddListener(OnReadyPressed);
    }

    private void OnDisable()
    {
        readyButton.onClick.RemoveAllListeners();
    }

    private async void OnReadyPressed()
    {
        bool succeed = await GameLobbyManager.Instance.SetPlayerRead();
        if (succeed)

        {
            readyButton.gameObject.SetActive(false);
        }
    }
    

}
