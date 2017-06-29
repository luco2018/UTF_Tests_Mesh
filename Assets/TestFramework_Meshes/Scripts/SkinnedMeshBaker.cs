using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class SkinnedMeshBaker : MonoBehaviour
{

	public SkinnedMeshRenderer SMR;
	public Animator anim;
	MeshRenderer mr;
	MeshFilter mf;

	// Use this for initialization
	IEnumerator Start ()
	{

		mr = GetComponent<MeshRenderer> ();
		mf = GetComponent<MeshFilter> ();
		yield return new WaitForSeconds (1f);
		anim.enabled = true;
		StartCoroutine (BakeMeshes ());
	}

	IEnumerator BakeMeshes ()
	{
		int count = 6;

		CombineInstance[] combine = new CombineInstance[count];
		yield return new WaitForEndOfFrame ();
		int i = 0;
		while (i < count) {
			combine [i].mesh = new Mesh ();
			SMR.BakeMesh (combine [i].mesh);
			combine [i].transform = SMR.transform.localToWorldMatrix;
			i++;
			yield return new WaitForSeconds (0.1f);
		}
		mf.mesh = new Mesh ();
		mf.mesh.CombineMeshes (combine);
	}
}
