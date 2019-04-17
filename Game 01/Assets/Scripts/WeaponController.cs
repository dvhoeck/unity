using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject FirePoint;
    public List<GameObject> Vfx = new List<GameObject>();
    public RotateToMouse RotateToMouse;
    public GameObject Fire02HitEffect;

    private List<GameObject> _casingEjectors;
    private GameObject _fire01EffectToSpawn;

    private float _timeToFire = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        _fire01EffectToSpawn = Vfx[0];
        _casingEjectors = GameObject.FindGameObjectsWithTag("CasingEjection").ToList();
    }

    // Update is called once per frame
    private void Update()
    {
        // fire 1
        if (Input.GetMouseButton(0) && Time.time >= _timeToFire)
        {
            _timeToFire = Time.time + 1 / _fire01EffectToSpawn.GetComponent<ProjectileController>().FireRate;
            FireWeapon01();
        }

        var fire2 = Input.GetAxis("Fire2");
        FireWeapon02(fire2);
    }

    private void FireWeapon01()
    {
        GameObject effect;

        if (FirePoint != null)
        {
            effect = Instantiate(_fire01EffectToSpawn, FirePoint.transform.position, Quaternion.identity);
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
        }
    }

    private void FireWeapon02(float fire2)
    {
        GameObject hitObject = RotateToMouse.GetHitObject();

        // fire 2 shell ejection
        _casingEjectors.ForEach(ejector =>
        {
            var particleSystem = ejector.GetComponent<ParticleSystem>();
            var emission = particleSystem.emission;

            if (fire2 > 0 && hitObject != null)
                emission.enabled = true;
            else
                emission.enabled = false;
        });

        if (fire2 <= 0)
            return;

        GameObject effect;

        
        
        if (FirePoint != null && RotateToMouse != null && hitObject != null)
        {
            effect = Instantiate(Fire02HitEffect, RotateToMouse.GetHitObjectPosition(), Quaternion.FromToRotation(Vector3.up, RotateToMouse.GetHitObjectNormal()));
            effect.transform.localRotation = RotateToMouse.GetRotation();

            hitObject?.GetComponent<AI_EnemyBase>().Hit(0.2f);

            //var effectSystem = effect.GetComponent<ParticleSystem>();
            //var timeToLive = 0.0f;
            //if (effectSystem != null)
            //{
            //    timeToLive = effectSystem.main.duration;
            //}
            //else
            //{
            //    timeToLive = effect.transform.GetChild(0).GetComponent<ParticleSystem>().main.duration;
            //}


            Destroy(effect, 10.0f);
        }
    }
}