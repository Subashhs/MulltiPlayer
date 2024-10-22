using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField playerNameInput; // Input field for player name
    public Button createRoomButton; // Button to create room
    public Button joinRoomButton; // Button to join room
    public string roomName = "RoomName"; // Default room name
    private bool isReadyToCreateOrJoin = false;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        createRoomButton.onClick.AddListener(CreateRoom);
        joinRoomButton.onClick.AddListener(JoinRoom);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master.");
        isReadyToCreateOrJoin = true; // Set flag to true
        // Optionally: Load the main menu or show UI for room management
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby.");
        isReadyToCreateOrJoin = true; // Set flag to true
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogError($"Disconnected: {cause}");
        isReadyToCreateOrJoin = false; // Set flag to false
    }

    public void CreateRoom()
    {
        if (!isReadyToCreateOrJoin)
        {
            Debug.LogWarning("Not ready to create room. Waiting for connection.");
            return; // Exit if not ready
        }

        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 20 };
        PhotonNetwork.CreateRoom(roomName, roomOptions);
        Debug.Log("Creating Room: " + roomName);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Create Room Failed: " + message);
        // Optionally: Show an error message to the player
    }

    public void JoinRoom()
    {
        if (!isReadyToCreateOrJoin)
        {
            Debug.LogWarning("Not ready to join room. Waiting for connection.");
            return; // Exit if not ready
        }

        PhotonNetwork.JoinRoom(roomName);
        Debug.Log("Joining Room: " + roomName);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Join Room Failed: " + message);
        // Optionally: Show an error message to the player
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room: " + roomName);
        // Optionally: Load the game scene or instantiate player here
        PhotonNetwork.Instantiate("PlayerPrefab", Vector3.zero, Quaternion.identity, 0);
    }
}
