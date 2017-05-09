using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
public class Launcher : MonoBehaviour {
	List<Number[]> list1 = new List<Number[]>();
	List<int> list2 = new List<int>();
	// Use this for initialization
	void Start () {
		UIManager.Instance.CreateUI ("MainMenuUI");
		admobdemo.Instance.ShowBanner1 ();
	//	StartCoroutine (generator (10));
	}
	private IEnumerator generator(int x){
		StringBuilder sb = new StringBuilder ("[\n");
		for (int i = 0; i < x; i++) {
			yield return doit ();
		}

		for (int i = 0; i < x; i++) {
			for (int j = i; j < x; j++) {
				if (list2 [j] < list2 [i]) {
					int temp = list2 [i];
					list2 [i] = list2 [j];
					list2 [j] = temp;
					Number[] temps = list1 [i];
					list1 [i] = list1 [j];
					list1 [j] = temps;
				}
			}
		}

		for (int i = 0; i < x; i++) {
			sb.Append ("{\n\t\"Numbers\" : [");
			for (int j = 0; j < 81; j++) {
				if (list1 [i] [j].d != 0) {
					sb.Append ("\n\t\t{");
					sb.Append ("\n\t\t\t\"x\" : " + list1 [i] [j].x);
					sb.Append (",\n\t\t\t\"y\" : " + list1 [i] [j].y);
					sb.Append (",\n\t\t\t\"v\" : " + list1 [i] [j].d);
					sb.Append ("\n\t\t}");
					list1 [i] [j].d = 0;
					break;
				}
			}
			for (int j = 0; j < 81; j++) {
				if (list1 [i] [j].d != 0) {
					sb.Append (",\n\t\t{");
					sb.Append ("\n\t\t\t\"x\" : " + list1 [i] [j].x);
					sb.Append (",\n\t\t\t\"y\" : " + list1 [i] [j].y);
					sb.Append (",\n\t\t\t\"v\" : " + list1 [i] [j].d);
					sb.Append ("\n\t\t}");
				}
			}
			sb.Append ("]\n},\n");
		}
			sb.Append ("]");
		FileStream fs = File.OpenWrite ("E:\\1.txt");
		byte[] arr = System.Text.Encoding.Default.GetBytes (sb.ToString ());
		fs.Write (arr, 0, arr.Length);
	}
	private IEnumerator doit(){
		Number[] mp = MathHelper.Instance.SudokuGenrater ();
		MathHelper.Instance.Output (mp);
		int count = 0;
		int min = 1000000000;
		int max = 0;
		for (int i = 0; i < 10; i++) {
			Solver solver = new Solver (mp);
			solver.SolveAny (0);
			count += solver.solveStep;
			if (solver.solveStep > max) {
				max = solver.solveStep;
			}
			if (solver.solveStep < min) {
				min = solver.solveStep;
			}
			//Debug.Log ("step:" + solver.solveStep);
		}
		list2.Add (count - max - min);
		list1.Add (mp);
		//MathHelper.Instance.Output(mp);
		Debug.Log ("max:" + max + ";min:" + min);
		yield return new WaitForEndOfFrame();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
