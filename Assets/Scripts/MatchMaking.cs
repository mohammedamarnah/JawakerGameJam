using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

namespace JawakerGameJam {
public class MatchMaking : MonoBehaviourPunCallbacks {
    [SerializeField] private Text soundsText,infoText;
    [SerializeField] private Button play, sounds;

    void Awake() {
        PhotonNetwork.ConnectUsingSettings();
        play.gameObject.SetActive(false);
        play.onClick.AddListener(() => {
            PhotonNetwork.JoinRandomRoom();
        });
        infoText.text = "Searching for room";
        sounds.onClick.AddListener(ToggleSound);
    }

    public void ToggleSound() {
        
    }
    
    public override void OnConnectedToMaster() {
        play.gameObject.SetActive(true);
        infoText.text = "Searching for room ..";
    }
    
    public override void OnJoinedRoom() {
        int numbers = PhotonNetwork.CurrentRoom.PlayerCount;
        PlayerPrefs.SetInt("index",numbers);
        PhotonNetwork.LoadLevel("MovementTest");
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        infoText.text = "Searching for room ...";
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
