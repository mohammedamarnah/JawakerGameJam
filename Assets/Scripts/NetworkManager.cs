using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [Tooltip("Player Prefab")]
    public GameObject playerShooterPrefab;
    public GameObject playerDefenderPrefab;

    // Start is called before the first frame update
    void Start()
    {
       //if (PlayerPrefs.GetInt("index") == 1) {
         PhotonNetwork.Instantiate(this.playerShooterPrefab.name, new Vector3(5f,5f,0f), Quaternion.identity, 0);
       //}
       //else {
         // PhotonNetwork.Instantiate(this.playerDefenderPrefab.name, new Vector3(5f,5f,0f), Quaternion.identity, 0);
       //}
    }
    
}
