using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

public class SolverUI : BaseUI {
	public Block[] Blocks;

	private Block SelectedBlock = null;
	private string id;
	private bool MarkingMode;

	public override void Show(){
		admobdemo.Instance.ShowBanner2 ();
		for (int i = 0; i < 81; i++) {
			Blocks [i].transform.localPosition = new Vector3 (Blocks [i % 9].transform.localPosition.x, Blocks [(i / 9) * 9].transform.localPosition.y, Blocks [i].transform.localPosition.z);
			Blocks [i].Init (this, new Number (i, 0));
		}
		MarkingMode = false;
	}

	public void OnPressBackHandler(){
		UIManager.Instance.CreateUI ("MainMenuUI");
		UIManager.Instance.DestroyUI ("SolverUI");
		admobdemo.Instance.ShowBanner1 ();
	}

	public void Update(){
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

	public void OnPressButtonSolve(){
		Number[] mp = new Number[81];
		for (int i = 0; i < 81; i++) {
			mp [i] = Blocks [i].number;
		}
		Solver sl = new Solver (mp);
		sl.SolveAny (0);
		mp = sl.GetAnswer ();
		for (int i = 0; i < 81; i++) {
			Blocks [i].SetNumber (mp [i].d, false);
		}
		if (SelectedBlock != null) {
			CheckIfSelectNumber (SelectedBlock.number);
		}
		admobdemo.Instance.ShowFull ();
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

	}

	private void CheckIfSelectNumber(Number num){
		for (int i = 0; i < 81; i++) {
			Blocks [i].CheckIfSelectNumber (num);
		}
	}
}
