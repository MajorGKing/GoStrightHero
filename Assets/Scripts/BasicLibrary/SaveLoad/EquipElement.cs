[System.Serializable]

public class EquipElement
{
	public string equipName = "";
	public EquipType equipType = EquipType.kWeapone;

	public EquipElement()
	{
		equipName = "";
		equipType = EquipType.kNull;
	}

	public EquipElement(EquipElement item)
	{
		equipName = item.equipName;
		equipType = item.equipType;
	}

	public EquipElement(string name, EquipType type)
	{
		equipName = name;
		equipType = type;
	}
}
