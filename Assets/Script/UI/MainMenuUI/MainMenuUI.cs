using UnityEngine;
using System.Collections;

public class MainMenuUI : BaseUI {
	public UILabel 			TitleLabel;
	public override void Show(){
		TitleLabel.transform.parent.localPosition = new Vector3 (0, UIManager.Instance.LeftTop.localPosition.y - TitleLabel.height / 2 - 80, 0);
	}

	public void OnPressPlayButtonHandler(){
		UIManager.Instance.CreateUI ("SelectDiffUI");
		UIManager.Instance.DestroyUI ("MainMenuUI");
	}

	public void OnPressPlaySolverHandler(){
		UIManager.Instance.CreateUI ("SolverUI");
		UIManager.Instance.DestroyUI ("MainMenuUI");
	}
}
