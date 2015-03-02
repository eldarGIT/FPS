using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

    public GameObject explosion;
    public float timeout = 3.0f;

	// Use this for initialization
	void Start ()
    {
        Invoke("KillRocket", timeout);
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Instantiate(explosion, contact.point, rotation);

        KillRocket();
    }

    private void KillRocket()
    {
        ParticleEmitter emitter = GetComponentInChildren<ParticleEmitter>();

        if (emitter != null)
        {
            emitter.emit = false;
        }

        transform.DetachChildren();

        Destroy(gameObject);
    }
}
