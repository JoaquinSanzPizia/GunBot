using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] int damageResistance;
    [SerializeField] int maxHp;
    [SerializeField] int currentHp;
    [SerializeField] GameObject[] drops;

    [SerializeField] ParticleSystem breakFX;

    void Start()
    {
        currentHp = maxHp;
    }

    void Update()
    {
        
    }

    void GetHit()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one, 0f);

        currentHp--;
        LeanTween.scale(gameObject, gameObject.transform.localScale * 1.1f, 0.1f).setLoopPingPong(1);

        if (currentHp <= 0)
        {
            Break();
        }
    }

    void Break()
    {
        breakFX.Play();
    }
}
