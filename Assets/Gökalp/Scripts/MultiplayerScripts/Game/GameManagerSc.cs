
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class GameManagerSc : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionApproval = true;
        if (RelayManager.Instance.IsHost)
        {
            NetworkManager.Singleton.ConnectionApprovalCallback = ConnectionApproval;
            (byte[] AllocationIdBytes, byte[] key, byte[] ConnectionData, string ip ,int port) = RelayManager.Instance.GetHostConnectionInfo();
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(ip, (ushort)port, AllocationIdBytes, key, ConnectionData, true);
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            (byte[] AllocationIdBytes, byte[] key, byte[] ConnectionData, byte[] HostConnectionData,  string ip, int port) = RelayManager.Instance.GetClientConnectionInfo();
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(ip, (ushort)port, AllocationIdBytes, key, ConnectionData, HostConnectionData,true);
            NetworkManager.Singleton.StartClient();
        }
    }

    private void ConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Pending = false;
    }


}
