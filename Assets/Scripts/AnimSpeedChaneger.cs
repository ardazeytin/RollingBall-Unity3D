using UnityEngine;
using System.Collections;

public class AnimSpeedChaneger : MonoBehaviour {

    private Animator anim;
    public float animSpeed;
    public new GameObject gameObject;

	// Use this for initialization
	void Start ()
    {
        anim = FindObjectOfType<Animator>();
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        anim.speed = animSpeed;
	}
}
