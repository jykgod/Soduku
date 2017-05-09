using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class Center3DNode : MonoBehaviour{
	void Awake () {
		transform.localPosition = new Vector3 (0, 0, Screen.height / UIManager.defaultHeight * UIManager.defaultZ3D);
	}
	void Update () {
		#if UNITY_EDITOR
		transform.localPosition = new Vector3 (0, 0, Screen.height / UIManager.defaultHeight * UIManager.defaultZ3D);
		#endif
	}
}

