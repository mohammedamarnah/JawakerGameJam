﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public Transform firePoint;
    public Bullet bullet;
    public float shootingSpeed;
    [SerializeField] private PhotonView _photonView;
    [SerializeField] public float timer = 0;

    void FixedUpdate() {
        timer += Time.deltaTime;
        if (_photonView.IsMine && timer > 5f) {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _photonView.RPC("Fire",RpcTarget.All,mousePosition);
        }
    }

    [PunRPC]
    public void Fire(Vector2 mousePosition) {
        Vector2 direction = (mousePosition - (Vector2) firePoint.position).normalized;
        firePoint.transform.up = direction;
        GameObject projectile = Instantiate(bullet.gameObject, new Vector2(firePoint.position.x,firePoint.position.y), firePoint.rotation);
       // projectile.transform.up = position;
        projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * shootingSpeed, ForceMode2D.Impulse);
    }
}
