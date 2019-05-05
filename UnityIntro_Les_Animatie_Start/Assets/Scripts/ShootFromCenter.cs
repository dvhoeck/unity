using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShootFromCenter : MonoBehaviour
{
    [SerializeField]
    GameObject explosionEffect;

    [SerializeField]
    AudioClip muzzleFX;

    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the left mouse button is held down in this frame
        if (Input.GetMouseButtonDown(0)) {

            // play muzzle vf 
            audioSource.Stop();
            audioSource.clip = muzzleFX;
            audioSource.Play();

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100)) { // try to determine if something is under the cursor
                if(hit.transform.name.Contains("Enemy")) // if the thing we hit is a clone (instance) of an enemy prefab
                {
                    // destroy the object we hit
                    Destroy(hit.transform.gameObject);

                    // create an explosion effect at this object's position
                    GameObject explosionEffect = Instantiate(this.explosionEffect, hit.transform.position, hit.transform.rotation);

                    // get the duration of the longest particle system in our explosion to use it in our Destroy call
                    //float effectLifeTime = explosionEffect.GetComponentsInChildren<ParticleSystem>().Max(pSystem => pSystem.main.duration);
                    float effectLifeTime = 3.0f;

                    // using the duration of the longest animation in the effect, destroy the target we hit
                    Destroy(explosionEffect, effectLifeTime);
                }
            }
        }
    }
}
