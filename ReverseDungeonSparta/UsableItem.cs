using Microsoft.VisualBasic;
using ReverseDungeonSparta;
using System.Xml.Linq;

public class UsableItem : Item
{
    public int Hp { get; set; }
    public int Mp { get; set; }
    public int Count { get; set; }

    public UsableItem(UsableItemInfo u)
    {
        Name = u.name;
        Information = u.info;
        Hp = u.hp;
        Mp = u.mp;
        Count = u.count;
    }

    public UsableItem() 
    {
        UsableItem item = new UsableItem();
    }

    // 소비 아이템 구조체
    public struct UsableItemInfo
    {
        public string name;
        public string info;
        public int hp;
        public int mp;
        public int count;

        public UsableItemInfo(string _name, string _info, int _hp, int _mp, int _count)
        {
            name = _name;
            info = _info;
            hp = _hp;
            mp = _mp;
            count = _count;
        }
    }

    public static List<UsableItem> GetUsableItemList(int num) // 생성된 리스트 아이템 넣기
    {
        List<UsableItem> usableItemList = new List<UsableItem>();
        for (int i = 0; i < num; i++)
        {
            usableItemList.Add(InstanceUsableItem(i));
        }
        return usableItemList;
    }

    public static UsableItem InstanceUsableItem(int index) // indx를 통한 아이템 정보를 반환
    {
        if (index >= 0 && index < allUsableItem.Length)
        {
            UsableItemInfo usableItemInfo = allUsableItem[index];
            return new UsableItem(usableItemInfo);
        }
        return new UsableItem();
    }

    // 소비 아이템 목록
    public static void UseItemView()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("소지품 확인 - 소비");
            Console.WriteLine("");

            Player player = GameManager.Instance.Player; // Player 객체 가져오기

            // 소비 아이템 목록 출력
            for (int i = 0; i < player.UsableItemInventory.Count; i++)
            {
                UsableItem item = player.UsableItemInventory[i];
                string recoveryInfo = "";

                if (item.Hp > 0)
                {
                    recoveryInfo += $"HP +{item.Hp}";
                }

                if (item.Mp > 0)
                {
                    if (recoveryInfo != "") // HP 정보가 있으면 공백 추가
                    {
                        recoveryInfo += " ";
                    }
                    recoveryInfo += $"MP +{item.Mp}";
                }

                Console.WriteLine($"{i + 1}. {item.Name} | {recoveryInfo} | {item.Information} | 보유 수 : {item.Count}개");
            }

            Console.WriteLine("\n사용할 소비 아이템을 선택해주세요.");

            List<(string, Action, Action)> inventoryItems = new List<(string, Action, Action)>();

            for (int i = 0; i < player.UsableItemInventory.Count; i++)
            {
                int index = i;
                inventoryItems.Add(($"{player.UsableItemInventory[i].Name}",
                    () =>
                    {
                        UseSelectedItem(index); // 아이템 사용
                        ShowRecoveryMessage();  // 회복 메시지 출력
                    },
                    () => AudioManager.PlayMoveMenuSE(0))); // 메뉴 이동 효과음
            }

            inventoryItems.Add(("나가기",
                () => GameManager.Instance.InventoryMenu(),
                () => AudioManager.PlayMoveMenuSE(0))); // 나가기 옵션

            Util.GetUserInput(inventoryItems, UseItemView, ref GameManager.Instance.selectedIndex); // 사용자 입력 받기
        }
    }

    private static void ShowRecoveryMessage()
    {
        Console.WriteLine("\n엔터 키를 눌러 계속 진행하세요...");
        Console.ReadLine();
        Console.Clear();
    }

    // 소비 아이템 사용
    public static void UseSelectedItem(int itemIndex)
    {
        Player player = GameManager.Instance.Player;
        UsableItem selectedItem = player.UsableItemInventory[itemIndex];

        if (selectedItem.Count > 0)
        {
            bool itemUsed = ApplyItemEffect(selectedItem);  // 아이템 효과 적용

            if (itemUsed)
            {
                selectedItem.Count--; // 사용하면 보유 수 차감
                Console.WriteLine($"{selectedItem.Name}을(를) 사용했습니다! 남은 개수: {selectedItem.Count}개");

                // 만약 사용한 아이템의 개수가 0이 되면 리스트에서 제거
                if (selectedItem.Count == 0)
                {
                    player.UsableItemInventory.RemoveAt(itemIndex);
                }
            }
            else
            {
                // HP 또는 MP가 이미 최대치라면 사용되지 않음
                Console.WriteLine("이 아이템을 사용할 수 없습니다.");
            }
        }
        else
        {
            // 아이템이 없다면
            Console.WriteLine("이 아이템을 사용할 수 없습니다.");
        }
    }

    // 아이템 효과 적용
    public static bool ApplyItemEffect(UsableItem item)
    {
        Player player = GameManager.Instance.Player;
        bool itemUsed = false; // 아이템 사용 여부 확인
        string recoveryMessage = ""; // 회복 메시지

        // HP 회복 아이템일 때
        if (item.Hp > 0)
        {
            if (player.HP < player.MaxHP) // 플레이어의 HP가 최대가 아닐 때
            {
                int newHP = Math.Min(player.HP + item.Hp, player.MaxHP);
                recoveryMessage += $"HP +{item.Hp} (현재 HP: {newHP}/{player.MaxHP}) ";
                player.HP = newHP;
                itemUsed = true; // 아이템 사용 표시
            }
            else
            {
                recoveryMessage += "HP가 이미 최대입니다. "; // 이미 HP가 최대일 때
            }
        }

        // MP 회복 아이템일 때
        if (item.Mp > 0)
        {
            if (player.MP < player.MaxMP) // 플레이어의 MP가 최대가 아닐 때
            {
                int newMP = Math.Min(player.MP + item.Mp, player.MaxMP);
                recoveryMessage += $"MP +{item.Mp} (현재 MP: {newMP}/{player.MaxMP}) ";
                player.MP = newMP;
                itemUsed = true; // 아이템 사용 표시
            }
            else
            {
                recoveryMessage += "MP가 이미 최대입니다. "; // 이미 최대 MP일 때
            }
        }

        // 회복 메시지가 비어있지 않으면 출력
        if (!string.IsNullOrEmpty(recoveryMessage))
        {
            Console.WriteLine(recoveryMessage);
        }

        return itemUsed; // 아이템 사용 여부 반환
    }

    // 소비 아이템 정보
    public static UsableItemInfo[] allUsableItem =
    {
        new UsableItemInfo("하급 체력 회복 포션", "플레이어의 HP를 30 회복합니다.", 30, 0, 0),
        new UsableItemInfo("중급 체력 회복 포션", "플레이어의 HP를 50 회복합니다.", 50, 0, 0),
        new UsableItemInfo("상급 체력 회복 포션", "플레이어의 HP를 70 회복합니다.", 70, 0, 0),
        new UsableItemInfo("하급 마나 회복 포션", "플레이어의 MP를 30 회복합니다.", 0, 30, 0),
        new UsableItemInfo("중급 마나 회복 포션", "플레이어의 MP를 50 회복합니다.", 0, 50, 0),
        new UsableItemInfo("상급 마나 회복 포션", "플레이어의 MP를 70 회복합니다.", 0, 70, 0),
    };
}
