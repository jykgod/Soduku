using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;public class UIManager : MonoBehaviour
{
	private Dictionary<String,BaseUI> dic;
	[NonSerialized]
	public static UIManager Instance = null;

	public Transform Center 		= null;
	public Transform RightTop 		= null;
	public Transform LeftBottom 	= null;
	public Transform RightBottom 	= null;
	public Transform LeftTop 		= null;
	public UIRoot	 Root2D			= null;

	[NonSerialized]
	public 	static  float defaultWidth 		= 9;
	[NonSerialized]
	public 	static 	float defaultHeight 	= 16;
	[NonSerialized]
	public 	static  float defaultZ3D		= 1920;
	[NonSerialized]
	public 	static  float manualHeightDiv2	= 360;
	[NonSerialized]
	public static float screenRationToNormal = 0;
	private float scale;
	private float scaleX;
	private float scaleY;
	private float scaleX3D;
	private float scaleY3D;
	private float scaleZ;
	private float realScale3D;
	//下面两个参数仅用于计算屏幕坐标系到逻辑坐标系的转换。
	private float realScaleX;
	private float realScaleY;
	[NonSerialized]
	public float logicWidth;
	[NonSerialized]
	public float logicHeight;
	int count = 0;
		
	void Awake(){
		Time.fixedDeltaTime = 0.02f;
		Application.targetFrameRate = 45;
		Input.multiTouchEnabled = false;
		if (Instance == null) {
			Instance = this;
			dic = new Dictionary<String,BaseUI> ();
			DontDestroyOnLoad (gameObject);
			Resources.Load("Prefabs/UI/GameUI");
			Resources.Load("Prefabs/UI/SolverUI");
		} else {
			Instance.Center = Center;
			Instance.RightTop = RightTop;
			Instance.LeftBottom = LeftBottom;
			Instance.RightBottom = RightBottom;
			Instance.LeftTop = LeftTop;
			Instance.Root2D = Root2D;
			Destroy (gameObject);
		}
		UIManager.Instance.ClearUI ();
		count = 0;
//		scaleX = (float)Screen.width / defaultWidth;
//		scaleY = (float)Screen.height / defaultHeight;
//		scale  = (float)Screen.height / defaultHeight;
		scaleY = 1;
		scaleX = (float)(Screen.width * defaultHeight) / (defaultWidth * Screen.height);
		screenRationToNormal = scaleX;
		scaleX3D = (float)Screen.width / defaultWidth;
		scaleY3D = (float)Screen.height / defaultHeight;
		realScale3D = Mathf.Max (scaleX3D, scaleY3D);
		realScaleY = defaultHeight / Screen.height;
		realScaleX = realScaleY;
		logicWidth = realScaleX * Screen.width;
		logicHeight = defaultHeight;
		Resources.UnloadUnusedAssets ();
	}

	public Vector3 GetLogicPositionFromScreenPosition(Vector3 v3){
		return new Vector3 (v3.x * realScaleX, v3.y * realScaleY, v3.z);
	}

	public void CreateUI(String UIName){
		CreateUI (UIName, Center);
	}

	public Boolean CreateUI(String UIName,Transform parentTrans,bool movePos = true){

		if (dic.ContainsKey (UIName)) {
			if (dic [UIName] != null) {
				//LogHelper.Instance.LogError ("Create UI faild : " + UIName + " cannot be created repeatedly!");
				dic [UIName].transform.parent = parentTrans;
				dic [UIName].Show ();
				return true;
			} else {
				dic.Remove (UIName);
			}
		}

		GameObject _gameObject = (GameObject)Instantiate(Resources.Load("Prefabs/UI/" + UIName));

		if (_gameObject == null) {
			return false;
		}

		BaseUI baseUI = _gameObject.GetComponent<BaseUI> ();

		if (baseUI == null) {
			Destroy (_gameObject);
			Resources.UnloadUnusedAssets ();
			return false;
		}

		dic.Add (UIName,baseUI);

		count ++;


		Vector3 tempPosition = _gameObject.transform.localPosition;
		_gameObject.transform.SetParent (parentTrans);
//		_gameObject.transform.localScale = new Vector3 (scaleX, scaleY, 1);
		_gameObject.transform.localScale = new Vector3 (1, 1, 1);
		if (movePos) {
			_gameObject.transform.localPosition = new Vector3 (scaleX * tempPosition.x, scaleY * tempPosition.y, tempPosition.z);
		}
		_gameObject.transform.localRotation = Quaternion.identity;
		//提高层级，有待改进
		UIWidget[] widgets = _gameObject.GetComponentsInChildren<UIWidget> ();
		for (int i = 0; i < widgets.Length; i++) {
			widgets [i].depth += count * 100;
		}

		baseUI.Show ();
		return true;
	}

	public void DestroyUI(String UIName){
		if (dic.ContainsKey (UIName) == false) {
//			LogHelper.Instance.LogError("Destroy UI faild : " + UIName + " not be created!");
			return;
		}

		BaseUI baseUI= dic [UIName];
		dic.Remove (UIName);
		if (baseUI != null) {
			Destroy (baseUI.gameObject);
//			Resources.UnloadUnusedAssets ();
		}
//		count --;

	}

	public void Show(String UIName){
		if (dic.ContainsKey (UIName) == false) {
//			LogHelper.Instance.LogError("Show UI faild : " + UIName + " not be created!");
			return;
		}
		dic [UIName].Show ();
	}

	public void Hide(String UIName){
		if (dic.ContainsKey (UIName) == false) {
//			LogHelper.Instance.LogError("Hide UI faild : " + UIName + " not be created!");
			return;
		}
		dic [UIName].Hide ();
	}

	public Boolean CreateComponentUI(String UIName,Transform parentTrans,int index = 0){
		
		string UIComponentName = UIName + index;

		if (dic.ContainsKey (UIComponentName)) {
			if (dic [UIComponentName] != null) {
				//LogHelper.Instance.LogError ("Create UI faild : " + UIName + " cannot be created repeatedly!");
				dic [UIComponentName].transform.parent = parentTrans;
				dic [UIComponentName].Show ();
				return true;
			} else {
				dic.Remove (UIComponentName);
			}
		}
		GameObject _gameObject = (GameObject)Instantiate(Resources.Load("Prefabs/ComponentUI/" + UIName));

		if (_gameObject == null) {
			return false;
		}

		_gameObject.name =	UIComponentName;
		BaseUI baseUI = _gameObject.GetComponent<BaseUI> ();

		if (baseUI == null) {
			Destroy (_gameObject);
			Resources.UnloadUnusedAssets ();
			return false;
		}
		dic.Add (UIComponentName,baseUI);

		count ++;
		Vector3 tempPosition = _gameObject.transform.localPosition;
		_gameObject.transform.SetParent (parentTrans);
		_gameObject.transform.localScale = Vector3.one;
		_gameObject.transform.localPosition = tempPosition;
		_gameObject.transform.localRotation = Quaternion.identity;

//		_gameObject.transform.localScale = new Vector3(scaleX,scaleY,1);
//		_gameObject.transform.localPosition = new Vector3(scaleX * tempPosition.x,scaleY * tempPosition.y,tempPosition.z);
		//提高层级，有待改进
		UIWidget[] widgets = _gameObject.GetComponentsInChildren<UIWidget> ();
		for (int i = 0; i < widgets.Length; i++) {
			widgets [i].depth += count * 100;
		}

		baseUI.Show ();

		return true;
	}

	public void DestroyComponentUI(String UIName,int index){
		UIName += index;
		DestroyUI (UIName);
	}

	public void ShowComponentUI(String UIName,int index){
		UIName += index;
		Show (UIName);
	}

	public void HideComponentUI(String UIName,int index){
		UIName += index;
		Hide (UIName);
	}

	public BaseUI GetUI(String UIName){
		return dic [UIName];
	}

	public bool ContainsUI(String UIName){
		return dic.ContainsKey (UIName);
	}

	public BaseUI GetComponentUI(String UIName,int index){
		return GetUI(UIName + index);
	}

	public void ClearUI(){
		dic.Clear ();
	}

	public void SetActive(GameObject obj,bool active){
		if (NGUITools.GetActive (obj) == active)
			return;
		NGUITools.SetActive (obj, active);
	}

	public void OnDestroy(){
	}

	void Update(){
//		TutorialLogicHelper.Instance.LogicUpdate ();
	}

	private float LastPauseTime = -1;
	public void OnApplicationPause(bool pauseStatus){
		if (pauseStatus) {
			LogicPause ();
		} else {
			LogicResume ();
		}
	}

	public void OnApplictionFocus(bool hasFocus){
		if (hasFocus) {
			LogicResume ();
		} else {
			LogicPause ();
		}
	}

	private void LogicPause(){
//		LogHelper.Instance.Log ("Pause;time:" + Time.time + ";realTime:" + Time.realtimeSinceStartup);
		LastPauseTime = Time.realtimeSinceStartup;
	}

	private void LogicResume(){
		LastPauseTime = -1;
	}
}

