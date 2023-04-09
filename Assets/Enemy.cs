using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Pathfinding;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IPoolableObject
{
    [SerializeField] enum EnemyType { melee, ranged};
    [SerializeField] EnemyType enemyType;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer model;
    [SerializeField] CircleCollider2D col;
    [SerializeField] ParticleSystem deathPS;

    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;

    [SerializeField] GameObject healthBarFill, healthBar;

    private Transform originalPos;
    private Material matInstance;

    [SerializeField] EnemyAI enemyAI;
    [SerializeField] EnemyShooter enemyShooter;

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
        if (enemyType == EnemyType.ranged)
        {
            enemyShooter.enabled = false;
            enemyShooter.player = FindObjectOfType<BotController>().gameObject;
        }
        enemyAI.enabled = true;
        
        currentHealth = maxHealth;
        healthBarFill.GetComponent<Image>().fillAmount = 1;

        healthBar.SetActive(true);
        originalPos = transform;
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

    public void Shoot()
    {
        if (enemyType == EnemyType.ranged)
        {
            enemyShooter.Shoot();
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
        healthBarFill.GetComponent<Image>().fillAmount = (1f / maxHealth) * currentHealth;
        LeanTween.scale(gameObject, gameObject.transform.localScale * 1.1f, 0.1f).setLoopPingPong(1);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (enemyType == EnemyType.ranged)
        {
            enemyShooter.player = null;
            enemyShooter.enabled = false;
        }
        healthBar.SetActive(false);
        enemyAI.enabled = false;
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
