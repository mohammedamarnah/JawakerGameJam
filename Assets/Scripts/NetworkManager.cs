using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [Tooltip("Player Prefab")]
    public String playerShooterPrefab;
    public String playerDefenderPrefab;

    // Start is called before the first frame update
    void Start()
    {
       //if (PlayerPrefs.GetInt("index") == 1) {
         PhotonNetwork.Instantiate(playerShooterPrefab, new Vector3(5f,5f,0f), Quaternion.identity, 0);
       //}
       //else {
         // PhotonNetwork.Instantiate(this.playerDefenderPrefab.name, new Vector3(5f,5f,0f), Quaternion.identity, 0);
       //}
    }
    
}
