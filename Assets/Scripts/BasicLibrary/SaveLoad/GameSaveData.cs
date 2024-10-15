using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameSaveData
 { //don't need ": Monobehaviour" because we are not attaching it to a game object

	//public static GameSaveData current;
	public HeroSave heroSave;
	public BlockSave blockSave;
	public UserSave userSave;
	public EquipSave equipSave;

	public GameSaveData () 
	{
		heroSave = new HeroSave();
		blockSave = new BlockSave();
		userSave = new UserSave();
		equipSave = new EquipSave();
	}
		
}
