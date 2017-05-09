using UnityEngine;
using System.Collections;

public class PopUpGameFinishUI : BaseUI {
	public UILabel UsedTimeLabel;
	public UILabel FastTimeLabel;
	private string id;

	public override void Show (){
		
	}

	public void Init(string id,int FastTime,int UsedTime){
		this.id = id;
		UsedTimeLabel.text = MathHelper.Instance.GetTime (UsedTime);
		if (FastTime == -1 || FastTime > UsedTime) {
			FastTimeLabel.text = UsedTimeLabel.text;
			PlayerPrefs.SetInt (id, UsedTime);
			return;
		}
		FastTimeLabel.text = MathHelper.Instance.GetTime (FastTime);
	}

	public void OnPressHandler(){
		UIManager.Instance.CreateUI ("SelectLevelUI");
		int index = int.Parse (id.Substring (0, id.IndexOf ('-')));
		(UIManager.Instance.GetUI ("SelectLevelUI") as SelectLevelUI).init (index);
		UIManager.Instance.DestroyUI ("GameUI");
		UIManager.Instance.DestroyUI ("PopUpGameFinishUI");
		admobdemo.Instance.ShowBanner1 ();
		admobdemo.Instance.ShowFull ();
	}
}
