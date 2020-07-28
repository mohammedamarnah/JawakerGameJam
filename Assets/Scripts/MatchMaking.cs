using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

namespace JawakerGameJam {
public class MatchMaking : MonoBehaviourPunCallbacks {
    [SerializeField] private Text networkText;
    
    void Awake() {
        PhotonNetwork.ConnectUsingSettings();
    }
    
    public override void OnConnectedToMaster() {
        networkText.text = "Try to join room";
        PhotonNetwork.JoinRandomRoom();
    }
    
    public override void OnJoinedRoom() {
        int numbers = PhotonNetwork.CurrentRoom.PlayerCount;
        PlayerPrefs.SetInt("index",numbers);
        networkText.text = "Room Joined :)";
        PhotonNetwork.LoadLevel("MovementTest");
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        networkText.text = "Create new room...";
        CreateRoom();
    }

    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(null, roomOptions, null);
    }
}
}
