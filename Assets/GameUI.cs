using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class GameUI : BaseUI {
	public Block[] Blocks;
	public UILabel FastTime;
	public UILabel CurrentTime;
	public UISprite MarkingBtnSpr;
	public UIButton MarkingBtn;

	private Block SelectedBlock = null;
	private string id;
	private bool MarkingMode;
	private float startTime;

	public override void Show(){
		admobdemo.Instance.ShowBanner2 ();
		for (int i = 0; i < 81; i++) {
			Blocks [i].transform.localPosition = new Vector3 (Blocks [i % 9].transform.localPosition.x, Blocks [(i / 9) * 9].transform.localPosition.y, Blocks [i].transform.localPosition.z);
			Blocks [i].Init (this, new Number (i, 0));
		}
		MarkingMode = false;
	}

	public void Init(List<NumberStruct> numbers,string id){
		for (int i = 0; i < numbers.Count; i++) {
			Blocks [numbers [i].x * 9 + numbers [i].y].Init (this, new Number (numbers [i].x * 9 + numbers [i].y, numbers [i].v));
		}
		this.id = id;
		int fastTime = PlayerPrefs.GetInt (id, -1);
		if (fastTime == -1) {
			FastTime.text = "N/A";
		} else {
			FastTime.text = MathHelper.Instance.GetTime (fastTime);
		}
		startTime = Time.time;
	}

	public void OnPressBackHandler(){
		UIManager.Instance.CreateUI ("SelectLevelUI");
		int index = int.Parse (id.Substring (0, id.IndexOf ('-')));
		(UIManager.Instance.GetUI ("SelectLevelUI") as SelectLevelUI).init (index);
		UIManager.Instance.DestroyUI ("GameUI");
		admobdemo.Instance.ShowBanner1 ();
	}

	public void Update(){
		CurrentTime.text = MathHelper.Instance.GetTime ((int)(Time.time - startTime));
		if(Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape)){
			OnPressBackHandler ();
		}
	}

	public void SelectBlock(int i){
		if (SelectedBlock != null) {
			SelectedBlock.UnSelect ();
		}
		SelectedBlock = Blocks [i];
		CheckIfSelectNumber (SelectedBlock.number);
	}

	public void OnPressButton1(){
		SetSelectBlock (1);
	}
	public void OnPressButton2(){
		SetSelectBlock (2);
	}
	public void OnPressButton3(){
		SetSelectBlock (3);
	}
	public void OnPressButton4(){
		SetSelectBlock (4);
	}
	public void OnPressButton5(){
		SetSelectBlock (5);
	}
	public void OnPressButton6(){
		SetSelectBlock (6);
	}
	public void OnPressButton7(){
		SetSelectBlock (7);
	}
	public void OnPressButton8(){
		SetSelectBlock (8);
	}
	public void OnPressButton9(){
		SetSelectBlock (9);
	}
	public void OnPressButtonDel(){
		SetSelectBlock (0);
	}

	public void OnPressButtonMarking(){
		MarkingMode = !MarkingMode;
		if (MarkingMode) {
			MarkingBtn.normalSprite = "Btn2";
			//MarkingBtnSpr.spriteName = "Btn2";
		} else {
			MarkingBtn.normalSprite = "Btn1";
//			MarkingBtnSpr.spriteName = "Btn1";
		}
	}

	private void SetSelectBlock(int num){
		if (SelectedBlock == null) {
			return;
		}
		if (num != 0) {
			SelectedBlock.SetNumber (num, MarkingMode);
		} else {
			SelectedBlock.SetNumber (num, false);
		}
		CheckIfSelectNumber (SelectedBlock.number);
		if (CheckGameOver ()) {
			UIManager.Instance.CreateUI ("PopUpGameFinishUI");
			(UIManager.Instance.GetUI ("PopUpGameFinishUI") as PopUpGameFinishUI).Init (id, PlayerPrefs.GetInt (id,-1), (int)(Time.time - startTime));
		}
	}

	private void CheckIfSelectNumber(Number num){
		for (int i = 0; i < 81; i++) {
			Blocks [i].CheckIfSelectNumber (num);
		}
	}

	private bool[,] vx = new bool[9,9];
	private bool[,] vy = new bool[9,9];
	private bool[,] vz = new bool[9,9];
	public bool CheckGameOver(){
		for(int i=0;i<9;i++){
			for(int j=0;j<9;j++){
				vx [i, j] = vy [i, j] = vz [i, j] = false;
			}
		}
		for (int i = 0; i < 81; i++) {
			if (Blocks [i].MarkingMode == true || Blocks [i].number.d  == 0) {
				return false;
			}
			if (vx [Blocks [i].number.x, Blocks [i].number.d - 1] == true || vy [Blocks [i].number.y, Blocks [i].number.d - 1] == true || vz [Blocks [i].number.z, Blocks [i].number.d - 1] == true) {
				return false;
			}
			vx [Blocks [i].number.x, Blocks [i].number.d - 1] = true;
			vy [Blocks [i].number.y, Blocks [i].number.d - 1] = true;
			vz [Blocks [i].number.z, Blocks [i].number.d - 1] = true;
		}
		return true;
	}
}
