using System;
using System.Collections.Generic;
using System.Text;

public class MathHelper{
	private static MathHelper instance = null;
	public static MathHelper Instance{
		get{
			if (instance == null) {
				instance = new MathHelper ();
			}
			return instance;
		}
	}
	private MathHelper(){
	}

	private string s =  "123456789456789123789123456234567891567891234891234567345678912678912345912345678";
	private string ss = "001000000002030004000500607500140000070000020000078009807009000400060300000000500";

	public Number[] Decode(string str){
		Number[] ret = new Number[81];
		for(int i=0;i<81;i++){
			ret [i] = new Number (i, ((int)str [i]) - 48);
		}
		return ret;
	}
	//随机生成一个完整解
	public Number[] Generater(){
//		UnityEngine.Debug.Log (s.Length);
//		return null;
		Number[] mp = new Number[81];
		for (int i = 0; i < 81; i++) {
			mp [i] = new Number (i, 0);
		}
		List<int> randArr_81 = new List<int> ();
		GenerateRandomArr (randArr_81, 81);

		bool findSlove = false;
		while(findSlove == false){
			for (int i = 0; i < 11; i++) {
				mp [randArr_81 [i]].d = UnityEngine.Random.Range (1, 10);
			}
//			mp = Decode (ss);
			Solver slover = new Solver (mp);
			findSlove = slover.SolveAny (0);
			mp = slover.GetAnswer ();
		}
//		Slover slover = new Slover (mp);
//		slover.SloveAny (0);
//		return slover.GetAnswer();
		return mp;
	}
	//生成一个数独（随机挖坑）
	public Number[] SudokuGenrater(){
		List<int> randArr_81 = new List<int> ();
		GenerateRandomArr (randArr_81, 81);
		Number[] mp = Generater();
		int temp = 0;
		int solves = 0;
		int keng = 0;
		for (int i = 0; i < 81; i++) {
			if (keng >= UnityEngine.Random.Range (28, 33))
				break;
			temp = mp [randArr_81[i]].d;
			mp [randArr_81[i]].d = 0;
			Solver slover = new Solver (mp);
			solves = slover.SolveOne (0);
			if (solves >= 2) {
				mp [randArr_81 [i]].d = temp;
			} else {
				keng++;
			}
		}
		return mp;
	}

	//生成一个数独（顺序挖坑）
	public Number[] SudokuGenrater2(){
		Number[] mp = Generater();
		int temp = 0;
		for (int i = 0; i < 81; i++) {
			temp = mp [i].d;
			mp [i].d = 0;
			Solver slover = new Solver (mp);
			if (slover.SolveOne (0) >= 2) {
				mp [i].d = temp;
			}
		}
		return mp;
	}

	public void GenerateRandomArr(List<int> a,int len){
		if (a == null)
			return;
		for (int i = 0; i < len && i < a.Count; i++) {
			a [i] = i;
		}
		for (int i = a.Count; i < len; i++) {
			a.Add (i);
		}
		int rand;
		int temp;
		for (int i = 0; i < len; i++) {
			rand = UnityEngine.Random.Range (i, len);
			if (rand != i) {
				temp = a [rand];
				a [rand] = a [i];
				a [i] = temp;
			}
		}
	}

	public void Output(Number[] mp){
		StringBuilder ret = new StringBuilder ("");
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 9; j++) {
				ret.Append (mp [i * 9 + j].d);
				ret.Append (" ");
			}
			ret.Append ("\n");
		}
		UnityEngine.Debug.Log (ret.ToString ());
	}

	private string Get2NumbesFormat(int x){
		if (x < 10) {
			return "0" + x;
		}
		return x.ToString ();
	}

	public String GetTime(int seconds){
		if (seconds <= 60) {
			return Get2NumbesFormat (seconds);
		}
		if (seconds < 3600) {
			return Get2NumbesFormat (seconds / 60) + ":" + Get2NumbesFormat (seconds % 60);
		}
		return Get2NumbesFormat (seconds / 3600) + ":" + Get2NumbesFormat ((seconds % 3600) / 60) + ":" + Get2NumbesFormat (seconds % 60);
	}
}

