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

    public float lookingAngle;

    public Vector2 lookDir;

    void FixedUpdate()
    {
        Move();

        LookAtMouse();
    }

    void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        direction = new Vector3(xInput, yInput, 0f);

        rb.MovePosition(transform.position + moveSpeed * Time.deltaTime * direction);

        if (direction != Vector3.zero)
        {
            botAnim.SetBool("moving", true);
            handsAnim.SetBool("moving", true);
        }
        else
        {
            botAnim.SetBool("moving", false);
            handsAnim.SetBool("moving", false);
        }
    }

    void LookAtMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        lookingAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (lookingAngle > 45f && lookingAngle < 135)
        {
            lookDir = new Vector2(0, 1);
        }

        if (lookingAngle > 135f && lookingAngle < 180f || lookingAngle > -180f && lookingAngle < -135f)
        {
            lookDir = new Vector2(-1, 0);
        }

        if (lookingAngle > -135f && lookingAngle < -45f)
        {
            lookDir = new Vector2(0, -1);
        }

        if (lookingAngle > 0f && lookingAngle < 45f || lookingAngle > -45f && lookingAngle < 0f)
        {
            lookDir = new Vector2(1, 0);
        }

        botAnim.SetFloat("mouseX", lookDir.x);
        botAnim.SetFloat("mouseY", lookDir.y);

        handsAnim.SetFloat("mouseX", lookDir.x);
        handsAnim.SetFloat("mouseY", lookDir.y);
    }
}
