using System;
using System.Collections.Generic;
[Serializable]
public struct NumberStruct{
	public int x;
	public int y;
	public int v;
}
[Serializable]
public class LevelConfigure{
	public List<NumberStruct> Numbers;
}

