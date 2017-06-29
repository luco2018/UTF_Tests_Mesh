using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMesh : MonoBehaviour
{
	List<Vector3> newVertices = new List<Vector3> ();
	List<Vector2> newUV = new List<Vector2> ();
	List<Color> newVertCols = new List<Color> ();
	List<int> newTriangles = new List<int> ();

	MeshFilter mf;
	MeshRenderer mr;
	public Material mat;

	public bool UVs;
	public bool vertCol;

	void Start ()
	{
		mf = gameObject.AddComponent <MeshFilter> ();
		mr = gameObject.AddComponent<MeshRenderer> ();
		mr.material = mat;
		Mesh mesh = new Mesh ();
		mf.mesh = mesh;


		AddTri (transform.position);

		mesh.vertices = newVertices.ToArray ();
		mesh.uv = newUV.ToArray ();
		mesh.triangles = newTriangles.ToArray ();

		if (vertCol)
			mesh.colors = newVertCols.ToArray ();

	}



	void AddTri (Vector3 pos)
	{
		
		for (int i = 0; i < 3; i++) {
			Vector3 vertPos = Vector3.up;
			Quaternion rot = Quaternion.AngleAxis (-120f * i, Vector3.forward);

			vertPos = rot * vertPos;
			vertPos += pos;
			Debug.Log (vertPos.ToString ());
			newVertices.Add (vertPos);

			if (vertCol) {
				Color c = Color.HSVToRGB (i / 3f, 1f, 1f);
				newVertCols.Add (c);
			}

			newTriangles.Add (i);
		}



	}

}
