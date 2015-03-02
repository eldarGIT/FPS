using UnityEngine;
using System.Collections;

public class MachineGun : MonoBehaviour {

    public int bulletsPerClip = 30;
    public int clips = 5;
    public float fireRate = 0.05f;
    public float damageRange = 100.0f;
    public float force = 10.0f;
    public float reloadTime = 2.0f;
    public Renderer muzzleFlash;

    private int bulletsLeft = 0;
    private float nextFireTime = 0.0f;
    private ParticleEmitter hitParticles;
    private int lastFrameShot = -1;

	// Use this for initialization
	void Start ()
    {
        hitParticles = GetComponentInChildren<ParticleEmitter>();

        if (hitParticles != null)
        {
            hitParticles.emit = false;
        }

        bulletsLeft = bulletsPerClip;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void LateUpdate()
    {
        if (muzzleFlash != null)
        {
            if (lastFrameShot == Time.frameCount)
            {
                muzzleFlash.transform.localRotation = Quaternion.AngleAxis(Random.value * 360, Vector3.forward);
                muzzleFlash.enabled = true;

                if (audio != null)
                {
                    if (!audio.isPlaying)
                    {
                        audio.Play();
                    }
                    audio.loop = true;
                }
            }
            else
            {
                muzzleFlash.enabled = false;

                if (audio != null)
                {
                    audio.loop = false;
                }
            }
        }
    }

    public void Fire()
    {
        if (bulletsLeft == 0)
        {
            return;
        }

        if (Time.time - fireRate > nextFireTime)
        {
            nextFireTime = Time.time - Time.deltaTime;
        }

        while (nextFireTime < Time.time && bulletsLeft > 0)
        {
            FireOneShot();
            nextFireTime += fireRate;
        }
    }

    private void FireOneShot()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if ( Physics.Raycast(transform.position, direction, out hit, damageRange) ) 
        {
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForceAtPosition(force * direction, hit.point);
            }

            if (hitParticles != null)
            {
                hitParticles.transform.position = hit.point;
                hitParticles.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                hitParticles.Emit();
            }
        }

        lastFrameShot = Time.frameCount;

        bulletsLeft--;

        if (bulletsLeft == 0)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);

        if (clips > 0)
        {
            clips--;
            bulletsLeft = bulletsPerClip;
        }
    }
}
