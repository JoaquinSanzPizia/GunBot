using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    private Vector3 direction;

    public Animator botAnim;
    public Animator handsAnim;

    public float lookingAngle;

    public Vector2 lookDir;

    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;

    [SerializeField] int maxBattery;
    [SerializeField] int currentBattery;

    [SerializeField] Image healthBarFill;

    Material matInstance;
    SpriteRenderer model;

    private void Start()
    {
        model = gameObject.GetComponentInChildren<SpriteRenderer>();
        currentHealth = maxHealth;
        currentBattery = maxBattery;
        matInstance = new Material(model.sharedMaterial);
    }
    void OnDestroy()
    {
        Destroy(matInstance);
    }
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

    void TakeDamage(int damage)
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one, 0f);

        matInstance.SetColor("_InsideTint", Color.white);
        model.sharedMaterial = matInstance;

        LeanTween.delayedCall(0.1f, () =>
        {
            matInstance.SetColor("_InsideTint", Color.clear);
            model.sharedMaterial = matInstance;
        });

        currentHealth--;
        healthBarFill.GetComponent<Image>().fillAmount = (1f / maxHealth) * currentHealth;
        LeanTween.scale(gameObject, gameObject.transform.localScale * 1.1f, 0.1f).setLoopPingPong(1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(1);
        }
    }
}
