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
       if (PlayerPrefs.GetInt("index") == 1) {
         PhotonNetwork.Instantiate(playerShooterPrefab, new Vector3(2f,5f,0f), Quaternion.identity, 0);
       } else {
         PhotonNetwork.Instantiate(playerDefenderPrefab, new Vector3(2f,5f,0f), Quaternion.identity, 0);
       }
    }
    
}
