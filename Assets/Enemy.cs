using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour, IPoolableObject
{
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer model;
    [SerializeField] CircleCollider2D col;
    [SerializeField] Light2D botLight;
    [SerializeField] ParticleSystem deathPS;

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    public float speed = 2f;
    public float followDistance = 10;

    private Vector3 originalPos;
    private Transform player;
    private Vector3 target;
    private Material matInstance;

    bool alive;

    void Start()
    {
        matInstance = new Material(model.sharedMaterial);
    }

    void OnDestroy()
    {
        Destroy(matInstance);
    }

    private void Update()
    {
        Move();
    }

    public void OnObjectSpawn()
    {
        player = FindObjectOfType<BotController>().transform;

        currentHealth = maxHealth;
        originalPos = transform.position;
        alive = true;
        model.enabled = true;
        botLight.enabled = true;
        col.enabled = true;
    }

    void Move()
    {
        if (!alive) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance < followDistance)
        {
            target = player.position;
        }
        else
        {
            target = originalPos;
        }

        if (target != null)
        {
            Vector3 direction = target - transform.position;
            direction.Normalize();
            transform.position += direction * speed * Time.deltaTime;
            anim.SetBool("walking", true);
        }

        if (transform.position == originalPos)
        {
            anim.SetBool("walking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            LeanTween.cancel(gameObject);
            LeanTween.scale(gameObject, Vector3.one, 0f);

            //Material mat = model.sharedMaterial;


            matInstance.SetColor("_InsideTint", Color.white);
            model.sharedMaterial = matInstance;

            //MaterialPropertyBlock block = new MaterialPropertyBlock();
            //block.SetColor("_InsideTint", Color.white);
            //model.SetPropertyBlock(block);

            LeanTween.delayedCall(0.1f, () =>
            {
                //block.SetColor("_InsideTint", Color.clear);
                //model.SetPropertyBlock(block);

                matInstance.SetColor("_InsideTint", Color.clear);
                model.sharedMaterial = matInstance;
            });

            currentHealth--;
            LeanTween.scale(gameObject, gameObject.transform.localScale * 1.1f, 0.1f).setLoopPingPong(1);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        alive = false;
        deathPS.Play();
        model.enabled = false;
        botLight.enabled = false;
        col.enabled = false;

        LeanTween.delayedCall(0.5f, () => 
        {
            transform.position = originalPos;
        });
    }
}
