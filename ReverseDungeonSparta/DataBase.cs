using System.Text;
using ReverseDungeonSparta.Entiity;

namespace ReverseDungeonSparta
{
    public static class DataBase //장착아이템, 사용아이템, 스킬, 몬스터, 플레이어 직업, 스토리라인 등 객체정보 보관 클래스
    {
        public static StringBuilder introText3 = new StringBuilder();
        public static string playerName = string.Empty;

        //이름 받기 전 까지의 출력 텍스트
        public static List<string> introText = new List<string>
            {
                "%스파르타 던전───%\n\n",

                "그곳이 언제부터, 어떤 이유로 존재하는지 아는 사람은 아무도 없지만,\n",
                "오늘도 많은 모험가들이 자신의 힘을 시험하기 위해 스파르타 던전에 도전한다.\n",
                "그러나 이런 모험가들의 방문이 달갑지 않았던 존재가 하나 있었으니...\n\n",

                "%[모험가]% 마왕! 오늘이야말로 네 목을 가져가겠다!\n\n",

                "%[마왕]% ... ... 아! 제발!\n",
                "%[마왕]% 너희 인간들에겐 에티켓이라는 개념이 존재하지 않는 거야?\n\n",

                "%[모험가]% 무슨 마왕이 에티켓을 따져?\n\n",

                "%[마왕]% 모르는 사람들이 내 집에서 난장판을 피우는데 안 따지게 생겼어?!\n\n",

                "%[모험가]% ... ... 알 게 뭐야! 죽어라!!!\n\n",

                "*칼싸움 소리*\n\n",

                "%[마왕]% ... 정말 지긋지긋해 죽겠어!\n",
                "%[마왕]% 집값이 싼 건 다 이유가 있었다고.\n",
                "%[마왕]% 애초에 왜 내가 마왕이라고 불리게 된 거야?\n",
                "%[마왕]% 나도 제대로 된 이름이 있다고!\n\n",

                "내 이름은...",

            };//introText End

        //이름 받은 후의 출력 텍스트
        public static List<string> introText2 = new List<string>
            {
                $"\n%[~이름~]% 내 이름은... 마왕이 아니라 ~이름~이가란 말이야!\n&",

                "*칼싸움 소리*&",
                "*상대 쓰러지는 소리*\n&",

                $"%[~이름~]% ... 이대로는 안 되겠어.\n&",
                $"%[~이름~]% 대체 무슨 헛소리가 돌고 있는지 내가 직접 내려가서 확인해야겠어!\n",
            };

        public static List<string>[] endingText = new List<string>[3];
        public static void InitEndingText()
        {
            endingText[0] = new List<string>
            {
                "%~이름~%은는 스파르타 던전을 나왔다.&",
                "1억 6천 년 만에 쬐는 햇빛은 좀 과하게 눈이 부셨지만, 따뜻했다.\n&",

                "새들은 지저귀고, 꽃들은 피어나고... ... ... 아니, 왠지 이 이상 생각하면 안 될 것 같은데.\n&",

                "아무튼 바깥은 %~이름~%의 집과 달리 평화로웠다.\n&",

                "%[~이름~]% 인간들은 이렇게 좋은 곳에 살면서 왜 어두컴컴하고 칙칙한 남의 보금자리까지 탐내는 거야?\n&",

                "그게 바로 인간 종족의 특성이라 할 수 있지만&",
                "1억 7천년 만에 외출하는 %~이름~%은는 알 길이 없다.\n&",

                "%[~이름~]% 그런데...&",
                "%[~이름~]% 일단 내려오기는 했는데, 이제 어떡하지?\n\n\n\n\n\n\n\n\n\n\n\n&",

                "    무단침입에 대한 피해보상을 청구한다.&",
                "    나온 김에 던전 주변을 깔끔하게 청소한다.\n&",
            };
            endingText[1] = new List<string>
            {
                "%[1]% 무단침입에 대한 피해보상을 청구한다.\n&",

                "%~이름~%은는 무단 침입에 대한 피해보상을 받아내기 위해&",
                "사람들에게 물어물어 인간들의 총책임자를 찾아가게 된다.\n&",

                "졸지에 마왕을 맞이하게 된 스파르타의 왕은&",
                "대경실색하며 %~이름~%의 요구를 모두 들어주었다.\n&",

                "%~이름~%은는 왕의 태도에 매우 만족하며 자신의 보금자리로 돌아간다.\n&",

                "인간들의 왕은 참으로 친절하니&",
                "다음에 또 놀러 와야겠다고 생각하면서...\n\n\n\n\n\n\n\n\n\n\n\n&",

                "%[END 1]% 인간들의 왕국에 당도한 것을 환영하오 마왕이여\n&",

                "스파르타 왕국 존속&",
                "%~이름~%은는 주기적으로 스파르타 왕국을 방문하며 교류를 이어갑니다.&",
            };
            endingText[2] = new List<string>
            {
                "%[2]% 나온 김에 던전 주변을 깔끔하게 청소한다.\n&",

                "%~이름~%은는 밖으로 나온 김에 1억 8천 년 만의 대청소를 결심한다.&",
                "... 아니, 1억 7천 년 만이던가? 아니면 1억 6천 년?\n&",

                "%~이름~%은는 자신의 나이를 어림잡아보다 그만둔다.&",
                "원래 나이란 건 스무 살만 넘어가도 금방 가물가물해지는 법이다.\n&",

                "...&",
                "...&",
                "...\n&",

                "아무튼 %~이름~%은는 장장 반올림 2억 년 만의 청소를 시작했다.&",
                "청소 거리가 쌓여 쉽지 않았지만, 쉬지 않고 매달리니 금세 끝낼 수 있었다.\n&",
                "%~이름~%은는 뿌듯한 마음을 안고 자신의 안락한 보금자리로 돌아간다.\n&",
                "이만큼 깨끗이 청소했으니, 한동안은 평화롭겠지!\n\n\n\n\n\n&",

                "%[END 2]% 깨끗한 시간을 보내고 싶어?\n&",

                "스파르타 왕국 멸망&",
                "앞으로도 %~이름~%은는 주기적으로 청소를 나옵니다.",
            };
        }

      

