using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Pathfinding;

public class Enemy : MonoBehaviour, IPoolableObject
{
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer model;
    [SerializeField] CircleCollider2D col;
    [SerializeField] ParticleSystem deathPS;

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    private Transform originalPos;
    private Material matInstance;

    [SerializeField] EnemyAI enemyAI;

    bool alive;

    void Start()
    {
        matInstance = new Material(model.sharedMaterial);
    }

    void OnDestroy()
    {
        Destroy(matInstance);
    }

    public void OnObjectSpawn()
    {
        enemyAI.enabled = true;

        currentHealth = maxHealth;
        originalPos = transform;
        alive = true;
        model.enabled = true;
        col.enabled = true;
        anim.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            GetHit();
        }
    }

    void GetHit()
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
        LeanTween.scale(gameObject, gameObject.transform.localScale * 1.1f, 0.1f).setLoopPingPong(1);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        enemyAI.enabled = false;
        alive = false;
        deathPS.Play();
        model.enabled = false;
        col.enabled = false;
        anim.enabled = false;

        LeanTween.delayedCall(0.5f, () => 
        {
            LeanTween.move(gameObject, Vector2.zero, 0f);
        });
    }
}
