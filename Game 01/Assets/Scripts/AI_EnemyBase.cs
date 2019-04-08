using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Effects;

public class AI_EnemyBase : MonoBehaviour
{
    public int HitPoints;
    public float Speed;
    public GameObject ExplosionPrefab;

    private float _headingHoldTimer;
    private float _lastDirectionChange;
    private float _upDirectionModifier;
    private float _rightDirectionModifier;
    

    // Start is called before the first frame update
    void Start()
    {
        _upDirectionModifier = 1.0f;
        _rightDirectionModifier = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // move
        DoMove();

        // attack player;
        Attack();
    }

    public virtual void DoMove()
    {
        // rotate object a little bit every frame
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

        // make it move around on X and Y axis
        if (Speed > 0)
        {
            if (Time.time > _lastDirectionChange + _headingHoldTimer)
            {
                _upDirectionModifier = UnityEngine.Random.Range(-1.0f, 1.0f);
                _rightDirectionModifier = UnityEngine.Random.Range(-1.0f, 1.0f);

                _headingHoldTimer = UnityEngine.Random.Range(1.0f, 3.0f);
                _lastDirectionChange = Time.time;
            }

            // Vector3 is used to ignore the object's rotation
            transform.position += Vector3.up * (Speed * _upDirectionModifier * Time.deltaTime);
            transform.position += Vector3.right * (Speed * _rightDirectionModifier * Time.deltaTime);
        }
    }

    public virtual void Attack()
    {
        
    }

    

    public virtual void Hit(int damage = 1)
    {
        HitPoints -= damage;
        if(HitPoints <= 0)
        {
            if (ExplosionPrefab != null)
            {
                // spawn hit effect
                var hit = Instantiate(ExplosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
                var explosionPhysics = hit.GetComponent<ExplosionPhysicsForce>();
                explosionPhysics.enabled = false;

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
            else
                throw new MissingComponentException("AI_Enemy_Base hit method is missing its hit prefab");

            Destroy(gameObject);
            
            // 1 kill = 300 points
            Camera.main.GetComponent<ScoreCounter>().AddScore(300);
        }
    }
}
