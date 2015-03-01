using UnityEngine;
using System.Collections;

public class PlayerWeapons : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        SelectWeapon(0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire1"))
        {
            BroadcastMessage("Fire");
        }

	    if (Input.GetKeyDown("1"))
        {
            SelectWeapon(0);
        }
        else if (Input.GetKeyDown("2"))
        {
            SelectWeapon(1);
        }
	}

    private void SelectWeapon(int index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == index)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
