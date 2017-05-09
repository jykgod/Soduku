using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectLevelUI : BaseUI {
	public UILabel 			TitleLabel;
	public UIPanel 			Panel;
	public Transform 		AddTo;
	public UIScrollView 	ScrowView;
//	public BoxCollider		Collider;
	private int 			index;
	private List<LevelConfigure> configure;
	public override void Show(){
		TitleLabel.transform.parent.localPosition = new Vector3 (0, UIManager.Instance.LeftTop.localPosition.y - TitleLabel.height / 2 - 80, 0);
		Panel.SetRect (0, 0, UIManager.Instance.RightBottom.localPosition.x * 2 / transform.localScale.x, Panel.height);
	}

	public void init(int index){
		this.index = index;
		if (index == 0) {
			configure = JsonManager.Instance.EasyConfigure;
		} else if (index == 1) {
			configure = JsonManager.Instance.NormalConfigure;
		} else {
			configure = JsonManager.Instance.HardConfigure;	
		}
		float pw = Panel.width;
		int cn = (int)(pw / 70);
		int sx = 35 - cn * 35;
		int sy = (int)(Panel.height / 2) - 35;
		for (int i = 0; i < configure.Count; i++) {
			UIManager.Instance.CreateComponentUI ("SelectLevelComponentUI", AddTo, i);
			SelectLevelComponentUI componentUI = UIManager.Instance.GetComponentUI ("SelectLevelComponentUI", i) as SelectLevelComponentUI;
			componentUI.Init (configure, i + 1, index);
			componentUI.transform.localPosition = new Vector3 (sx + (i % cn) * 70, sy - (i / cn) * 70, 0);
		}
//		Collider.size = new Vector3 (Panel.width, (configure.Count / cn + 1) * 70, 0);
//		Collider.center = new Vector3 (0, (Panel.height - Collider.size.y) / 2, 0);
		//ScrowView.ResetPosition ();
	}

	void Update(){
		if(Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape)){
			OnPressBackButtonHandler ();
		}	
	}

	public void OnPressBackButtonHandler(){
		UIManager.Instance.CreateUI ("SelectDiffUI");
		UIManager.Instance.DestroyUI ("SelectLevelUI");
	}

	void OnDestroy(){
		for (int i = 0; i < configure.Count; i++) {
			UIManager.Instance.DestroyComponentUI ("SelectLevelComponentUI", i);
		}
	}
}
