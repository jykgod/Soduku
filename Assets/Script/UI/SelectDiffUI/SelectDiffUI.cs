using UnityEngine;
using System.Collections;

public class SelectDiffUI : BaseUI {
	public UILabel 			TitleLabel;
	public override void Show(){
		TitleLabel.transform.parent.localPosition = new Vector3 (0, UIManager.Instance.LeftTop.localPosition.y - TitleLabel.height / 2 - 80, 0);
	}

	void Update(){
		if(Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape)){
			OnPressBackButtonHandler ();
		}	
	}

	public void OnPressBackButtonHandler(){
		UIManager.Instance.CreateUI ("MainMenuUI");
		UIManager.Instance.DestroyUI ("SelectDiffUI");
	}

	public void OnPressEasyButtonHandler(){
		UIManager.Instance.CreateUI ("SelectLevelUI");
		(UIManager.Instance.GetUI ("SelectLevelUI") as SelectLevelUI).init (0);
		UIManager.Instance.DestroyUI ("SelectDiffUI");
	}

	public void OnPressNormalButtonHandler(){
		UIManager.Instance.CreateUI ("SelectLevelUI");
		(UIManager.Instance.GetUI ("SelectLevelUI") as SelectLevelUI).init (1);
		UIManager.Instance.DestroyUI ("SelectDiffUI");
	}

	public void OnPressHardButtonHandler(){
		UIManager.Instance.CreateUI ("SelectLevelUI");
		(UIManager.Instance.GetUI ("SelectLevelUI") as SelectLevelUI).init (2);
		UIManager.Instance.DestroyUI ("SelectDiffUI");
	}
}
