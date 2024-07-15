using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lobbyCodeText;
    [SerializeField] private Button readyButton;
    [SerializeField] private Image mapImage;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private TextMeshProUGUI mapName;
    [SerializeField] private MapSelectionData mapSelectionData;

    private int currentMapIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        lobbyCodeText.text = $"Lobby code: {GameLobbyManager.Instance.GetLobbyCode()}";

        if (!GameLobbyManager.Instance.isHost)
        {
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {

        if (GameLobbyManager.Instance.isHost)
        {         
            leftButton.onClick.AddListener(OnLeftButtonClicked);
            rightButton.onClick.AddListener(OnRightButtonClicked);
        }

        readyButton.onClick.AddListener(OnReadyPressed);

        LobbyEventsGame.OnLobbyUpdated += OnLobbyUpdated;
    }


    private void OnDisable()
    {

        if (GameLobbyManager.Instance.isHost)
        {
            leftButton.onClick.RemoveAllListeners();
            rightButton.onClick.RemoveAllListeners();
        }

        readyButton.onClick.RemoveAllListeners();
        
        LobbyEventsGame.OnLobbyUpdated -= OnLobbyUpdated;
    }

    private async void OnLeftButtonClicked()
    {
        if (currentMapIndex - 1 > 0)
        {
            currentMapIndex--;
        }
        else
        {
            currentMapIndex = 0;
        }

        UpdateMap();

        GameLobbyManager.Instance.SetSelectedMap(currentMapIndex);
    }

    private async void OnRightButtonClicked()
    {
        if (currentMapIndex + 1 < mapSelectionData.Maps.Count - 1)
        {
            currentMapIndex++;
        }
        else
        {
            currentMapIndex = mapSelectionData.Maps.Count - 1;
        }

        UpdateMap();

        GameLobbyManager.Instance.SetSelectedMap(currentMapIndex);
    }

    private void UpdateMap()
    {
        mapImage.color = mapSelectionData.Maps[currentMapIndex].MapThumbnail;
        mapName.text = mapSelectionData.Maps[currentMapIndex].MapName;

    }

    private async void OnReadyPressed()
    {
        bool succeed = await GameLobbyManager.Instance.SetPlayerRead();
        if (succeed)

        {
            readyButton.gameObject.SetActive(false);
        }
    }

    private void OnLobbyUpdated()
    {
        currentMapIndex = GameLobbyManager.Instance.GetMapIndex();
        UpdateMap();
    }

}
