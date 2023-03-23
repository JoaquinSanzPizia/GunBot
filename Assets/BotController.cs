using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    private Vector3 direction;

    public Animator botAnim;
    public Animator handsAnim;

    void Awake()
    {

    }

    void FixedUpdate()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        botAnim.SetFloat("velX", xInput);
        botAnim.SetFloat("velY", yInput);

        handsAnim.SetFloat("velX", xInput);
        handsAnim.SetFloat("velY", yInput);

        direction = new Vector3(xInput, yInput, 0f);

        rb.MovePosition(transform.position + moveSpeed * Time.deltaTime * direction);
    }
}
