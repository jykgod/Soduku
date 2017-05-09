using System;

public class Number{
	public int x;//行号
	public int y;//列号
	public int z;//所属的九宫格位置
	public int d;//填的数
	public int index;//一维数组中的编号
	public Number (int index,int d){
		this.x = index / 9;
		this.y = index % 9;
		this.d = d;
		this.z = (x / 3) * 3 + (y / 3);
		this.index = index;
	}
}

