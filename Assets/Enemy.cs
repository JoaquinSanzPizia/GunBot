using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Pathfinding;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IPoolableObject
{
    public enum EnemyType { melee, ranged};
    public EnemyType enemyType;
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
            enemyShooter.enabled = true;
            enemyShooter.player = FindObjectOfType<BotController>().gameObject;
            if (enemyShooter.canon) enemyShooter.canon.GetComponent<SpriteRenderer>().enabled = true;
        }

        enemyAI.enabled = true;
        enemyAI.spawnPos = transform.position;
        
        currentHealth = maxHealth;
        healthBarFill.GetComponent<Image>().fillAmount = 1;

        healthBar.SetActive(true);
        originalPos = transform;
        model.enabled = true;
        col.enabled = true;
        if (anim) anim.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            GetHit(other.gameObject.GetComponent<Bullet>().damage);
        }
    }

    public void Shoot()
    {
        if (enemyType == EnemyType.ranged)
        {
            enemyShooter.Shoot();

            if (anim) anim.SetTrigger("shoot");
        }
    }

    void GetHit(int damage)
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

        currentHealth -= damage;
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
            if (enemyShooter.canon) enemyShooter.canon.GetComponent<SpriteRenderer>().enabled = false;
        }

        healthBar.SetActive(false);
        enemyAI.enabled = false;
        deathPS.Play();
        model.enabled = false;
        col.enabled = false;
        if (anim) anim.enabled = false;

        LeanTween.delayedCall(0.5f, () => 
        {
            LeanTween.move(gameObject, Vector2.zero, 0f);
        });
    }
}
