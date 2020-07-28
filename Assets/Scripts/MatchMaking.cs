using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

namespace JawakerGameJam {
public class MatchMaking : MonoBehaviourPunCallbacks {
    [SerializeField] private Text soundsText;
    [SerializeField] private Button play, sounds;

    void Awake() {
        play.onClick.AddListener(() => PhotonNetwork.ConnectUsingSettings());
        sounds.onClick.AddListener(ToggleSound);
    }

    public void ToggleSound() {
        
    }
    
    public override void OnConnectedToMaster() {
        PhotonNetwork.JoinRandomRoom();
    }
    
    public override void OnJoinedRoom() {
        int numbers = PhotonNetwork.CurrentRoom.PlayerCount;
        PlayerPrefs.SetInt("index",numbers);
        PhotonNetwork.LoadLevel("MovementTest");
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
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
