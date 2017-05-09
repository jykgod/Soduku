using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class RightBottomNode : MonoBehaviour {

	void Awake(){
		transform.localPosition = new Vector3 ((float)Screen.width * UIManager.manualHeightDiv2 / Screen.height, -UIManager.manualHeightDiv2);
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		#if UNITY_EDITOR
		transform.localPosition = new Vector3 ((float)Screen.width * UIManager.manualHeightDiv2 / Screen.height, -UIManager.manualHeightDiv2);
		#endif
	}
}

