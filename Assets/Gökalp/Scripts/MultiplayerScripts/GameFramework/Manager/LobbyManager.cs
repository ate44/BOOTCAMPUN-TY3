using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Lobbies.Models;
using Unity.VisualScripting;
using System;
using System.Collections;

public class LobbyManager : Singleton<LobbyManager>
{
    private Lobby lobby;
    private Coroutine hearthbeatCoroutine;
    private Coroutine refreshLobbyCoroutine;

    public string GetLobyCode()
    {
        return lobby?.LobbyCode;
    }

    public async Task<bool> CreateLobby(int maxPlayers, bool isPrivate, Dictionary<string,string> data, Dictionary<string,string> lobbyData)
    {
        Dictionary<string, PlayerDataObject> playerData = SerializePlayerData(data);

        Player player = new Player(AuthenticationService.Instance.PlayerId, null, playerData);

        CreateLobbyOptions options = new CreateLobbyOptions()
        {
            Data = SerializeLobbyData(lobbyData),
            IsPrivate = isPrivate,
            Player = player
        };

        try
        {
            lobby = await LobbyService.Instance.CreateLobbyAsync("Lobby", maxPlayers, options);

        }
        catch (System.Exception)
        {
            return false;
        }

        Debug.Log($"lobby created with lobby id {lobby.Id}");

        hearthbeatCoroutine = StartCoroutine(HearthbeatLobbyCoroutine(lobby.Id, 6f));
        refreshLobbyCoroutine = StartCoroutine(RefreshLobbyCoroutine(lobby.Id, 1f));

        return true;
    }

    private IEnumerator HearthbeatLobbyCoroutine(string lobbyId, float waitTimeSeconds)
    {
        while(true)
        {
            Debug.Log("Hearthbeat");
            LobbyService.Instance.SendHeartbeatPingAsync(lobbyId);
            yield return new WaitForSecondsRealtime(waitTimeSeconds); 
        }
    }

    private IEnumerator RefreshLobbyCoroutine(string lobbyId, float waitTimeSeconds)
    {
        while (true)
        {
            Task<Lobby> task = LobbyService.Instance.GetLobbyAsync(lobbyId);
            yield return new WaitUntil(() => task.IsCompleted);
            Lobby newLobby = task.Result;
            if(newLobby.LastUpdated > lobby.LastUpdated)
            {
                lobby = newLobby;
                LobbyEvents.OnLobbyUpdated?.Invoke(newLobby);
            }
            yield return new WaitForSecondsRealtime(waitTimeSeconds);
        }
    }

    private Dictionary<string, PlayerDataObject> SerializePlayerData(Dictionary<string, string> data)
    {
        Dictionary<string, PlayerDataObject> playerData = new Dictionary<string, PlayerDataObject>();
        foreach(var (key,value) in data)
        {
            playerData.Add(key, new PlayerDataObject(
                visibility: PlayerDataObject.VisibilityOptions.Member,
                value: value
                ));
        }
        return playerData;
    }

    private Dictionary<string, DataObject> SerializeLobbyData(Dictionary<string, string> lobbyData)
    {
        Dictionary<string, DataObject> _lobbyData = new Dictionary<string, DataObject>();
        foreach (var (key, value) in lobbyData)
        {
            _lobbyData.Add(key, new DataObject(
                visibility: DataObject.VisibilityOptions.Member,
                value: value
                ));
        }
        return _lobbyData;
    }

    public void OnApplicationQuit()
    {
        if(lobby != null && lobby.HostId == AuthenticationService.Instance.PlayerId)
        {
            LobbyService.Instance.DeleteLobbyAsync(lobby.Id);
        }
    }

    public async Task<bool> JoinLobby(string code, Dictionary<string, string> playerData)
    {
        JoinLobbyByCodeOptions options = new JoinLobbyByCodeOptions();
        Player player = new Player(AuthenticationService.Instance.PlayerId, null, SerializePlayerData(playerData));

        options.Player = player;

        try
        {
            lobby = await LobbyService.Instance.JoinLobbyByCodeAsync(code, options);

        }
        catch (System.Exception)
        {
            return false;
        }

        refreshLobbyCoroutine = StartCoroutine(RefreshLobbyCoroutine(lobby.Id, 1f));

        return true;

    }

    internal List<Dictionary<string, PlayerDataObject>> GetPlayersData()
    {
        List<Dictionary<string, PlayerDataObject>> data = new List<Dictionary<string, PlayerDataObject>>();

        foreach (Player player in lobby.Players)
        {
            data.Add(player.Data);
        }

        return data;
    }

    public async Task<bool> UpdatePlayerData(string playerId, Dictionary<string, string> data)
    {
        Dictionary<string, PlayerDataObject> playerData = SerializePlayerData(data);

        UpdatePlayerOptions options = new UpdatePlayerOptions()
        {
            Data = playerData
        };

        try
        {
            lobby = await LobbyService.Instance.UpdatePlayerAsync(lobby.Id, playerId, options);

        }catch(System.Exception) { return false; }

        LobbyEvents.OnLobbyUpdated(lobby);

        return true;

    }

    public async Task<bool> UpdateLobbyData(Dictionary<string, string> lobbyData)
    {
        Dictionary<string, DataObject> _lobbyData = SerializeLobbyData(lobbyData);

        UpdateLobbyOptions options = new UpdateLobbyOptions() { Data = _lobbyData };

        try
        {
            lobby = await LobbyService.Instance.UpdateLobbyAsync(lobby.Id, options);

        }catch(System.Exception) { return false; }

        LobbyEvents.OnLobbyUpdated(lobby);

        return true;
    }

    public string GetHostId()
    {
        return lobby.HostId;
    }
}
