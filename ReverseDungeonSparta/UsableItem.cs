using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Dmo;
using static Monster;
using static ReverseDungeonSparta.EquipItem;

namespace ReverseDungeonSparta
{
    public class UsableItem : Item
    {
        public UsableItemInfo ItemInfo { get; private set; }
        string Name { get; set; }
        string Description { get; set; }
        int Hp { get; set; }
        int Mp { get; set; }

        public int Count { get; set; }

        public UsableItem(UsableItemInfo usableItemInfo)
        {
            this.ItemInfo = usableItemInfo;
        }
        public UsableItem()
        {

        }

        // 소비 아이템 구조체
        public struct UsableItemInfo
        {
            public string name;
            public string description;
            public int hp;
            public int mp;
            public int count;

            public UsableItemInfo(string _name, string _description, int _hp, int _mp, int _count)
            {
                name = _name;
                description = _description;
                hp = _hp;
                mp = _mp;
                count = _count;
            }
        }

        public static UsableItemInfo[] allUsableItem =
        {
            new UsableItemInfo("체력 회복 포션", "플레이어의 HP를 50 회복합니다.", 50, 0, 10), // 체력 포션
            new UsableItemInfo("마나 회복 포션", "플레이어의 MP를 50 회복합니다.", 0, 50, 6), // 마나 포션
            new UsableItemInfo("만능 회복 포션", "플레이어의 HP와 MP를 50 회복합니다.", 50, 50,1), // 만능 포션
        };

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
            if (index >= 0 && index <= allUsableItem.Length)
            {
                UsableItemInfo usableItemInfo = allUsableItem[index];

                return new UsableItem(usableItemInfo);
            }
            else
            {
                return new UsableItem();
            }
        }

        // 소비 아이템 목록
        public static void UseItemView()
        {
            Console.Clear();
            Console.WriteLine("소지품 확인 - 소비");
            Console.WriteLine("");

            for (int i = 0; i < allUsableItem.Length; i++)
            {
                UsableItemInfo item = allUsableItem[i];
                string recoveryInfo = "";
                if (item.hp > 0 && item.mp > 0)
                {
                    recoveryInfo = $"HP +{item.hp}, MP +{item.mp}";
                }
                else if (item.hp > 0)
                {
                    recoveryInfo = $"HP +{item.hp}";
                }
                else if (item.mp > 0)
                {
                    recoveryInfo = $"MP +{item.mp}";
                }

                Console.WriteLine($"{item.name} | {recoveryInfo} | {item.description} | 보유 수 : {item.count}개");
            }
            Console.WriteLine("");
            List<(String, Action, Action)> inventoryItems = new List<(string, Action, Action)>
            {
                ("아이템 사용", UseSelectedItem, () => AudioManager.PlayMoveMenuSE(0)),
                ("나가기", GameManager.Instance.InventoryMenu, () => AudioManager.PlayMoveMenuSE(0))
            };
            Util.GetUserInput(inventoryItems, UseItemView, ref GameManager.Instance.selectedIndex);
        }

        // 소비 아이템 사용
        public static void UseSelectedItem()
        {
            Console.Clear();
            Console.WriteLine("소지품 확인 - 소비 아이템 사용");
            Console.WriteLine("");
            for (int i = 0; i < allUsableItem.Length; i++)
            {
                UsableItemInfo item = allUsableItem[i];
                string recoveryInfo = "";
                if (item.hp > 0 && item.mp > 0)
                {
                    recoveryInfo = $"HP +{item.hp}, MP +{item.mp}";
                }
                else if (item.hp > 0)
                {
                    recoveryInfo = $"HP +{item.hp}";
                }
                else if (item.mp > 0)
                {
                    recoveryInfo = $"MP +{item.mp}";
                }

                Console.WriteLine($"{i + 1} {item.name} | {recoveryInfo} | {item.description} | 보유 수 : {item.count}개");
            }
            Console.WriteLine("");
            Console.WriteLine("사용할 소비 아이템을 선택해주세요.");
            string input = Console.ReadLine();
            int selectedIndex = -1;

            if (int.TryParse(input, out selectedIndex) && selectedIndex >= 1 && selectedIndex <= allUsableItem.Length)
            {
                selectedIndex--;
                ref UsableItemInfo selectedItem = ref allUsableItem[selectedIndex];

                if (selectedItem.count > 0)
                {
                    // 아이템 효과 적용
                    bool itemUsed = ApplyItemEffect(selectedItem);

                    if (itemUsed) // 아이템이 효과를 적용했다면, 보유 수 차감
                    {
                        selectedItem.count--;
                        Console.WriteLine($"{selectedItem.name}을(를) 사용했습니다! 남은 개수: {selectedItem.count}개");
                    }
                }
                else // 보유 수가 0개일 경우
                {
                    Console.WriteLine("이 아이템을 더 이상 사용할 수 없습니다.");
                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 선택해주세요.");
            }

            Console.WriteLine("\n엔터 키를 눌러 계속 진행하세요...");
            Console.ReadLine();
            UseItemView(); // 다시 아이템 목록을 출력
        }


        //아이템 효과 적용
        public static bool ApplyItemEffect(UsableItemInfo item)
        {
            Player player = GameManager.Instance.Player; // Player 객체 가져오기
            bool itemUsed = false;

            // HP와 MP 회복을 합쳐서 출력할 변수
            string recoveryMessage = "";

            // HP 회복
            if (item.hp > 0)
            {
                if (player.HP == player.MaxHP)
                {
                    recoveryMessage += $"플레이어의 HP는 이미 최대입니다. ";
                }
                else
                {
                    int newHP = Math.Min(player.HP + item.hp, player.MaxHP);
                    recoveryMessage += $"플레이어의 HP가 {item.hp}만큼 회복되었습니다. (현재 HP: {newHP}/{player.MaxHP}) ";
                    player.HP = newHP;
                    itemUsed = true; // 효과 적용됨
                }
            }

            // MP 회복
            if (item.mp > 0)
            {
                if (player.MP == player.MaxMP)
                {
                    recoveryMessage += $"플레이어의 MP는 이미 최대입니다. ";
                }
                else
                {
                    int newMP = Math.Min(player.MP + item.mp, player.MaxMP);
                    recoveryMessage += $"플레이어의 MP가 {item.mp}만큼 회복되었습니다. (현재 MP: {newMP}/{player.MaxMP}) ";
                    player.MP = newMP;
                    itemUsed = true; // 효과 적용됨
                }
            }

            // HP와 MP 모두 최대일 경우
            if (item.hp > 0 && player.HP == player.MaxHP && item.mp > 0 && player.MP == player.MaxMP)
            {
                recoveryMessage = "플레이어의 HP와 MP는 이미 최대입니다.";
            }

            // HP와 MP 모두 회복되었을 경우, 메시지 출력
            if (item.hp > 0 || item.mp > 0)
            {
                if (recoveryMessage != "")
                {
                    Console.WriteLine(recoveryMessage);
                }
            }

            return itemUsed; // 아이템이 사용되었는지 여부 반환
        }
    }


}
    

