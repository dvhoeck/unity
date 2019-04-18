using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject FirePoint;
    public List<GameObject> Vfx = new List<GameObject>();
    public RotateToMouse RotateToMouse;
    public GameObject Fire02HitEffect;
    public float Fire02MaxAmmoCount = 1;
    private float _fire02AmmoCount = 1;
    public float Fire02FireRate = 1;
    public float Fire02AmmoConsumption = 1;

    private List<GameObject> _casingEjectors;
    private GameObject _fire01EffectToSpawn;

    private float _timeToFire = 0.0f;
    private float _timeIsFiringFire02 = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        _fire01EffectToSpawn = Vfx[0];
        _casingEjectors = GameObject.FindGameObjectsWithTag("CasingEjection").ToList();

        // init ammo
        _fire02AmmoCount = Fire02MaxAmmoCount;
        SetAmmoCounterUI();
    }

    // Update is called once per frame
    private void Update()
    {
        

        // fire 1
        if (Input.GetMouseButton(0) && Time.time >= _timeToFire)
        {
            // create a delay between shots
            _timeToFire = Time.time + 1 / _fire01EffectToSpawn.GetComponent<ProjectileController>().FireRate;
            FireWeapon01();
        }

        
        FireWeapon02();
    }

    /// <summary>
    /// Fire weapon 1
    /// </summary>
    private void FireWeapon01()
    {
        GameObject effect;

        if (FirePoint != null)
        {
            // create a projectile, aimed at the target. ProjectileController script will move the shot and handle collisions
            effect = Instantiate(_fire01EffectToSpawn, FirePoint.transform.position, Quaternion.identity);
            if (RotateToMouse != null)
                effect.transform.localRotation = RotateToMouse.GetRotation();

            // determine when the muzzle FX ends so it can be destroyed
            var effectSystem = effect.GetComponent<ParticleSystem>();
            var timeToLive = 0.0f;
            if (effectSystem != null)
                timeToLive = effectSystem.main.duration;
            else
                timeToLive = effect.transform.GetChild(0).GetComponent<ParticleSystem>().main.duration;

            Destroy(effect, timeToLive);
        }
    }

    /// <summary>
    /// Fire weapon 2
    /// </summary>
    private void FireWeapon02()
    {
        var fire2 = Input.GetAxis("Fire2");
        GameObject hitObject = RotateToMouse.GetHitObject();

        // get fire02's audio (is a child of FirePoint)
        var audioSource = FirePoint.GetComponent<AudioSource>();

        // fire 02 shell ejection
        _casingEjectors.ForEach(ejector =>
        {
            var particleSystem = ejector.GetComponent<ParticleSystem>();
            var emission = particleSystem.emission;

            if (fire2 > 0 && hitObject != null && _fire02AmmoCount > 0)
                emission.enabled = true;
            else
                emission.enabled = false;
        });

        // stop audio and return from method is Fire02 trigger is released
        if (fire2 <= 0)
        {
            audioSource.Stop();
            return;
        }

        // fire weapon
        if (FirePoint != null && RotateToMouse != null && hitObject != null && _fire02AmmoCount > 0)
        {
            // create hit effect on target
            var effect = Instantiate(Fire02HitEffect, RotateToMouse.GetHitObjectPosition(), Quaternion.FromToRotation(Vector3.up, RotateToMouse.GetHitObjectNormal()));
            effect.transform.localRotation = RotateToMouse.GetRotation();
            
            // make sure effect is cleaned up after 10 secs
            Destroy(effect, 10.0f);

            // deal damage
            hitObject?.GetComponent<AI_EnemyBase>().Hit(0.2f);

            
            // start fire02 audio, if it's not already playing
            if (!audioSource.isPlaying)
                audioSource.Play();

            // decrease Fire02AmmoCount by Fire02AmmoConsumptionPerSecond per second
            _timeIsFiringFire02 += Time.deltaTime;

            if (_timeIsFiringFire02 >= (1.0f / Fire02FireRate)) // if fireRate is 10, shoot 10 times per second
            {

                // decrease ammo by Fire02AmmoConsumption but do not go below zero
                if (_fire02AmmoCount > 0)
                    _fire02AmmoCount -= Fire02AmmoConsumption;
                else
                    _fire02AmmoCount = 0.0f;

                // reset time out counter
                _timeIsFiringFire02 = 0.0f;

                SetAmmoCounterUI();
            }
        }
        else // not hitting anything, stop gun sound
        {
            audioSource.Stop();
        }

        

        Debug.Log(_timeIsFiringFire02);
    }

    private void SetAmmoCounterUI()
    {
        // set ammo counter
        var scoreCounter = Camera.main.GetComponent<ScoreCounter>();
        scoreCounter.SetFire02Ammo(_fire02AmmoCount);
    }
}