public class Solver{
	private Number[] mp = new Number[81];
	private bool[,] vCols = new bool[9, 10];
	private bool[,] vRows = new bool[9, 10];
	private bool[,] vSquare = new bool[9, 10];
	private List<int> randArr_81 = new List<int>();//为了保证生成的数独具有独特性,在做解时按照随机顺序进行遍历
	private bool error = false;
	public int solveStep = 0;
	public Solver(Number[] nums){
		for (int i = 0; i < 81; i++) {
			mp [i] = new Number (i, nums [i].d);
		}
//		mp = nums;
		for (int i = 0; i < 9; i++) {
			for (int j = 0; j < 10; j++) {
				vCols [i, j] = false;
				vRows [i, j] = false;
				vSquare [i, j] = false;
			}
		}
		for (int i = 0; i < 81; i++) {
			if (mp [i].d == 0)
				continue;
			if (vRows [mp [i].x, mp [i].d] == true || vCols [mp [i].y, mp [i].d] == true || vSquare [mp [i].z, mp [i].d] == true) {
				error = true;
				return;
			}
			vRows [mp [i].x, mp [i].d] = true;
			vCols [mp [i].y, mp [i].d] = true;
			vSquare [mp [i].z, mp [i].d] = true;
		}
		solveStep = 0;
		MathHelper.Instance.GenerateRandomArr (randArr_81, 81);
	}
	//找出任意解
	public bool SolveAny(int i){
		if (error) {
			return false;
		}
		if (i == 81) {
			return true;
		}
		solveStep++;
		List<int> randArr_9 = new List<int> ();//为了保证生成的数独具有独特性,在做解时按照随机顺序尝试填数
		MathHelper.Instance.GenerateRandomArr (randArr_9, 9);

		int j = 0;
		bool f;
		if (mp [i].d == 0) {
			f = false;
			for (int loop = 0; loop < 9; loop++) {
//			for (j = 1; j <= 9; j++) {
				j = randArr_9 [loop] + 1;
				if (vRows [mp [i].x, j] == false &&
				    vCols [mp [i].y, j] == false &&
				    vSquare [mp [i].z, j] == false) {
					f = true;
					vRows [mp [i].x, j] = vCols [mp [i].y, j] = vSquare [mp [i].z, j] = true;
					mp [i].d = j;
					if (SolveAny (i + 1)) {
						return true;
					}
					mp [i].d = 0;
					vRows [mp [i].x, j] = vCols [mp [i].y, j] = vSquare [mp [i].z, j] = false;
				}
			}
			if (f == false) {
				return false;
			}
		} else {
			return SolveAny (i + 1);
		}

		return false;
	}

	//找出唯一解;0:无解,1:唯一解,2:多解
	public int SolveOne(int i){
		if (error) {
			return 0;
		}
		if (i == 81) {
			return 1;
		}
		//List<int> randArr_9 = new List<int> ();//为了保证生成的数独具有独特性,在做解时按照随机顺序尝试填数
		//MathHelper.Instance.GenerateRandomArr (randArr_9, 9);
		int ret = 0;
		bool f;
		if (mp [i].d == 0) {
			f = false;
			for (int j = 1; j <= 9; j++) {
				if (vRows [mp [i].x, j] == false &&
					vCols [mp [i].y, j] == false &&
					vSquare [mp [i].z, j] == false) {
					f = true;
					vRows [mp [i].x, j] = vCols [mp [i].y, j] = vSquare [mp [i].z, j] = true;
					mp [i].d = j;
					ret += SolveOne (i + 1); 
					if (ret > 1) {
						return ret;
					}
					mp [i].d = 0;
					vRows [mp [i].x, j] = vCols [mp [i].y, j] = vSquare [mp [i].z, j] = false;
				}
			}
			if (f == false) {
				return 0;
			}
		} else {
			return SolveOne (i + 1);
		}

		return ret;
	}

	public Number[] GetAnswer(){
		return mp;
	}
}

