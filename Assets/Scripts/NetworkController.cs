using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // Make sure to include this for Photon functionality
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks // Inherit from MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Connects to Photon master servers
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("We are now connected to the " + PhotonNetwork.CloudRegion + " server!"); // Added a space
    }

    // Update is called once per frame
    void Update()
    {
        // You can add update logic here if needed
    }
}
