using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [Tooltip("Player Prefab")]
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,5f,0f), Quaternion.identity, 0);
    }
    
}
