
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.NetworkConfig.ConnectionApproval = true;
        
        if(RelayManager.Instance.IsHost) 
        {
            NetworkManager.Singleton.ConnectionApprovalCallback = ConnectionApproval;
            (byte[] AllocationId, byte[] key, byte[] ConnectionData, string dtlsAddress, ushort dtlsPort) = RelayManager.Instance.GetHostConnectionInfo();
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(dtlsAddress, dtlsPort, AllocationId, key, ConnectionData, true);
            NetworkManager.Singleton.StartHost();
            Debug.Log("host");
        }
        else
        {
            (byte[] AllocationId, byte[] key, byte[] ConnectionData, byte[] HostConnectionData, string dtlsAddress, ushort dtlsPort) = RelayManager.Instance.GetClientConnectionInfo();
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(dtlsAddress, dtlsPort, AllocationId, key, ConnectionData, HostConnectionData, true);
            NetworkManager.Singleton.StartClient();
            Debug.Log("client");
        }
    }

    private void ConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        response.Approved = true;
        response.CreatePlayerObject = true;
        response.Pending = false;
    }
}
