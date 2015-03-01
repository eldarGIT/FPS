using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour {

    public Rigidbody projectile;
    public int ammoCount = 15;
    public float reloadTime = 0.5f;
    public float initSpeed = 20.0f;

    private float lastShotTime = -10.0f;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Fire()
    {
        if ( Time.time > (reloadTime + lastShotTime) && ammoCount > 0 )
        {
            Rigidbody projectileInstance = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
            projectileInstance.velocity = transform.TransformDirection(new Vector3(0, 0, initSpeed));
            Physics.IgnoreCollision(projectileInstance.collider, transform.root.collider);
            lastShotTime = Time.time;
            ammoCount--;
        }
    }
}
