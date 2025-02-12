﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseDungeonSparta
{
    public static class DataBase
    {
        //장착아이템, 사용아이템, 스킬, 몬스터, 플레이어 직업, 스토리라인 등 객체정보 보관용 이 정보들로 인스턴스 생성 및 프린트 해야함

        

        public static List<string> introText;   //이름 받기 전 까지의 출력 텍스트
        public static List<string> introText2;  //이름 받은 후의 출력 텍스트
        public static List<string> introText4;
        public static List<string> introText5;
        public static List<string> introText6;

        public static StringBuilder introText3 = new StringBuilder();   //이름 받기 전 까지의 출력 텍스트

        public static string playerName = string.Empty;


        public static void IntroTextInit()
        {

            

            

            introText = new List<string>
            {
                "%스파르타 던전───%\n\n",

                "그곳이 언제부터, 어떤 이유로 존재하는지 아는 사람은 아무도 없지만,\n",
                "오늘도 많은 모험가들이 자신의 힘을 시험하기 위해 스파르타 던전에 도전한다.\n",
                "그러나 이런 모험가들의 방문이 달갑지 않았던 존재가 하나 있었으니...\n\n",

                "*긴박한 BGM*\n\n",

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

            introText2 = new List<string>
            {
                $"\n%[~이름~]% 내 이름은... 마왕이 아니라 ~이름~(이)란 말이야!\n\n",

                "*칼싸움 소리*\n",
                "*상대 쓰러지는 소리*\n\n",

                $"%[~이름~]% ... 이대로는 안 되겠어.\n",
                $"%[~이름~]% 대체 무슨 헛소리가 돌고 있는지 내가 직접 내려가서 확인해야겠어!\n\n",
            };

            introText4 = new List<string>
            {
                "플레이어(은)는 스파르타 던전을 나왔다.",
                "1억 6천 년 만에 쬐는 햇빛은 좀 과하게 눈이 부셨지만, 따뜻했다.",

                "새들은 지저귀고, 꽃들은 피어나고... ... ... 아니, 왠지 이 이상 생각하면 안 될 것 같은데.",

                "아무튼 바깥은 플레이어의 집과 달리 평화로웠다. ",

                "[플레이어] 인간들은 이렇게 좋은 곳에 살면서 왜 어두컴컴하고 칙칙한 남의 보금자리까지 탐내는 거야?",

                "그게 바로 인간 종족의 특성이지만",
                "1억 7천년 만에 외출하는 플레이어는 알 길이 없다.",

                "[플레이어] 그런데...",
                "[플레이어] 일단 내려오기는 했는데, 이제 어떡하지?",

                "[1] 무단침입에 대한 피해보상을 청구한다.",
                "[2] 나온 김에 던전 주변을 깔끔하게 청소한다.",
            };

            introText5 = new List<string>
            {
                "[1] 무단침입에 대한 피해보상을 청구한다.",

                "플레이어(은)는 무단 침입에 대한 피해보상을 받아내기 위해",
                "사람들에게 물어물어 인간들의 총책임자를 찾아가게 된다.",
                "졸지에 마왕을 맞이하게 된 스파르타의 왕은",
                "대경실색하며 플레이어의 요구를 모두 들어주었다.",

                "플레이어는 왕의 태도에 매우 만족하며 자신의 보금자리로 돌아간다.",

                "인간들의 왕은 참으로 친절하니",
                "다음에 또 놀러 와야겠다고 생각하면서...",

                "[END 1] 인간들의 왕국에 당도한 것을 환영하오 마왕이여",

                "스파르타 왕국 존속",
                "플레이어(은)는 주기적으로 스파르타 왕국을 방문하며 교류를 이어갑니다.",
            };

            introText6 = new List<string>
            {
                "[2] 나온 김에 던전 주변을 깔끔하게 청소한다.",

                "플레이어(은)는 밖으로 나온 김에 1억 8천 년 만의 대청소를 결심한다.",
                "... 아니, 1억 7천 년 만이던가? 아니면 1억 6천 년?",
                "마왕은 자신의 나이를 어림잡아보다 그만둔다.",
                "원래 나이란 건 스무 살만 넘어가도 금방 가물가물해지는 법이다.",
                "...",
                "...",
                "아무튼 플레이어(은)는 장장 반올림 2억 년 만의 청소를 시작했다.",
                "청소 거리가 쌓여 쉽지 않았지만, 쉬지 않고 매달리니 금세 끝낼 수 있었다.",
                "플레이어(은)는 뿌듯한 마음을 안고 자신의 안락한 보금자리로 돌아간다.",
                "이만큼 깨끗이 청소했으니, 한동안은 평화롭겠지!",

                "[END 2] 깨끗한 시간을 보내고 싶어?",

                "스파르타 왕국 멸망",
                "앞으로도 플레이어(은)는 주기적으로 청소를 나옵니다.",
            };


        }







    }
}
