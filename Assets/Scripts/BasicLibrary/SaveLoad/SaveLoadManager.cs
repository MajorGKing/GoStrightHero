using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadManager  : TMonoSingleton<SaveLoadManager>
{
	//it's static so we can call it from anywhere
	public static GameSaveData savedGames;

	public GameSaveData GameData
	{
		get
		{
			return savedGames;
		}
	}
	

	public override void Initialize()
    {
		savedGames = new GameSaveData();
		gameObject.name = "SaveLoadManager";
		InitSave();
		Load();
	}
	public override void Destroy ()
	{

	}
	public void Save() 
	{	
		m_Save();
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
		bf.Serialize(file, SaveLoadManager.savedGames);
		file.Close();

		Debug.Log("Saved");
	}	
	
	public void Load() 
	{
		if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) 
		{
			Debug.Log("Load start");
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			SaveLoadManager.savedGames = (GameSaveData)bf.Deserialize(file);
			file.Close();
		}
	}

	private void m_Save()
	{
		// Save Hero Data
		HeroManager heroManager = (HeroManager)UserManager.Instance.Hero.GetComponent(typeof(HeroManager));

		savedGames.heroSave = heroManager.HeroSaveData;
	}

	private void InitSave()
	{
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
		bf.Serialize(file, SaveLoadManager.savedGames);
		file.Close();
	}

	public void ResetSave()
	{
		Debug.Log("Reset int manager");
		savedGames.heroSave = new HeroSave();
		savedGames.blockSave = new BlockSave();
		savedGames.userSave = new UserSave();
		savedGames.equipSave = new EquipSave();

		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd"); //you can call it anything you want
		bf.Serialize(file, SaveLoadManager.savedGames);
		file.Close();

		Load();

		GameManager.Instance.ChangeState (GameState.kOutGame);
	}
}
