using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTest : MonoBehaviourPun {
    public float moveSpeed = 5f;
    public Camera mainCamera;
    public Rigidbody2D rb;
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private Animator _animator;

    private Vector2 moveDirection, mousePosition,movement;

    void Awake() {
      mainCamera = Camera.main;
      if (!photonView.IsMine) {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        if (gameObject.tag.Equals("defender")) {
          gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = true;

        }
      }
    }
  
    void Update() {
        if (_photonView.IsMine) { 
          movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");
            moveDirection = new Vector2(movement.x, movement.y).normalized;
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - (Vector2) transform.position).normalized;
            _animator.SetFloat("Horizontal",direction.x);
            _animator.SetFloat("Vertical",direction.y);
            _animator.SetFloat("Speed",movement.sqrMagnitude);
        }
    }
    
    void FixedUpdate() {
      if (_photonView.IsMine) {
        float newYVelocity = moveDirection.y * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, newYVelocity);
      }
    }
}
