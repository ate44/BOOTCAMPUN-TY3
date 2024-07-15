using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class GameLobbyManager : Singleton<GameLobbyManager>
{
    
    private List<LobbyPlayerData> lobbyPlayerDatas = new List<LobbyPlayerData>();

    private LobbyPlayerData localLobbyPlayerData;


    private void OnEnable()
    {
        LobbyEvents.OnLobbyUpdated += OnLobbyUpdated;
    }

    private void OnDisable()
    {
        LobbyEvents.OnLobbyUpdated -= OnLobbyUpdated;
    }

    public string GetLobbyCode()
    {
        return LobbyManager.Instance.GetLobyCode();
    }

    public async Task<bool> CreateLobby()
    {
        LobbyPlayerData playerData = new LobbyPlayerData();
        playerData.Initialize(AuthenticationService.Instance.PlayerId, "HostPlayer");

        bool succeeded = await LobbyManager.Instance.CreateLobby(4,true, playerData.Serialize());

        return succeeded;
    }

    public async Task<bool> JoinLobby(string code)
    {
        LobbyPlayerData playerData = new LobbyPlayerData();
        playerData.Initialize(AuthenticationService.Instance.PlayerId, "JoinPlayer");

        bool succeeded = await LobbyManager.Instance.JoinLobby(code, playerData.Serialize());
        return succeeded;
    }

    private void OnLobbyUpdated(Lobby lobby)
    {
        List<Dictionary<string, PlayerDataObject>> playerData = LobbyManager.Instance.GetPlayersData();
        lobbyPlayerDatas.Clear();

        foreach (Dictionary<string, PlayerDataObject> data in playerData)
        {
            LobbyPlayerData lobbyPlayerData = new LobbyPlayerData();
            lobbyPlayerData.Initialize(data);
            
            if(lobbyPlayerData.Id == AuthenticationService.Instance.PlayerId)
            {
                localLobbyPlayerData = lobbyPlayerData;
            }

            lobbyPlayerDatas.Add(lobbyPlayerData);
        }

        LobbyEventsGame.OnLobbyUpdated?.Invoke();
    }

    public List<LobbyPlayerData> GetPlayers()
    {
        return lobbyPlayerDatas;
    }

    public async Task<bool> SetPlayerRead()
    {
        localLobbyPlayerData.IsReady = true;
        return await LobbyManager.Instance.UpdatePlayerData(localLobbyPlayerData.Id, localLobbyPlayerData.Serialize());
    }
}
