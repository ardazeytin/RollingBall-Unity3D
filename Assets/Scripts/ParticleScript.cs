using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {

    private ParticleSystem partical1;

	// Use this for initialization
	void Start ()
    {
       partical1 = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!partical1.isPlaying)
        {
            Destroy(gameObject);
        }
	}
}
