using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct item {
  public GameObject itemObject;
  public GameObject spawnPoint;
}
public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Tooltip("Player Prefab")]
    public String playerShooterPrefab;
    public String playerDefenderPrefab;
    [SerializeField] NPCPath[] Pointers;
    [SerializeField] NPCPath[] Pointers2;
    [SerializeField] private Transform GoodGuySpwaner;
    [SerializeField] private Transform BadGuySpwaner;
    [SerializeField] private item[] items;
    [SerializeField] private item[] itemsMap2;

    [SerializeField] private GameObject itemsContainer;
    [SerializeField] public ParticleSystem explosion;
    [SerializeField] public GameObject Map0;
    [SerializeField] public GameObject Map1;
    
    

    // Start is called before the first frame update
    void Start() {
      if (PhotonNetwork.IsMasterClient) {
        // NextMap();
        foreach (var item in items) {
          GameObject i =
            PhotonNetwork.InstantiateSceneObject(item.itemObject.name, item.spawnPoint.transform.position,Quaternion.identity);
          i.GetComponent<ItemController>().explosion = explosion;
        }
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
       GameObject player = PhotonNetwork.Instantiate(playerShooterPrefab, GoodGuySpwaner.position, Quaternion.identity, 0);
       Camera.main.GetComponent<SmoothCamera2D>().target = player.transform;
      } else {
        GameObject player = PhotonNetwork.Instantiate(playerDefenderPrefab, BadGuySpwaner.position, Quaternion.identity, 0);
        Camera.main.GetComponent<SmoothCamera2D>().target = player.transform;

      }
    }

    public void NextMap() {
      if (Map1.activeSelf) {
        LeaveRoom();
      }
      Map0.SetActive(false);
      Map1.SetActive(true);
      if (PhotonNetwork.IsMasterClient) {
        foreach (var item in itemsMap2) {
          GameObject i =
            PhotonNetwork.InstantiateSceneObject(item.itemObject.name, item.spawnPoint.transform.position,item.spawnPoint.transform.rotation);
          i.GetComponent<ItemController>().explosion = explosion;
        }
        foreach (var path in Pointers2) {
          Hostage npc = PhotonNetwork.InstantiateSceneObject(path.NPC.name, path.Paths[0].transform.position,
            path.Paths[0].transform.rotation).GetComponent<Hostage>();
          npc.pointers = path.Paths;
          npc.waitingTimeBeforeMove = path.waitingTimeBeforeMove;
          Hostage.currentHostages.Add(npc);
          npc.Movement();
        }
      }
    }
    
    public void RestartMap() {
      Map0.SetActive(true);
      Map1.SetActive(false);
      if (FindObjectOfType<Weapon>().gameObject.GetComponent<PhotonView>().IsMine) {
        FindObjectOfType<Weapon>().timer = 0;
        FindObjectOfType<Weapon>().transform.position = BadGuySpwaner.position;
      }

      if (GameObject.FindGameObjectWithTag("defender") != null && GameObject.FindGameObjectWithTag("defender").GetComponent<PhotonView>().IsMine) {
        GameObject.FindGameObjectWithTag("defender").transform.position = GoodGuySpwaner.position;
      }
      if (PhotonNetwork.IsMasterClient) {
        GameObject[] oldNPC = GameObject.FindGameObjectsWithTag("Hostage");
        foreach (var npc in oldNPC) {
          PhotonNetwork.Destroy(npc);
        }
        GameObject[] oldItems = GameObject.FindGameObjectsWithTag("Items");
        foreach (var item in oldItems) {
          PhotonNetwork.Destroy(item);
        }
        
        foreach (var item in items) {
          GameObject i =
            PhotonNetwork.InstantiateSceneObject(item.itemObject.name, item.spawnPoint.transform.position,item.spawnPoint.transform.rotation);
          i.GetComponent<ItemController>().explosion = explosion;
        }
        foreach (var path in Pointers) {
          Hostage npc = PhotonNetwork.InstantiateSceneObject(path.NPC.name, path.Paths[0].transform.position,
            path.Paths[0].transform.rotation).GetComponent<Hostage>();
          npc.pointers = path.Paths;
          npc.waitingTimeBeforeMove = path.waitingTimeBeforeMove;
          Hostage.currentHostages.Add(npc);
          npc.Movement();
        
        }
      }
    }

    public void LeaveRoom() {
      PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom() {
      Destroy(Component.FindObjectOfType<SoundManager>().gameObject);
      SceneManager.LoadScene(0);
    }

}
