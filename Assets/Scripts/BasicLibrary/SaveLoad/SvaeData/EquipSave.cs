using UnityEngine;
using System.Collections;

[System.Serializable]
public class EquipSave
{
	private EquipElement[] equipSet = new EquipElement[4];
	public EquipElement GetEquipElement(EquipType type)
	{
		return equipSet[(int)type];
	}

	public void SetEquip(EquipType type, string name)
	{
		equipSet[(int)type].equipType = type;
		equipSet[(int)type].equipName = name;
	}

	public void SetEquip(EquipElement equip)
	{
		equipSet[(int)equip.equipType] = equip;
	}

	public void SetUnEquip(EquipType type)
	{
		EquipElement emptyEquip = new EquipElement();
		equipSet[(int)type] = emptyEquip;

		SaveLoadManager.Instance.Save();
	}

	public bool equipSaveInit = false;

	public EquipSave()
	{
		equipSet = new EquipElement[4];
		equipSet[0] = new EquipElement("W1",EquipType.kWeapone);
		equipSet[1] = new EquipElement("A1",EquipType.kArmor);
		equipSet[2] = new EquipElement("SA1",EquipType.kSubArmor);
		equipSet[3] = new EquipElement("AC1",EquipType.kAccessory);

		equipSaveInit = false;
	}
}
