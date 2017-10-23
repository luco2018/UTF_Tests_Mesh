using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimRandomStart : MonoBehaviour
{

	Animator anim;

	// Use this for initialization
	void Awake ()
	{
		anim = GetComponent<Animator> ();
		anim.Play ("HumanoidRun", 0, Random.Range (0f, 1f));
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
