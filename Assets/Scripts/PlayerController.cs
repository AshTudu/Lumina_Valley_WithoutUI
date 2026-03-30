using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D theRB;
    [SerializeField] private float moveSpeed = 10f;

    private void Start()
    {
        
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized * Time.deltaTime * moveSpeed;

        theRB.velocity = moveDirection;
    }

}
