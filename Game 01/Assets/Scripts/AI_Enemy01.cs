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
            // get player ship
            var player = GameObject.FindGameObjectsWithTag("Player").ToList().Where(taggedAsPlayer => taggedAsPlayer.name == "PlayerShip").SingleOrDefault();

            // get position of player ship
            Vector3 targetPosition = player.transform.position;

            // calculate rotation to be done
            Quaternion targetRotation = Quaternion.LookRotation(targetPosition - FirePoint01.transform.position);

            // spawn fire01 prefab
            effect = Instantiate(Fire01Prefab, FirePoint01.transform.position, targetRotation);
        }
    }
}