using ReverseDungeonSparta.Manager;

namespace ReverseDungeonSparta.Entiity
{
    public enum EquipItemGrade
    {
        Normal,
        Uncommon,
        Rare,
    }
    public enum EquipItemType
    {
        Armor,
        Weapon,
        Helmet,
        Shoes,
        Ring,
        Necklace
    }
    
    public class EquipItem : Item
    {
        public EquipItemType Type { get; set; }
        public EquipItemGrade Grade { get; set; }
        public int AddLuck { get; set; }
        public int AddDefence { get; set; }
        public int AddAttack { get; set; }
        public int AddIntelligence { get; set; }
        public int AddMaxHp { get; set; }
        public int AddMaxMp { get; set; }

        public bool IsEquiped { get; set; }
        public bool IsSelected { get; set; }

        public EquipItem(EquipItemInfo e)
        {
            Name = e.name;
            Information = e.info;
            Type = e.type;
            Grade = e.grade;
            AddLuck = e.addLuck;
            AddDefence = e.addDefence;
            AddAttack = e.addAttack;
            AddIntelligence = e.addIntelligence;
            AddMaxHp = e.addMaxHp;
            AddMaxMp = e.addMaxMp;
            IsEquiped = false;
            IsSelected = false;
        }

        public EquipItem()
        {

        }
    }
    public struct EquipItemInfo
    {
        public string name;
        public string info;
        public EquipItemType type;
        public EquipItemGrade grade;
        public int addLuck;
        public int addDefence;
        public int addAttack;
        public int addIntelligence;
        public int addMaxHp;
        public int addMaxMp;
        public EquipItemInfo(string _name, int _addLuck, int _addDefence, int _addAttack, int _addIntelligence, int _addMaxHp, int _addMaxMp, EquipItemType _type, EquipItemGrade _grade, string _info)
        {
            name = _name;
            info = _info;
            type = _type;
            grade = _grade;
            addLuck = _addLuck;
            addDefence = _addDefence;
            addAttack = _addAttack;
            addIntelligence = _addIntelligence;
            addMaxHp = _addMaxHp;
            addMaxMp = _addMaxMp;
        }
    }
}