using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	public UILabel DataLabel;
	public UILabel[] LittleDatas;
	public GameObject LittleObj;
	public GameObject Mark;
	public GameObject Select;
	public Number number;
	public bool MarkingMode;

	private UIButton LabelBtn;
	private UISprite MarkSpr;
	private GameUI gameUI = null;
	private SolverUI solverUI = null;
	private bool InitHasNumber;

	public void Init(GameUI gameUI,Number num){
		this.number = num;
		this.gameUI = gameUI;
		if (this.number.d != 0) {
			InitHasNumber = true;
			DataLabel.text = this.number.d.ToString();
		} else {
			InitHasNumber = false;
			DataLabel.text = "";
		}
		NGUITools.SetActive (Select, false);
		NGUITools.SetActive (LittleObj, false);
		NGUITools.SetActive (Mark, false);
		MarkSpr = Mark.GetComponent<UISprite> ();
		for (int i = 0; i < 9; i++) {
			LittleDatas [i].text = "";
		}
		LabelBtn = DataLabel.GetComponent<UIButton> ();
	}

	public void Init(SolverUI solverUI,Number num){
		this.number = num;
		this.solverUI = solverUI;
		if (this.number.d != 0) {
			InitHasNumber = true;
			DataLabel.text = this.number.d.ToString();
		} else {
			InitHasNumber = false;
			DataLabel.text = "";
		}
		NGUITools.SetActive (Select, false);
		NGUITools.SetActive (LittleObj, false);
		NGUITools.SetActive (Mark, false);
		MarkSpr = Mark.GetComponent<UISprite> ();
		for (int i = 0; i < 9; i++) {
			LittleDatas [i].text = "";
		}
		LabelBtn = DataLabel.GetComponent<UIButton> ();
	}

	public void CheckIfSelectNumber(Number num){
		if (num.d != 0 && num.d == number.d && MarkingMode == false) {
			NGUITools.SetActive (Mark, true);
			MarkSpr.color = Color.white;
			MarkSpr.spriteName = "bg6";
		} else {
			if (number.x == num.x || number.y == num.y || number.z == num.z) {
				MarkSpr.spriteName = "选择高亮背景";
				NGUITools.SetActive (Mark, true);
				MarkSpr.color = new Color (0f / 255, 174f / 255, 116f / 255);
			} else {
				NGUITools.SetActive (Mark, false);
			}
		}
	}

	public void OnPressHandler(){
		if (gameUI != null) {
			gameUI.SelectBlock (number.index);
		} else {
			solverUI.SelectBlock (number.index);
		}
		NGUITools.SetActive (Select, true);
	}

	public void UnSelect(){
		NGUITools.SetActive (Select, false);
	}

	public void SetNumber(int num,bool MarkingMode){
		if (InitHasNumber == false) {
			this.number.d = num;	
		} else {
			return;
		}
		this.MarkingMode = MarkingMode;
		if (MarkingMode == false) {
			NGUITools.SetActive (LittleObj, false);
			if (num != 0) {
				DataLabel.text = num.ToString ();
				DataLabel.color = new Color (0 / 255, 223f / 255, 255f / 255);
				LabelBtn.defaultColor = DataLabel.color;
				LabelBtn.hover = DataLabel.color;
				LabelBtn.pressed = DataLabel.color;
			} else {
				DataLabel.text = "";
			}
			for (int i = 0; i < 9; i++) {
				LittleDatas [i].text = "";
			}
		} else {
			NGUITools.SetActive (LittleObj, true);
//			NGUITools.SetActive (Mark, false);
			DataLabel.text = "";
			LittleDatas [num - 1].text = num.ToString ();
			LittleDatas [num - 1].color = new Color (223f / 255, 255f / 255, 255f / 255);
		}
	}
}
