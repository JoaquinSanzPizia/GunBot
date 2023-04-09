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

    public void Shoot()
    {
        shootMuzzle.Play();
        GameObject bullet = pooler.SpawnFromPool("Bullet01", shootPoint.transform.position, shootPoint.transform.rotation);
        Bullet bulletCs = bullet.GetComponent<Bullet>();

        Vector3 direction = (player.transform.position - gameObject.transform.position).normalized;

        bulletCs.tweenID = LeanTween.move(bullet, shootPoint.transform.position + (direction * shootRange), 1f / bulletSpeed).setOnComplete(() =>
        {
            bulletCs.DisableBullet();
        }).uniqueId;
    }
}
