
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class RelayManager : Singleton<RelayManager>
{
    private string _joinCode;
    private RelayHostData hostData;
    private RelayJoinData joinData;
    private bool _isHost = false;

    
    public struct RelayHostData
    {
        public string joinCode;
        public string IPv4Address;
        public ushort Port;
        public Guid AllocationID;
        public byte[] AllocationIDBytes;
        public byte[] ConnectionData;
        public byte[] Key;

    }

    public struct RelayJoinData
    {
        public string IPv4Address;
        public ushort Port;
        public Guid AllocationID;
        public byte[] AllocationIDBytes;
        public byte[] ConnectionData;
        public byte[] HostConnectionData;
        public byte[] Key;

    }

    public bool IsHost
    {
        get { return _isHost; }
    }

    public string GetAllocationId()
    {
        return hostData.AllocationID.ToString();
    }

    public string GetConnectionData()
    {
        if (hostData.ConnectionData == null)
        {
            UnityEngine.Debug.LogError("hostData.ConnectionData is null");
            return null;
        }

        return BitConverter.ToString(hostData.ConnectionData);
    }

    public async Task<string> CreateRelay(int maxConnection)
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);

        if (allocation.ConnectionData == null)
        {
            UnityEngine.Debug.LogError("allocation.ConnectionData is null");
            return null;
        }

        hostData = new RelayHostData()
        {
            IPv4Address = allocation.RelayServer.IpV4,
            Port = (ushort)allocation.RelayServer.Port,
            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            ConnectionData = allocation.ConnectionData,
            Key = allocation.Key
        };

        _joinCode = hostData.joinCode = await RelayService.Instance.GetJoinCodeAsync(hostData.AllocationID);

        _isHost = true;
        UnityEngine.Debug.Log("createrelay");

        return _joinCode;

    }

    public async Task<bool> JoinRelay(string joinCode)
    {
        _joinCode = joinCode;
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        joinData = new RelayJoinData()
        {
            IPv4Address = allocation.RelayServer.IpV4,
            Port = (ushort)allocation.RelayServer.Port,
            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            ConnectionData = allocation.ConnectionData,
            HostConnectionData = allocation.HostConnectionData,
            Key = allocation.Key

        };

        UnityEngine.Debug.Log("joinrelay");

        return true;
    }

    public (byte[] AllocationId, byte[] key, byte[] ConnectionData, string dtlsAddress, ushort dtlsPort) GetHostConnectionInfo(){
        return (hostData.AllocationIDBytes, hostData.Key, hostData.ConnectionData, hostData.IPv4Address, hostData.Port);
    }

    public (byte[] AllocationId, byte[] key, byte[] ConnectionData, byte[] HostConnectionData, string dtlsAddress, ushort dtlsPort) GetClientConnectionInfo()
    {
        return (joinData.AllocationIDBytes, joinData.Key, joinData.ConnectionData, joinData.HostConnectionData, joinData.IPv4Address, joinData.Port);
    }





}
