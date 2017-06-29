using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRendererAPI : MonoBehaviour
{
	public Font font;
	public Matrix4x4 matrix;

	public enum Mode
	{
		isStaticBatched,
		isVisible,
		lightmapIndexScaleOffset,
		realtimeLightmapISO,
		loc2worlMatrix,
		worl2locMatrix
	}

	public Mode currentMode;

	// Use this for initialization
	IEnumerator Start ()
	{
		MeshRenderer mr = GetComponent<MeshRenderer> ();
		Color c = Color.blue;

		yield return new WaitForEndOfFrame ();

		switch (currentMode) {
		case Mode.isStaticBatched:
			c = mr.isPartOfStaticBatch ? Color.green : Color.red;
			CreateTextLabel ("Static with others");
			break;
		case Mode.isVisible:
			c = mr.isVisible ? Color.green : Color.red;
			CreateTextLabel ("isVisible");
			break;
		case Mode.lightmapIndexScaleOffset:
			CreateTextLabel (mr.lightmapIndex.ToString () + "\n" + mr.lightmapScaleOffset.ToString ());
			break;
		case Mode.realtimeLightmapISO:
			CreateTextLabel (mr.realtimeLightmapIndex.ToString () + "\n" + mr.realtimeLightmapScaleOffset.ToString ());
			break;
		case Mode.loc2worlMatrix:
			if (matrix == mr.localToWorldMatrix)
				c = Color.green;
			else
				c = Color.red;
			CreateTextLabel ("Local2WorldMatrix");
			break;
		case Mode.worl2locMatrix:
			if (matrix == mr.worldToLocalMatrix)
				c = Color.green;
			else
				c = Color.red;
			CreateTextLabel ("World2LocalMatrix");
			break;
		default:
			c = Color.blue;
			break;
		}

		mr.material.color = c;

	}

	[ContextMenu ("SetMatrix")]
	public void SaveMatrix ()
	{
		MeshRenderer mr = GetComponent<MeshRenderer> ();
		if (currentMode == Mode.loc2worlMatrix)
			matrix = mr.localToWorldMatrix;
		else if (currentMode == Mode.worl2locMatrix)
			matrix = mr.worldToLocalMatrix;
	}

	void CreateTextLabel (string label)
	{
		GameObject go = new GameObject (gameObject.name + "_label");
		go.transform.SetParent (gameObject.transform);
		go.transform.localPosition = Vector3.zero;
		go.transform.localScale = Vector3.one * 0.025f;
		TextMesh tm = go.AddComponent<TextMesh> ();
		tm.font = font;
		go.GetComponent<MeshRenderer> ().material = font.material;
		tm.fontSize = 40;
		tm.text = label;
		tm.alignment = TextAlignment.Center;
		tm.anchor = TextAnchor.MiddleCenter;
	}
}
