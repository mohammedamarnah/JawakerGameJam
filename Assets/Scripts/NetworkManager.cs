using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Tooltip("Player Prefab")]
    public String playerShooterPrefab;
    public String playerDefenderPrefab;
    [SerializeField] NPCPath[] Pointers;
    [SerializeField] private Transform GoodGuySpwaner;
    [SerializeField] private Transform BadGuySpwaner;

    // Start is called before the first frame update
    void Start() {
      if (PhotonNetwork.IsMasterClient) {
        Debug.Log("Iam Master");
        foreach (var path in Pointers) {
          Hostage npc = PhotonNetwork.InstantiateSceneObject(path.NPC.name, path.Paths[0].transform.position,
            path.Paths[0].transform.rotation).GetComponent<Hostage>();
          npc.pointers = path.Paths;
          npc.waitingTimeBeforeMove = path.waitingTimeBeforeMove;
          Hostage.currentHostages.Add(npc);
          npc.Movement();
        }
       
      }
      if (PlayerPrefs.GetInt("index") == 1) {
        PhotonNetwork.Instantiate(playerShooterPrefab, GoodGuySpwaner.position, Quaternion.identity, 0);
      } else {
        PhotonNetwork.Instantiate(playerDefenderPrefab, BadGuySpwaner.position, Quaternion.identity, 0);
      }
    }

    public void LeaveRoom() {
      PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom() {
      PhotonNetwork.DestroyAll();
      PhotonNetwork.LeaveRoom();
      PhotonNetwork.LoadLevel("Home");
      Debug.Log("Left Room");
    }
    
}
