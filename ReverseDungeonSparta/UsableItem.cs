using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Monster;

namespace ReverseDungeonSparta
{
    public class UsableItem : Item
    {
        int Hp {  get; set; }
        int Mp { get; set; }
        public UsableItem(UsableItemInfo usableItemInfo)
        {

        }
        public UsableItem()
        {

        }

        // 소비 아이템 구조체
        public struct UsableItemInfo
        {
            public int hp;
            public int mp;

            public UsableItemInfo(int _hp, int _mp)
            {
                hp = _hp;
                mp = _mp;
            }
        }

        public static UsableItemInfo[] allUsableItem =
        {
            new UsableItemInfo(50, 0), // 체력 포션
            new UsableItemInfo(0, 50), // 마나 포션
            new UsableItemInfo(50, 50), // 만능 포션
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

    }
}
