
using System.Drawing;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ReverseDungeonSparta
{


    static class ViewManager
    {
        //colorWord: 컬러 출력을 하고 싶은 단어를 키워드로 저장한 딕셔너리 (값은 컬러)
        public static Dictionary<string, ConsoleColor> colorWord = new Dictionary<string, ConsoleColor>();

        //화면 넓이 받아오기
        static public int width = 120;      //콘솔 가로 크기
        static public int height = 30;      //콘솔 세로 크기

        static public int CursorX = 0;   //마우스 커서 x위치
        static public int CursorY = 0;   //마우스 커서 y위치


        //커서 위치 받아오기
        static int top = Console.WindowTop;
        static int left = Console.WindowLeft;


        public static void ViewInit()
        {
            colorWord.Add("스파르타 던전───", ConsoleColor.Red);
            colorWord.Add("[마왕]", ConsoleColor.Green);
            colorWord.Add("[모험가]", ConsoleColor.Red);

            colorWord.Add("[END 1]", ConsoleColor.Cyan);
            colorWord.Add("[END 2]", ConsoleColor.Cyan);

            colorWord.Add("[1]", ConsoleColor.Cyan);
            colorWord.Add("[2]", ConsoleColor.Cyan);
        }



        // DrawLine 1
        // 한 줄을 길게 그리는 메서드
        public static void DrawLine()
        {
            string str = "";
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < width; i++)
            {
                str += "─";
            }
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        //높이 지정하고 한 줄을 길게 그림
        public static void DrawLine(int y)
        {
            Console.SetCursorPosition(0, y);
            DrawLine();
        }

        // DrawLine 2
        // 한 줄을 길게 그리고 장면 이름을 출력하는 메소드
        public static void DrawLine(string sceneName)
        {
            PrintText(1, 1, sceneName, ConsoleColor.Red);
            DrawLine(2);
        }

        // DrawLine 3
        // 한 줄을 길게 그리고 장면 이름과 정보를 출력하는 메소드
        public static void DrawLine(string sceneName, string sceneInfo)
        {
            PrintText(1, 1, sceneName, ConsoleColor.Red);
            int x = Console.GetCursorPosition().Left;
            PrintText(x + 10, 1, sceneInfo, ConsoleColor.Gray); //정보는 회색
            DrawLine(2);
        }


        // PrintText 1-1
        // 커서 위치와 텍스트를 받아와 해당 위치에 출력하는 메서드
        public static void PrintText(int cursorX, int cursorY, string text)
        {
            CursorX = cursorX;
            CursorY = cursorY;
            Console.SetCursorPosition(CursorX, CursorY);
            Console.Write(text);
            CursorY++;
        }

        // PrintText 1-2
        // 텍스트를 출력하고 커서를 한 줄 내린다.
        // 좌표를 받아오지 않으므로 앞에서 위치를 잡은 경우에만 사용
        public static void PrintText(string text)
        {
            Console.SetCursorPosition(CursorX, CursorY);
            Console.Write(text);
            CursorY++;
        }

        // PrintText 2-1
        // x,y좌표와 텍스트, 컬러를 받아와 해당 컬러값으로 텍스트를 출력하는 메서드
        public static void PrintText(int cursorX, int cursorY, string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            CursorX = cursorX;
            CursorY = cursorY;
            Console.SetCursorPosition(CursorX, CursorY);
            Console.Write(text);

            Console.ForegroundColor = ConsoleColor.White;

            CursorY++;
        }

        // PrintText 2-2
        // 텍스트, 컬러를 받아와 해당 컬러값으로 텍스트를 출력하는 메서드
        // 좌표를 받아오지 않으므로 앞에서 위치를 잡은 경우에만 사용
        public static void PrintText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;

            Console.SetCursorPosition(CursorX, CursorY);
            Console.Write(text);

            Console.ForegroundColor = ConsoleColor.White;

            CursorY++;
        }

        // PrintText 3-1
        // 컬러 출력을 동적으로 관리하고 싶을 때 사용
        // bool값을 받아와서 isColor가 true되면 받은 color값을 출력한다.
        public static void PrintText(int cursorX, int cursorY, string text, ConsoleColor color, bool isColor)
        {
            if (isColor)
                Console.ForegroundColor = color;

            CursorX = cursorX;
            CursorY = cursorY;
            Console.SetCursorPosition(CursorX, CursorY);
            Console.Write(text);

            if (isColor)
                Console.ForegroundColor = ConsoleColor.White;

            CursorY++;
        }

        // PrintText 3-2
        // 컬러 출력을 동적으로 관리하고 싶을 때 사용
        // bool값을 받아와서 isColor가 true되면 받은 color값을 출력한다.
        // 좌표를 받아오지 않으므로 앞에서 위치를 잡은 경우에만 사용
        public static void PrintText(string text, ConsoleColor color, bool isColor)
        {
            if (isColor)
                Console.ForegroundColor = color;

            Console.SetCursorPosition(CursorX, CursorY);
            Console.Write(text);

            if (isColor)
                Console.ForegroundColor = ConsoleColor.White;

            CursorY++;
        }

        //string을 받아와 char 단위로 하나씩 출력해준다.
        public static void PrintTextAnimation(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Console.Write(text.Substring(i, 1));
                Thread.Sleep(100);
            }
        }
        //string을 받아와 지정한 좌표값에 char 단위로 하나씩 출력해준다.
        public static void PrintTextAnimation(int x, int y, string text)
        {
            Console.SetCursorPosition(x, y);
            PrintTextAnimation(text);
        }

        //string 타입 List를 받아와 char 단위로 하나씩 출력해준다.
        public static void PrintTextAnimation(List<string> textList)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var str in textList)
            {
                sb.Append(str);
            }

            for (int i = 0; i < sb.Length; i++)
            {
                Console.Write(sb[i]);
            }
        }



  
        public static void PrintLongTextAnimation(string text)
        {
            string[] str = null;

            //colorWord의 키 값들을 받아옴
            ICollection<string> colorKeyword = colorWord.Keys;

            //받은 text에 키 값이 포함되어 있는지 확인해줌
            foreach (string key in colorKeyword)
            {
                //만약 있다면 %을 기준으로 텍스트를 나눠준다
                if(text.Contains(key))
                {
                    str = text.Split('%');
                }
            }

            //컬러로 출력할 것들이 없었다면 텍스트를 그대로 출력
            if (str == null) Console.Write(text);
            else
            {
                for (int i = 0; i < str.Length; i++)
                {
                    //TryGetValue: 
                    if (colorWord.TryGetValue(str[i], out ConsoleColor textColor))
                    {
                        Console.ForegroundColor = textColor;
                        Console.Write(str[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(str[i]);
                    }
                }
            }
        }

        public static void PrintLongTextAnimation(List<string> textList)
        {
            foreach(string str in textList)
            {
                PrintLongTextAnimation(str);
                Thread.Sleep(1000);
            }
        }

        public static void PrintList(List<EquipItem> items)
        {
            int[] x = { 3, 7, 29, 32, 39, 41, 51, 53 };
            int y = 3;
            //커서는 x==1 위치에 들어감
            for (int i = 0; i < items.Count; i++)
            {
                int[] optionArray = { items[i].AddLuck, items[i].AddDefence, items[i].AddAttack, items[i].AddIntelligence, items[i].AddMaxHp, items[i].AddMaxMp };
                string[] nameArray = { "AddLuck", "AddDefence", "AddAttack", "AddIntelligence", "AddMaxHp", "AddMaxMp" };
                if (items[i].IsEquiped) PrintText(x[0], y + i, "[E]");
                else PrintText(x[0], y + i, "[-]");
                PrintText(x[1], y + i, $"{items[i].Name}");
                PrintText(x[2], y + i, "|");
                PrintText(x[3], y + i, $"{TranslateString(items[i].Type.ToString())}");
                PrintText(x[7], y + i, $"{items[i].Information}");

                int count = 0;
                for (int j = 0; j < optionArray.Length; j++)
                {
                    if (optionArray[j] != 0)
                    {
                        PrintText(x[4], y + i + count, "|");
                        PrintText(x[5], y + i + count, $"{TranslateString(nameArray[j])} +{optionArray[j]}");
                        PrintText(x[6], y + i + count, "|");
                        count++;
                    }
                }
                y += count;

            }
        }

        //플레이어 정보를 출력할 메서드
        public static void PrintPlayerState(Player player)
        {
            PrintText(0, 3, $"{player.Name} (마왕)");
            PrintText($"Lv. {player.Level} [{player.NowEXP}/{player.MaxEXP}]");
            PrintText("");
            PrintText($"HP : {player.HP}/{player.MaxHP}");
            PrintText($"MP : {player.MP}/{player.MaxMP}");
            PrintText("");
            DrawLine();
        }

        //플레이어 이름 변경 갱신 메서드
        public static List<string> ChangePlayerName(List<string> textList)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var line in textList)
            {
                sb.AppendLine(line);  // 줄바꿈 포함하여 추가
            }

            sb.Replace("~이름~", DataBase.playerName);

            sb.Replace("이가", AddPostposition(DataBase.playerName, "이가"));
            sb.Replace("은는", AddPostposition(DataBase.playerName, "은는"));
            sb.Replace("을를", AddPostposition(DataBase.playerName, "을를"));
            sb.Replace("과와", AddPostposition(DataBase.playerName, "과와"));


            return sb.ToString().Split("&").ToList();
        }


        // 한글 조사 (은/는, 이/가) 자동 선택 함수
        public static string AddPostposition(string word, string postpositionType)
        {
            if (string.IsNullOrEmpty(word))
                return word;

            char lastChar = word[word.Length - 1];

            // 한글 유니코드 범위 체크
            if (lastChar >= 0xAC00 && lastChar <= 0xD7A3)
            {
                int unicodeIndex = lastChar - 0xAC00;
                int jongseongIndex = unicodeIndex % 28; // 종성 확인

                bool hasJongseong = jongseongIndex != 0; // 종성이 있으면 true

                switch (postpositionType)
                {
                    case "은는":
                        return hasJongseong ? "은" : "는";
                    case "이가":
                        return hasJongseong ? "이" : "";
                    case "을를":
                        return hasJongseong ? "을" : "를";
                    case "과와":
                        return hasJongseong ? "과" : "와";
                    default:
                        throw new ArgumentException("올바른 조사 유형이 아닙니다. (예: '은는', '이가', '을를', '과와')");
                }
            }

            // 한글이 아닐 경우 기본 조사 적용
            return postpositionType == "은는" ? "는" : "가";
        }


        public static string TranslateString(string enumType)
        {
            switch (enumType)
            {
                case "Armor":
                    return "방어구";
                case "Weapon":
                    return "무  기";
                case "Helmet":
                    return "모  자";
                case "Shoes":
                    return "신  발";
                case "Ring":
                    return "반  지";
                case "Necklace":
                    return "목걸이";
                case "AddLuck":
                    return "행  운";
                case "AddDefence":
                    return "방어력";
                case "AddAttack":
                    return "공격력";
                case "AddIntelligence":
                    return "지  력";
                case "AddMaxHp":
                    return "체  력";
                case "AddMaxMp":
                    return "마  나";
                default:
                    return "TranslateError";
            }
        }

        //타이틀 출력
        public static void PrintTitle()
        {
            Console.OutputEncoding = Encoding.UTF8;

            PrintText(1, 1, "██████  ███████ ██    ██ ███████ ██████  ███████ ███████");
            PrintText("██   ██ ██      ██    ██ ██      ██   ██ ██      ██");
            PrintText("██████  █████   ██    ██ █████   ██████  ███████ █████");
            PrintText("██   ██ ██       ██  ██  ██      ██   ██      ██ ██");
            PrintText("██   ██ ███████   ████   ███████ ██   ██ ███████ ███████");
            PrintText("");
            PrintText("");
            PrintText("██████  ██    ██ ███    ██  ██████  ███████  ██████  ███    ██");
            PrintText("██   ██ ██    ██ ████   ██ ██       ██      ██    ██ ████   ██");
            PrintText("██   ██ ██    ██ ██ ██  ██ ██   ███ █████   ██    ██ ██ ██  ██");
            PrintText("██   ██ ██    ██ ██  ██ ██ ██    ██ ██      ██    ██ ██  ██ ██");
            PrintText("██████   ██████  ██   ████  ██████  ███████  ██████  ██   ████");
            PrintText("");
            PrintText("");
            PrintText("███████ ██████   █████  ██████  ████████  █████");
            PrintText("██      ██   ██ ██   ██ ██   ██    ██    ██   ██");
            PrintText("███████ ██████  ███████ ██████     ██    ███████");
            PrintText("     ██ ██      ██   ██ ██   ██    ██    ██   ██");
            PrintText("███████ ██      ██   ██ ██   ██    ██    ██   ██");

            //Console.ReadLine();
        }


        //메인 씬에서 탑 그려줄 메서드
        public static void PrintTower()
        {
            Console.OutputEncoding = Encoding.UTF8;
            //GameManager GM = GameManager.Instance;

            PrintText(width / 2 - 8, height / 2 - 7, "⠀⠀⠀⠀⠀⢀⡀", ConsoleColor.Red, GameManager.Instance.clearCheck[0]);
            PrintText("⠀⠀⠀⠀⠀⣸⣧", ConsoleColor.Red, GameManager.Instance.clearCheck[0]);
            PrintText("⠀⠀⢢⣶⣾⣥⣿⣷⣶⡖", ConsoleColor.Red, GameManager.Instance.clearCheck[0]);
            PrintText("⠀⠀⢹⣻⠿⣿⣿⠿⣟⡏", ConsoleColor.Red, GameManager.Instance.clearCheck[1]);
            PrintText("⠀⣤⢼⣿⡆⣻⣿⣶⣿⡧⣤", ConsoleColor.Red, GameManager.Instance.clearCheck[2]);
            PrintText("⠀⣸⣸⢿⣧⣽⣿⣿⣿⣇⣿", ConsoleColor.Red, GameManager.Instance.clearCheck[3]);
            PrintText("⠀⠿⢿⠿⡿⠿⠿⢿⣿⡯⠿", ConsoleColor.Red, GameManager.Instance.clearCheck[4]);
            PrintText("⠀⠀⣾⠀⠁⠀⠀⢘⣿⣷", ConsoleColor.Red, GameManager.Instance.clearCheck[5]);
            PrintText("⠀⠀⡟⠀⠀⢸⡞⢈⣿⣿", ConsoleColor.Red, GameManager.Instance.clearCheck[6]);
            PrintText("⠀⢀⣥⣤⣤⣾⣷⣤⣿⣯⡀", ConsoleColor.Red, GameManager.Instance.clearCheck[7]);
            PrintText("⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⡇", ConsoleColor.Red, GameManager.Instance.clearCheck[8]);
            PrintText("⠀⠸⠿⠟⠛⠛⠛⠛⣿⣿⠇", ConsoleColor.Red, GameManager.Instance.clearCheck[9]);
            PrintText("⠀⢸⡆⠀⠀⢸⡟⢘⣿⣿⡇", ConsoleColor.Red, GameManager.Instance.clearCheck[10]);
            PrintText("⠀⢸⠃⠀⠀⠘⠃⠘⣿⣿⡇", ConsoleColor.Red, GameManager.Instance.clearCheck[11]);
            PrintText("⠀⣼⣶⣾⣿⣿⣿⣿⣿⣿⣧", ConsoleColor.Red, GameManager.Instance.clearCheck[12]);
            PrintText("⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿", ConsoleColor.Red, GameManager.Instance.clearCheck[13]);
            PrintText("⠀⣿⡽⠹⠿⠿⠿⠿⣿⣿⣿", ConsoleColor.Red, GameManager.Instance.clearCheck[14]);
            PrintText("⠀⡇⠀⠀⠀⠀⠀⠀⣿⣿⢿", ConsoleColor.Red, GameManager.Instance.clearCheck[15]);
            PrintText("⠀⡏⠀⠀⠀⡶⢶⡄⣿⣿⣿", ConsoleColor.Red, GameManager.Instance.clearCheck[16]);
            PrintText("⢰⠀⠀⠀⠀⣿⣿⡇⠉⣛⡋⡆", ConsoleColor.Red, GameManager.Instance.clearCheck[17]);
            PrintText("⢸⣶⣶⣾⣿⣿⣿⣿⣶⣿⣿⡇", ConsoleColor.Red, GameManager.Instance.clearCheck[18]);
            PrintText("⠈⠉⠉⠉⠉⠁⠀⠉⠉⠉⠉⠁", ConsoleColor.Red, GameManager.Instance.clearCheck[18]);

            for (int i = 0; i < 19; i++)
            {
                PrintText(width / 2 + 6, height / 2 - 5 + i, "▼", ConsoleColor.Red, GameManager.Instance.clearCheck[i]);
            }
        }

        //메인 메뉴 창에서 텍스트 출력하는 메소드
        public static void MainMenuTxt()
        {
            Console.Clear();
            PrintTower();
            PrintText(3, 25, "   상태확인");
            PrintText("   소지품 확인");
            PrintText("   내려가기");
            PrintText("   저장하기");
            PrintText("   게임종료");
        }

        public static void TitleMenuTxt()
        {
            Console.Clear();
            PrintTitle();
            PrintText(100, 24, "   새로하기");
            PrintText("   이어하기");
            PrintText("   게임종료");
        }

        public static void PrintCurrentFloors(int floors)
        {
            PrintText(width - 30, 1, $"현재 위치: 스파르타 던전 {floors}층", ConsoleColor.Red);
        }
    }
}