using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Enemy01 : AI_EnemyBase
{
    public GameObject Fire01Prefab;
    public GameObject FirePoint01;
    public float Fire01FireDelayModifier;
    private float _timeToFire;

    public override void Attack()
    {
        if (Time.time >= _timeToFire)
        {
            _timeToFire = Time.time + 1 * Fire01FireDelayModifier;
            FireWeapon01();
        }
    }

    private void FireWeapon01()
    {
        GameObject effect;

        if (FirePoint01 != null)
        {
            effect = Instantiate(Fire01Prefab, FirePoint01.transform.position, Quaternion.identity);
            /*
            if (RotateToMouse != null)
            {
                effect.transform.localRotation = RotateToMouse.GetRotation();
            }
            var effectSystem = effect.GetComponent<ParticleSystem>();
            var timeToLive = 0.0f;
            if (effectSystem != null)
            {
                timeToLive = effectSystem.main.duration;
            }
            else
            {
                timeToLive = effect.transform.GetChild(0).GetComponent<ParticleSystem>().main.duration;
            }
            Destroy(effect, timeToLive);
            */
        }
    }
}
