using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectLevelComponentUI : BaseUI {
	public UILabel 					Label;
	public GameObject 				CompeletMark;
	private int						index;
	private int 					fatherIndex;
	private List<LevelConfigure> 	configure;
	private string 					id;
	public override void Show(){
		NGUITools.SetActive (CompeletMark, false);
	}

	public void Init(List<LevelConfigure> configure ,int index,int fatherIndex){
		this.configure = configure;
		Label.text = index.ToString();
		this.index = index;
		id = fatherIndex.ToString () + "-" + index.ToString ();
		int fastTime = PlayerPrefs.GetInt (id, -1);
		if (fastTime != -1) {
			NGUITools.SetActive (CompeletMark, true);
		}
	}

	public void OnPressEnterHandler(){
		UIManager.Instance.CreateUI ("GameUI");
		(UIManager.Instance.GetUI ("GameUI")as GameUI).Init (configure [index].Numbers, id);
		UIManager.Instance.DestroyUI ("SelectLevelUI");
	}
}
