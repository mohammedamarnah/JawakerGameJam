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
    public void Explode() {
      Debug.Log("Explode Called");
      explosion.transform.position = transform.position;
      explosion.gameObject.SetActive(true);
      explosion.GetComponent<AudioSource>().Play();
      explosion.Play();
      Destroy(this.gameObject);
      //Tahkeeeeeer Rasssssmmmmmmmyyyyy BazBoooooz?????
      if (GameObject.FindGameObjectsWithTag("Items").Length == 1) {
        Debug.Log("KanZZ");
      }
    }
}