        // 생성된 배열에 만들어진 아이템 목록
        public static EquipItemInfo[] EQUIPITEMINFO =
        {
            new EquipItemInfo("찢어진 도적의 망토",0,5,0,0,25,0,EquipItemType.Armor,EquipItemGrade.Normal,"    도적이 버리고 간 찢어진 망토"),
            new EquipItemInfo("전사의 강철 갑옷",0,10,0,0,35,0,EquipItemType.Armor,EquipItemGrade.Uncommon,"    가문 대대로 내려온 강철 갑옷"),
            new EquipItemInfo("마법사의 고대 로브",5,9,0,0,0,0,EquipItemType.Armor,EquipItemGrade.Rare,"    해리포터가 사용한 고대 로브"),
            new EquipItemInfo("치유의 지팡이",0,0,2,7,0,0,EquipItemType.Weapon, EquipItemGrade.Normal,"    마법이 부족한 지팡이"),
            new EquipItemInfo("빛 바랜 단검",5,0,10,0,0,0,EquipItemType.Weapon, EquipItemGrade.Uncommon,"    옛날부터 수련에 사용된 단검"),
            new EquipItemInfo("태양의 활",10,0,15,0,0,0,EquipItemType.Weapon, EquipItemGrade.Rare,"    태양의 힘을 서린 활"),
            new EquipItemInfo("허름한 궁수의 모자",2,4,0,0,0,0,EquipItemType.Helmet, EquipItemGrade.Normal,"    초보자가 사용한 모자"),
            new EquipItemInfo("마법사의 마나의 왕관",0,10,0,0,35,0,EquipItemType.Helmet, EquipItemGrade.Uncommon,"    숙련된 마법사의 왕관"),
            new EquipItemInfo("그림자의 두건",0,15,0,0,50,0,EquipItemType.Helmet, EquipItemGrade.Rare,"    도적 군집이 사용했던 그림자 두건"),
            new EquipItemInfo("마법사의 천 신발",0,7,0,0,25,0,EquipItemType.Shoes, EquipItemGrade.Normal,"    견습생이 신는 신발"),
            new EquipItemInfo("도적의 철의 발걸음",10,10,0,0,0,0,EquipItemType.Shoes, EquipItemGrade.Uncommon,"    대장장이가 만든 도적의 철 신발"),
            new EquipItemInfo("용맹의 전투신발",0,15,0,0,50,0,EquipItemType.Shoes, EquipItemGrade.Rare,"    용맹함이 가득한 전사의 신발"),
            new EquipItemInfo("금이 간 전사의 반지",0,0,0,10,0,25,EquipItemType.Ring, EquipItemGrade.Normal,"    전투하다 금이 간 전사의 반지"),
            new EquipItemInfo("힐러의 생명의 반지",0,0,0,15,0,35,EquipItemType.Ring, EquipItemGrade.Uncommon,"    지능을 상당히 증가시키는 반지"),
            new EquipItemInfo("날렵한 명사수의 반지",5,0,15,0,0,0,EquipItemType.Ring, EquipItemGrade.Rare,"    일등사수가 사용했던 명사수의 반지"),
            new EquipItemInfo("생명의 구슬 목걸이",0,0,0,10,0,25,EquipItemType.Necklace, EquipItemGrade.Normal,"    여러 사람 구한 힐러의 목걸이"),
            new EquipItemInfo("명사의 목걸이",0,0,15,0,0,35,EquipItemType.Necklace, EquipItemGrade.Uncommon,"    백발백중 명사가 사용했던 목걸이"),
            new EquipItemInfo("힐러의 목걸이",0,0,0,10,0,50,EquipItemType.Necklace, EquipItemGrade.Rare,"    신성력이 가득한 힐러의 목걸이"),
        };

