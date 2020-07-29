using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;

public class ItemController : MonoBehaviour {
    [SerializeField] public ParticleSystem explosion;
    [SerializeField] private Vector2 spawnPoint;
    [NonSerialized] public float health = 100.0f;

    private float hitFactor = 2.5f;

    void Awake() {
      if(explosion == null)
        explosion = FindObjectOfType<NetworkManager>().explosion;
    }

    public void GetInjured() {
      if (PhotonNetwork.IsMasterClient) {
        health -= hitFactor;
        if (health <= 0) {
          GetComponent<PhotonView>().RPC("Explode",RpcTarget.All);

         // Explode();
        }
      }
    }

    [PunRPC]
    public void NextMap() {
      if (PhotonNetwork.IsMasterClient) {
        GameObject[] oldNPC = GameObject.FindGameObjectsWithTag("Hostage");
        foreach (var npc in oldNPC) {
          PhotonNetwork.Destroy(npc);
        }
      }
      FindObjectOfType<NetworkManager>().NextMap();
    }

    [PunRPC]
    public void Explode() {
      explosion.transform.position = transform.position;
      explosion.gameObject.SetActive(true);
      explosion.GetComponent<AudioSource>().Play();
      explosion.Play();
      if (PhotonNetwork.IsMasterClient && GameObject.FindGameObjectsWithTag("Items").Length > 1) {
       PhotonNetwork.Destroy(gameObject);
      }
      //Tahkeeeeeer Rasssssmmmmmmmyyyyy BazBoooooz?????
      if (GameObject.FindGameObjectsWithTag("Items").Length <= 1) {
        if (PhotonNetwork.IsMasterClient) {
          GetComponent<PhotonView>().RPC("NextMap",RpcTarget.All);
          PhotonNetwork.Destroy(gameObject);
        }
      }
    }
}
