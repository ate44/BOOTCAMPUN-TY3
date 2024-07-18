


using System.Linq;
using System.Threading.Tasks;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;


public class RelayManager : Singleton<RelayManager>
{
    private string _joinCode;
    private RelayServerEndpoint conn;
    private string _ip;
    private int _port;
    private byte[] _key;
    private byte[] _connectionData;
    private byte[] _hostConnectionData;
    private byte[] _allocationIdBytes;
    private System.Guid _allocationId;
    

    private bool _isHost = false;

    public bool IsHost
    {
        get { return _isHost; }
    }

    internal string GetAllocationId()
    {
        return _allocationId.ToString();
    }

    internal string GetConnectionData()
    {
        return _connectionData.ToString();
    }

    public async Task<string> CreateRelay(int maxConnection)
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
        _joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        RelayServerEndpoint endpoint = allocation.ServerEndpoints.First(conn => conn.ConnectionType == "dtls");

        _ip = endpoint.Host;
        _port = endpoint.Port;
        _allocationId = allocation.AllocationId;
        _allocationIdBytes = allocation.AllocationIdBytes;
        _connectionData = allocation.ConnectionData;
        _key = allocation.Key;

        _isHost = true;

        return _joinCode;

    }

    public async Task<bool> JoinRelay(string joinCode)
    {
        _joinCode = joinCode;

        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        RelayServerEndpoint endpoint = allocation.ServerEndpoints.First(conn => conn.ConnectionType == "dtls");

        _ip = endpoint.Host;
        _port = endpoint.Port;
        _allocationId = allocation.AllocationId;
        _allocationIdBytes = allocation.AllocationIdBytes;
        _connectionData = allocation.ConnectionData;
        _key = allocation.Key;
        _hostConnectionData = allocation.HostConnectionData;

        return true;


    }

    public (byte[] AllocationIdBytes, byte[] Key, byte[] ConnectionData, string Ip, int Port) GetHostConnectionInfo()
    {
        return (_allocationIdBytes, _key, _connectionData, _ip, _port);
    }

    public (byte[] AllocationIdBytes, byte[] Key, byte[] ConnectionData, byte[] HostConnectionData, string Ip, int Port) GetClientConnectionInfo()
    {
        return (_allocationIdBytes, _key, _connectionData, _hostConnectionData, _ip, _port);
    }




}
