using UnityEngine;
using System.Collections;

[System.Serializable]
public class BlockSave
{
	public string[] blockName = new string[6];

	public bool blockSaveInit = false;

	public BlockSave()
	{
		blockSaveInit = false;
	}

}
