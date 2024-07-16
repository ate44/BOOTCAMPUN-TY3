using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySpawner : MonoBehaviour
{
    [SerializeField] private List<LobbyPlayer> players = new List<LobbyPlayer>();

    private void OnEnable()
    {
        LobbyEventsGame.OnLobbyUpdated += OnLobbyUpdated;
    }

    private void OnDisable()
    {
        LobbyEventsGame.OnLobbyUpdated -= OnLobbyUpdated;
    }

    private void OnLobbyUpdated()
    {
        List<LobbyPlayerData> playerData = GameLobbyManager.Instance.GetPlayers();

        for (int i = 0; i < playerData.Count; i++)
        {
            LobbyPlayerData data = playerData[i];
            players[i].SetData(data);
        }
    }
}
