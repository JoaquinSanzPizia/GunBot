using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] ParticleSystem shootMuzzle;
    [SerializeField] public GameObject player;
    [SerializeField] GameObject shootPoint;
    [SerializeField] ObjectPooler pooler;
    [SerializeField] float bulletSpeed;
    [SerializeField] float shootRange;
    [SerializeField] float attackSpeed;
    [SerializeField] int damage;

    public GameObject canon;
    [SerializeField] float lookingAngle;
    [SerializeField] bool isCanon;

    bool canShoot;
    private void OnEnable()
    {
        player = FindObjectOfType<BotController>().gameObject;
        canShoot = true;
    }
    private void FixedUpdate()
    {
        ShootCR();
    }
    public void Shoot()
    {
        canShoot = false;
        Debug.Log("Shoot");;
        shootMuzzle.Play();
        GameObject bullet = pooler.SpawnFromPool("Bullet01", shootPoint.transform.position, shootPoint.transform.rotation);
        Bullet bulletCs = bullet.GetComponent<Bullet>();
        bulletCs.damage = damage;

        Vector3 direction = (player.transform.position - gameObject.transform.position).normalized;

        bulletCs.tweenID = LeanTween.move(bullet, shootPoint.transform.position + (direction * shootRange), 1f / bulletSpeed).setOnComplete(() =>
        {
            bulletCs.DisableBullet();
        }).uniqueId;

        LeanTween.delayedCall((1f / attackSpeed), () => 
        {
            canShoot = true;
        });
    }

    void ShootCR()
    {
        if (gameObject.GetComponentInParent<EnemyAI>().playerNearby)
        {
            if (canShoot) Shoot();

            if (isCanon)
            {
                Vector3 aimDirection = (player.transform.position - transform.position).normalized;
                lookingAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
                canon.transform.eulerAngles = new Vector3(0f, 0f, lookingAngle);
            }
        }
    }
}
