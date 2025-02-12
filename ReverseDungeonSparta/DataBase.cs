using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseDungeonSparta
{
    public static class DataBase
    {
        //장착아이템, 사용아이템, 스킬, 몬스터, 플레이어 직업, 스토리라인 등 객체정보 보관용 이 정보들로 인스턴스 생성 및 프린트 해야함

        public static List<string> introText;

            
        public static void IntroTextInit()
        {
            introText = new List<string>
            {
                "스파르타 던전───\n\n",

                "그곳이 언제부터, 어떤 이유로 존재하는지 아는 사람은 아무도 없지만,\n",
                "오늘도 많은 모험가들이 자신의 힘을 시험하기 위해 스파르타 던전에 도전한다.\n",
                "그러나 이런 모험가들의 방문이 달갑지 않았던 존재가 하나 있었으니...\n\n",

                "*긴박한 BGM*\n\n",

                "[모험가] 마왕! 오늘이야말로 네 목을 가져가겠다!\n\n",

                "[마왕] ... ... 아! 제발!\n",
                "[마왕] 너희 인간들에겐 에티켓이라는 개념이 존재하지 않는 거야?\n\n",

                "[모험가] 무슨 마왕이 에티켓을 따져?\n\n",

                "[마왕] 모르는 사람들이 내 집에서 난장판을 피우는데 안 따지게 생겼어?!\n\n",

                "[모험가] ... ... 알 게 뭐야! 죽어라!!!\n\n",

                "*칼싸움 소리*\n\n",

                "[마왕] ... 정말 지긋지긋해 죽겠어!\n",
                "[마왕] 집값이 싼 건 다 이유가 있었다고.\n",
                "[마왕] 애초에 왜 내가 마왕이라고 불리게 된 거야?\n",
                "[마왕] 나도 제대로 된 이름이 있다고!\n\n",

                "내 이름은...\n",
                ">>"

            };
        }







    }

    






}
