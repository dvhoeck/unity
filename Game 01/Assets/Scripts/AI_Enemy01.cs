using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            //effect = Instantiate(Fire01Prefab, FirePoint01.transform.position, Quaternion.identity);
            //effect = Instantiate(Fire01Prefab, FirePoint01.transform.position, Quaternion.Euler(0, 90, 0));

            // register player ship position at start of projectile life
            var player = GameObject.FindGameObjectsWithTag("Player").ToList().Where(taggedAsPlayer => taggedAsPlayer.name == "PlayerShip").SingleOrDefault();

            // get position of target object
            Vector3 targetPosition = player.transform.position;

            // calculate rotation to be done
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - FirePoint01.transform.position);

            effect = Instantiate(Fire01Prefab, FirePoint01.transform.position, targetRotation);






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
