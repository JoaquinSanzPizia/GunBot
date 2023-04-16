using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] ParticleSystem shootMuzzle;
    public Sprite[] gunSprites;
    [SerializeField] GameObject shootPoint;
    [SerializeField] ObjectPooler pooler;
    [SerializeField] float range;
    [SerializeField] float bulletSpeed;
    public float attackSpeed;
    public int damage;

    public void Shoot()
    {
        shootMuzzle.Play();
        GameObject bullet = pooler.SpawnFromPool("Bullet01", shootPoint.transform.position, shootPoint.transform.rotation);
        Bullet bulletCs = bullet.GetComponent<Bullet>();

        bulletCs.damage = damage;

        bulletCs.tweenID = LeanTween.move(bullet, shootPoint.transform.position + (-shootPoint.transform.right * range), 1f / bulletSpeed).setOnComplete(() => 
        {
            bulletCs.DisableBullet();
        }).uniqueId;
    }

    public void ChangeSprite(bool isTop)
    {
        if (isTop)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = gunSprites[1];
            LeanTween.moveLocalY(shootPoint, 0f, 0f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = gunSprites[0];
            LeanTween.moveLocalY(shootPoint, 0.03f, 0f);
        }
    }
}
