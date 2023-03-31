using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Bullet : MonoBehaviour, IPoolableObject
{
    [SerializeField] ParticleSystem hitPS;
    [SerializeField] ParticleSystem trailPS;
    public Transform originalParent;
    public GameObject visual;
    public int tweenID;

    [SerializeField] CircleCollider2D col;
    [SerializeField] Light2D bulletLight;

    private void OnEnable()
    {
        originalParent = transform.parent;
    }
    public void OnObjectSpawn()
    {
        visual.SetActive(true);
        col.enabled = true;
        bulletLight.enabled = true;

        trailPS.Play();
        transform.SetParent(null);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DisableBullet();
    }

    public void DisableBullet()
    {
        LeanTween.cancel(tweenID);

        col.enabled = false;
        visual.SetActive(false);

        hitPS.gameObject.transform.SetParent(null);
        hitPS.Play();
        gameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>().Stop();

        transform.SetParent(originalParent);
        LeanTween.moveLocal(gameObject, Vector3.zero, 0f);

        LeanTween.delayedCall(0.2f, () =>
        {
            bulletLight.enabled = false;
            hitPS.gameObject.transform.SetParent(gameObject.transform);
            LeanTween.moveLocal(hitPS.gameObject, Vector3.zero, 0f);
        });
    }
}
