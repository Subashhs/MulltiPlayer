using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class HomePage : MonoBehaviourPunCallbacks
{
    public InputField playerNameInput;
    public Button createRoomButton;
    public Button joinRoomButton;

    private void Start()
    {
        // Make sure to connect to Photon
        PhotonNetwork.ConnectUsingSettings();

        // Add listeners to buttons
        createRoomButton.onClick.AddListener(CreateRoom);
        joinRoomButton.onClick.AddListener(JoinRoom);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server");
        createRoomButton.interactable = true;
        joinRoomButton.interactable = true;
    }

    // Function to create a room
    public void CreateRoom()
    {
        string playerName = playerNameInput.text;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Player name cannot be empty!");
            return;
        }

        PhotonNetwork.NickName = playerName;

        // Create the room with some basic options
        RoomOptions roomOptions = new RoomOptions { MaxPlayers = 20 }; // Max of 20 players
        PhotonNetwork.CreateRoom(null, roomOptions); // Create a random room
    }

    // Function to join a room
    public void JoinRoom()
    {
        string playerName = playerNameInput.text;
        if (string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Player name cannot be empty!");
            return;
        }

        PhotonNetwork.NickName = playerName;
        PhotonNetwork.JoinRandomRoom();
    }

    // Called when the room is created successfully
    public override void OnCreatedRoom()
    {
        Debug.Log("Room created successfully");
        PhotonNetwork.LoadLevel("GameScene"); // Load your next multiplayer scene here
    }

    // Called when the room join is successful
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully");
        PhotonNetwork.LoadLevel("GameScene"); // Load your next multiplayer scene here
    }

    // Called if there are no rooms to join
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join a random room: " + message);
    }
}