        // 소비 아이템 정보
        public static UsableItemInfo[] USABLEITEMINFO =
        {
        new UsableItemInfo("하급 체력 회복 포션", "플레이어의 HP를 30 회복합니다.", 30, 0),
        new UsableItemInfo("중급 체력 회복 포션", "플레이어의 HP를 50 회복합니다.", 50, 0),
        new UsableItemInfo("상급 체력 회복 포션", "플레이어의 HP를 70 회복합니다.", 70, 0),
        new UsableItemInfo("하급 마나 회복 포션", "플레이어의 MP를 30 회복합니다.", 0, 30),
        new UsableItemInfo("중급 마나 회복 포션", "플레이어의 MP를 50 회복합니다.", 0, 50),
        new UsableItemInfo("상급 마나 회복 포션", "플레이어의 MP를 70 회복합니다.", 0, 70)
        };

        //물리 스킬을 저장해두는 스킬 정보 배열
        public static SkillInfo[] PHYSICALSKILLINFO = new SkillInfo[]
        {
        new SkillInfo("강타", 1.5d, ExtentEnum.First, ApplyType.Enemy, SkillType.Physical, 5, BuffType.None, 0, "상대방을 강하게 때려 1.5배의 피해를 입힙니다."),
        new SkillInfo("휘두르기", 1.0d, ExtentEnum.FirstAndThird, ApplyType.Enemy, SkillType.Physical, 8, BuffType.None, 0, "무기를 휘둘러 최대 2명에게 1배의 피해를 입힙니다."),
        new SkillInfo("회전 베기", 0.7d, ExtentEnum.Fourth, ApplyType.Enemy, SkillType.Physical, 10, BuffType.None, 0, "한 바퀴 회전하며 모든 적들에게 0.7배의 피해를 입힙니다.")
        };

        //마법 스킬을 저장해두는 스킬 정보 배열
        public static SkillInfo[] MAGICALSKILLINFO = new SkillInfo[]
        {
        new SkillInfo("파이어 볼", 1.5d, ExtentEnum.First, ApplyType.Enemy, SkillType.Magic, 8, BuffType.None, 0, "작은 불덩이를 쏘아 적에게 1.5배의 피해를 입힙니다."),
        new SkillInfo("메테오", 2.0d, ExtentEnum.First, ApplyType.Enemy, SkillType.Magic, 15, BuffType.None, 0, "큰 불덩이를 쏘아 적에게 2배의 피해를 입힙니다."),
        new SkillInfo("파이어 월", 0.7d, ExtentEnum.Third, ApplyType.Enemy, SkillType.Magic, 7, BuffType.None, 0, "불의 벽을 만들어 상대방에서 0.7배의 피해를 입힙니다."),
        new SkillInfo("화염 분출", 1.2d, ExtentEnum.Second, ApplyType.Enemy, SkillType.Magic, 10, BuffType.None, 0, "바닥에서 화염을 분출시켜 적 2명에게 1.2배의 피해를 입힙니다."),
        new SkillInfo("지옥불 폭발", 1.4d, ExtentEnum.FirstAndFourth, ApplyType.Enemy, SkillType.Magic, 12, BuffType.None, 0, "큰 폭발을 만들어 적 2명에게 1.4배의 피해를 입힙니다.")
        };

        //버프 스킬을 저장해두는 스킬 정보 배열
        public static SkillInfo[] BUFFSKILLINFO = new SkillInfo[]
        {
        new SkillInfo("공격력 강화", 1.3d, ExtentEnum.First, ApplyType.Team, SkillType.Buff, 8,BuffType.AttackBuff, 2, "정신을 집중해서 2턴 동안 공격력을 1.3배 올립니다."),
        new SkillInfo("방어력 강화", 1.3d, ExtentEnum.First, ApplyType.Team, SkillType.Buff, 8, BuffType.DefenceBuff, 2, "정신을 집중해서 2턴 동안 방어력을 1.3배 올립니다."),
        new SkillInfo("행운 강화", 1.3d, ExtentEnum.First, ApplyType.Team, SkillType.Buff, 8, BuffType.LuckBuff, 2, "정신을 집중해서 2턴 동안 행운을 1.3배 올립니다.")
        };

        //힐 스킬을 저장해두는 스킬 정보 배열
        public static SkillInfo[] HEALINGSKILLINFO = new SkillInfo[]
        {
        new SkillInfo("힐링", 35, ExtentEnum.First, ApplyType.Team, SkillType.Buff, 5, BuffType.HealingBuff, 1, "정신을 집중해서 체력을 35 회복합니다."),
        new SkillInfo("퓨어 힐", 50, ExtentEnum.First, ApplyType.Team, SkillType.Buff, 8, BuffType.HealingBuff, 1, "정신을 집중해서 체력을 50 회복합니다."),
        new SkillInfo("지속 힐", 15, ExtentEnum.First, ApplyType.Team, SkillType.Buff, 5, BuffType.HealingBuff, 3, "정신을 집중해서 3턴 동안 체력을 15씩 회복합니다.")
        };
    }
}
