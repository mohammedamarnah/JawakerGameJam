using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementTest : MonoBehaviour {
    public float moveSpeed = 0.5f;
    public Camera mainCamera;
    public Rigidbody2D rb;
    public Weapon weapon;
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private Animator _animator;

    private Vector2 moveDirection, mousePosition,aimDirection;

    void Awake() {
      mainCamera = Camera.main;
    }
    void Update() {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        
        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    
    void FixedUpdate() {
      if (_photonView.IsMine) {
        float newYVelocity = moveDirection.y * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, newYVelocity);
        aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
       // rb.rotation = aimAngle;
      }
 
     // _animator.SetFloat("Walk",moveDirection.y * moveSpeed);
     //_animator.SetFloat("SideWalk",moveDirection.x * moveSpeed);
     if ((moveDirection.x * moveSpeed != 0) || (moveDirection.y * moveSpeed != 0)) {
       _animator.SetFloat("SideWalk",aimDirection.x);
       _animator.SetFloat("Walk",aimDirection.y);
     } else {
       _animator.SetFloat("SideWalk",0);
       _animator.SetFloat("Walk",0);
       Debug.Log("Aim : " + aimDirection.y);
       _animator.SetFloat("Aim",aimDirection.y);
       Debug.Log("SideAim : "+ aimDirection.x);
       _animator.SetFloat("SideAim",aimDirection.x);
     }

    }
}
