using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playermovement : MonoBehaviour {
    private PhotonView photonView;
    public float speed;
    private Rigidbody2D rb2d;   

    void Awake() {
        photonView = GetComponent<PhotonView>();
        rb2d = GetComponent<Rigidbody2D> ();
    }

    void FixedUpdate() {
        if (photonView.IsMine) {
            float moveHorizontal = Input.GetAxis ("Horizontal");
            float moveVertical = Input.GetAxis ("Vertical");
            Vector2 movement = new Vector2 (moveHorizontal, moveVertical);
            rb2d.AddForce (movement * speed);
            if (moveHorizontal == 0 && moveVertical == 0) {
                rb2d.velocity = Vector2.zero;
                rb2d.angularVelocity = 0f;
            }
            
            
        }
    }
}
