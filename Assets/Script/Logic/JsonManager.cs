using System;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections;
/// <summary>
/// add new configure follow the steps:
/// 1.add one or more struct files in the Assets/BallPool/Scripts/Json/,name end with *.cs;
/// 2.add a configure file in the Resources/Json/,name end with *.txt;
/// 3.add the configure file name in the JsonManager.filesName;
/// 4.add a non serialized var in the JsonManager;
/// 5.add the deserialize code in the method JsonManager.Deserialize;hint:the list need use the method getJsonArray<T>(json);
/// </summary>
/// 

public enum LanguagesType{
	English=0,
	Deutsch
}
public class JsonManager : MonoBehaviour{
	private String 			JsonPath;
	private String[]		filesName = {
							"Easy",
							"Normal",
							"Hard"
	};
	private float 				initProgress;
	public List<LevelConfigure>	EasyConfigure;
	public List<LevelConfigure>	NormalConfigure;
	public List<LevelConfigure>	HardConfigure;

	public static JsonManager Instance = null;
	void Awake(){
		if (Instance == null) {
			Instance = this;
			JsonPath 		= "Json/";
			initProgress 	= 0.0f;
			Init ();
			DontDestroyOnLoad (gameObject);
		} else {
			DestroyObject (gameObject);
		}
	}

	private void Init(){
		StartCoroutine ("initCoroutine");
	}

	private IEnumerator initCoroutine(){
		String data;
		for (int i = 0; i < filesName.Length; i++){
			yield return data = (Resources.Load (JsonPath + filesName [i]) as TextAsset).text;
			Deserialize (filesName[i], data);

			initProgress = (float)(i + 1) / filesName.Length;
		}
		Resources.UnloadUnusedAssets ();
	}

	private void Deserialize(string fileName, string data){
		if (fileName == "Easy") {
			EasyConfigure = getJsonArray<LevelConfigure> (data);
		} else if (fileName == "Normal") {
			NormalConfigure = getJsonArray<LevelConfigure> (data);
		} else if (fileName == "Hard") {
			HardConfigure = getJsonArray<LevelConfigure> (data);
		}
	}

	private List<T> getJsonArray<T>(string json)
	{
		string newJson = "{ \"array\": " + json + "}";
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>> (newJson);
		return wrapper.array;
	}

	[Serializable]
	private class Wrapper<T>
	{
		public List<T> array = null;
	}
}

