public static class LobbyEventsGame
{
    public delegate void LobbyUpdated();
    public static LobbyUpdated OnLobbyUpdated;

    public delegate void LobbyReady();
    public static LobbyReady OnLobbyReady;

}
