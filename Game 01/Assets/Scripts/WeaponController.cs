using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject FirePoint;
    public List<GameObject> Vfx = new List<GameObject>();
    public RotateToMouse RotateToMouse;


    private List<GameObject> _casingEjectors;
    private GameObject _effectToSpawn;
    private float _timeToFire = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _effectToSpawn = Vfx[0];
        _casingEjectors = GameObject.FindGameObjectsWithTag("CasingEjection").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        // fire 1
        if (Input.GetMouseButton(0) && Time.time >= _timeToFire)
        {
            _timeToFire = Time.time + 1 / _effectToSpawn.GetComponent<ProjectileController>().FireRate;
            FireWeapon01();
        }

        var fire2 = Input.GetAxis("Fire2");

        // fire 2 shell ejection
        _casingEjectors.ForEach(ejector =>
        {
            var particleSystem = ejector.GetComponent<ParticleSystem>();
            var emission = particleSystem.emission;

            if (fire2 > 0)
                emission.enabled = true;
            else
                emission.enabled = false;
        });
    }

    private void FireWeapon01()
    {
        GameObject effect;

        if (FirePoint != null)
        {
            effect = Instantiate(_effectToSpawn, FirePoint.transform.position, Quaternion.identity);
            if(RotateToMouse != null)
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
}
