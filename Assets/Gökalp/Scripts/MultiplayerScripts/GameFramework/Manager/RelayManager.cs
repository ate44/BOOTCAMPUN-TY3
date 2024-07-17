

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity.Services.Lobbies.Models;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class RelayManager : Singleton<RelayManager>
{
    private string _joinCode;
    private RelayData data;

    private bool isHost = false;

    public bool IsHost 
    { 
        get { return isHost; }  
    }

    public string GetAllocationId()
    {
        return data.AllocationID.ToString();
    }

    public string GetConnectionData()
    {
        return data.ConnectionData.ToString();
    }

    public async Task<string> CreateRelay(int maxConnection)
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);

        data = new RelayData()
        {
            IPv4Address = allocation.RelayServer.IpV4,
            Port = (ushort)allocation.RelayServer.Port,
            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            ConnectionData = allocation.ConnectionData,
            Key = allocation.Key,
            joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId),

        };

        _joinCode = data.joinCode;
        isHost = true;


        if (_joinCode == null)
        {
            UnityEngine.Debug.Log("join null");

        }
        else
        {
            UnityEngine.Debug.Log("join not null");

        }

        return _joinCode;
        
    }

    public async Task<bool> JoinRelay(string joinCode)
    {
        _joinCode = joinCode;
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        data = new RelayData()
        {
            IPv4Address = allocation.RelayServer.IpV4,
            Port = (ushort)allocation.RelayServer.Port,
            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            ConnectionData = allocation.ConnectionData,
            HostConnectionData = allocation.HostConnectionData,
            Key = allocation.Key,

        };

        if(data.HostConnectionData == null)
        {
            UnityEngine.Debug.Log("null");

        }
        else
        {
            UnityEngine.Debug.Log("not null");

        }


        return true;
    }

    public (byte[] AllocationIdBytes, byte[] key, byte[] ConnectionData, string ip, ushort port) GetHostConnectionInfo()
    {
        return (data.AllocationIDBytes, data.Key, data.ConnectionData, data.IPv4Address, data.Port);
    }

    public (byte[] AllocationIdBytes, byte[] key, byte[] ConnectionData, byte[] HostConnectionData, string ip, ushort port) GetClientConnectionInfo()
    {
        return (data.AllocationIDBytes, data.Key, data.ConnectionData, data.HostConnectionData, data.IPv4Address, data.Port);
    }



    public struct RelayData
    {
        public string joinCode;
        public string IPv4Address;
        public ushort Port;
        public Guid AllocationID;
        public byte[] AllocationIDBytes;
        public byte[] ConnectionData;
        public byte[] HostConnectionData;
        public byte[] Key;
    }




}
