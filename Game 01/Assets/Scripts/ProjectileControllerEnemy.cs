using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;

public class ProjectileControllerEnemy: MonoBehaviour
{
    public int Speed = 5;
    public float FireRate = 20;
    public float WeaponDamage = 1.0f;
    public GameObject MuzzleFlashPrefab;
    public GameObject HitPrefab;
    public float ProjectileLifeTime = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (MuzzleFlashPrefab != null)
        {
            // spawn muzzle flash and face it forward
            var MuzzleFlash = Instantiate(MuzzleFlashPrefab, transform.position, Quaternion.identity);
            MuzzleFlash.transform.forward = gameObject.transform.forward;

            // get duration of muzzle flash particle system...
            var muzzleFlashParticleSystem = MuzzleFlash.GetComponent<ParticleSystem>();
            var timeToLive = 0.0f;
            if(muzzleFlashParticleSystem != null)
                timeToLive = muzzleFlashParticleSystem.main.duration;
            else
                timeToLive = MuzzleFlash.transform.GetChild(0).GetComponent<ParticleSystem>().main.duration;

            // ...and despawn muzzle flash after duration has expired
            Destroy(MuzzleFlash, timeToLive);
        }

        // destroy this projectile after ProjectileLifeTime seconds
        Destroy(gameObject, ProjectileLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Speed > 0)
            transform.position += transform.forward * (-Speed * Time.deltaTime); // forward is relative to the fire point's rotation
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Speed = 0;

    //    if (collision.gameObject.tag == "Player")
    //    {
    //        var contactPoint = collision.GetContact(0);

    //        var playerController = collision.gameObject.GetComponent<PlayerController>() ?? throw new InvalidOperationException("No player controller script found");

    //        playerController.PlayerIsHit();

                        
    //        var rotation = Quaternion.FromToRotation(Vector3.up, contactPoint.normal);
    //        var position = contactPoint.point;
    //        /*
    //        // damage contact (enemy)
    //        collision.gameObject.GetComponent<AI_EnemyBase>().Hit();
    //        */
    //        if (HitPrefab != null)
    //        {
    //            // spawn hit effect
    //            var hit = Instantiate(HitPrefab, position, rotation);

    //            // get duration of hit particle system...
    //            var hitParticleSystem = hit.GetComponent<ParticleSystem>();
    //            var timeToLive = 0.0f;
    //            if (hitParticleSystem != null)
    //                timeToLive = hitParticleSystem.main.duration;
    //            else
    //                timeToLive = hit.transform.GetChild(0).GetComponent<ParticleSystem>().main.duration;

    //            // ...and despawn hit effect after duration has expired
    //            Destroy(hit, timeToLive);
    //        }
            
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider collider)
    {
        Speed = 0;

        // DEBUG
        //Debug.Log(collider.gameObject.name);

        if (collider.gameObject.tag == "Player")
        {
            var contactPoint = collider.ClosestPointOnBounds(transform.position);

            var playerController = collider.gameObject.GetComponentInParent<PlayerController>() ?? throw new InvalidOperationException("No player controller script found");

            playerController.PlayerIsHit();


            var rotation = Quaternion.FromToRotation(Vector3.up, contactPoint);
            
            if (HitPrefab != null)
            {
                // spawn hit effect
                var hit = Instantiate(HitPrefab, contactPoint, rotation);

                // get duration of hit particle system...
                var hitParticleSystem = hit.GetComponent<ParticleSystem>();
                var timeToLive = 0.0f;
                if (hitParticleSystem != null)
                    timeToLive = hitParticleSystem.main.duration;
                else
                    timeToLive = hit.transform.GetChild(0).GetComponent<ParticleSystem>().main.duration;

                // ...and despawn hit effect after duration has expired
                Destroy(hit, timeToLive);
            }

            
        }

        Destroy(gameObject);
    }
}
