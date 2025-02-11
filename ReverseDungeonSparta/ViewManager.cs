using System.Text;


namespace ReverseDungeonSparta
{
    static class ViewManager
    {
        //화면 넓이 받아오기
        static public int width = 120;      //콘솔 가로 크기
        static public int height = 30;      //콘솔 세로 크기

        static public int CursorX = 0;   //마우스 커서 x위치
        static public int CursorY = 0;   //마우스 커서 y위치

        //커서 위치 받아오기
        static int top = Console.WindowTop;
        static int left = Console.WindowLeft;

        public static void StateView(Player player)
        {
            //테스트 코드
            //DrawLine("상태보기");
            //DrawList(player);
            //DrawLine();

            //Console.WriteLine($"width: {width}");
            //Console.WriteLine($"height: {height}");
            //Console.WriteLine($"top: {top}");
            //Console.WriteLine($"left: {left}");


            //Console.ReadLine();


        }


        //한 줄을 길게 그리고 위에 제목을 출력하는 메소드
        public static void DrawLine(string sceneName)
        {

            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"\n {sceneName}");
            DrawLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.GetCursorPosition();
            //PrintText()
        }


        //한 줄을 길게 그리는 메서드
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

        
        //플레이어 정보를 출력할 메서드
        public static void PrintPlayerState(Player player)
        {
            PrintText(1, 3, $"{player.Name} (마왕)");

            Console.SetCursorPosition(0, 3);
            Console.WriteLine($"{player.Name} (마왕)");
            Console.WriteLine($" Lv. {player.Level} [{player.NowEXP}/{player.MaxEXP}]");
            Console.WriteLine();
            //Console.WriteLine($" Lv. {player.Level} [{player.Exp}/{player.MaxExp}]");
        }


        //해당 메서드로 커서 위치를 잡고 텍스트를 출력한다.
        //이 메서드로 출력한 텍스트를 기준으로 한 줄 아래에 그리고 싶다면
        //이 다음으로 PrintText로 출력하면 된다.
        public static void PrintText(int cursorX, int cursorY, string text)
        {
            CursorX = cursorX;
            CursorY = cursorY;
            Console.SetCursorPosition(CursorX, CursorY);
            Console.Write(text);
            CursorY++;
        }
        public static void PrintText(int cursorX, int cursorY, string text, bool isColor, ConsoleColor color)
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

        public static void PrintTextLine(int x, int y, string text)
        {
            CursorX = x;
            CursorY = y;
            Console.SetCursorPosition(x, y);
            Console.WriteLine(text);
            CursorY++;
        }


        //해당 메서드로 위치를 잡은 커서를 기준으로 텍스트를 출력한다.
        public static void PrintText(string text)
        {
            Console.SetCursorPosition(CursorX, CursorY);
            Console.Write(text);
            CursorY++;
        }
        public static void PrintText(string text, bool isColor, ConsoleColor color)
        {
            if (isColor)
                Console.ForegroundColor = color;

            Console.SetCursorPosition(CursorX, CursorY);
            Console.Write(text);

            if (isColor)
                Console.ForegroundColor = ConsoleColor.White;

            CursorY++;
        }


        public static void PrintList(List<EquipItem> items)
        {
            int[] x = { 3, 7, 24, 26, 33, 35, 45, 47 };
            int y = 3;
            //커서는 x==1 위치에 들어감
            for (int i = 0; i < items.Count; i++)
            {
                int[] optionArray = { items[i].AddLuck, items[i].AddDefence, items[i].AddAttack, items[i].AddIntelligence, items[i].AddMaxHp, items[i].AddMaxMp };
                string[] nameArray = { "AddLuck", "AddDefence", "AddAttack", "AddIntelligence", "AddMaxHp", "AddMaxMp" };
                if (items[i].IsEquiped) PrintTextLine(x[0], y + i, "[E]");
                else PrintTextLine(x[0], y + i, "[-]");
                PrintTextLine(x[1], y + i, $"{items[i].Name}");
                PrintTextLine(x[2], y + i, "|");
                PrintTextLine(x[3], y + i, $"{TranslateString(items[i].Type.ToString())}");
                PrintTextLine(x[7], y + i, $"{items[i].Information}");

                int count = 0;
                for (int j = 0; j < optionArray.Length; j++)
                {
                    if (optionArray[j] != 0)
                    {
                        PrintTextLine(x[4], y + i + count, "|");
                        PrintTextLine(x[5], y + i + count, $"{TranslateString(nameArray[j])} +{optionArray[j]}");
                        PrintTextLine(x[6], y + i + count, "|");
                        count++;
                    }
                }
                y += count;

            }
        }



        public static string TranslateString(string enumType)
        {
            switch(enumType)
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

            PrintText(1, 1, "██████  ███████ ██    ██ ███████ ██████  ███████ ███████");
            PrintText("██   ██ ██      ██    ██ ██      ██   ██ ██      ██");
            PrintText("██████  █████   ██    ██ █████   ██████  ███████ █████");
            PrintText("██   ██ ██       ██  ██  ██      ██   ██      ██ ██");
            PrintText("██   ██ ███████   ████   ███████ ██   ██ ███████ ███████");
            PrintText("");
            PrintText("");
            PrintText("██████  ██    ██ ███    ██  ██████  ███████  ██████  ███    ██");
            PrintText("██   ██ ██    ██ ████   ██ ██       ██      ██    ██ ████   ██");
            PrintText("██   ██ ██    ██ ██ ██  ██ ██   ███ █████   ██    ██ ██ ██  ██");
            PrintText("██   ██ ██    ██ ██  ██ ██ ██    ██ ██      ██    ██ ██  ██ ██");
            PrintText("██████   ██████  ██   ████  ██████  ███████  ██████  ██   ████");
            PrintText("");
            PrintText("");
            PrintText("███████ ██████   █████  ██████  ████████  █████");
            PrintText("██      ██   ██ ██   ██ ██   ██    ██    ██   ██");
            PrintText("███████ ██████  ███████ ██████     ██    ███████");
            PrintText("     ██ ██      ██   ██ ██   ██    ██    ██   ██");
            PrintText("███████ ██      ██   ██ ██   ██    ██    ██   ██");

            //Console.ReadLine();
        }


        //메인 씬에서 탑 그려줄 메서드
        public static void PrintTower()
        {
            Console.OutputEncoding = Encoding.UTF8;

            PrintText(width / 2 - 8, height / 2 - 7, "⠀⠀⠀⠀⠀⢀⡀", GameManager.Instance.clearCheck[0], ConsoleColor.Red);
            PrintText("⠀⠀⠀⠀⠀⣸⣧", GameManager.Instance.clearCheck[0], ConsoleColor.Red);
            PrintText("⠀⠀⢢⣶⣾⣥⣿⣷⣶⡖", GameManager.Instance.clearCheck[0], ConsoleColor.Red);
            PrintText("⠀⠀⢹⣻⠿⣿⣿⠿⣟⡏", GameManager.Instance.clearCheck[1], ConsoleColor.Red);
            PrintText("⠀⣤⢼⣿⡆⣻⣿⣶⣿⡧⣤", GameManager.Instance.clearCheck[2], ConsoleColor.Red);
            PrintText("⠀⣸⣸⢿⣧⣽⣿⣿⣿⣇⣿", GameManager.Instance.clearCheck[3], ConsoleColor.Red);
            PrintText("⠀⠿⢿⠿⡿⠿⠿⢿⣿⡯⠿", GameManager.Instance.clearCheck[4], ConsoleColor.Red);
            PrintText("⠀⠀⣾⠀⠁⠀⠀⢘⣿⣷", GameManager.Instance.clearCheck[5], ConsoleColor.Red);
            PrintText("⠀⠀⡟⠀⠀⢸⡞⢈⣿⣿", GameManager.Instance.clearCheck[6], ConsoleColor.Red);
            PrintText("⠀⢀⣥⣤⣤⣾⣷⣤⣿⣯⡀", GameManager.Instance.clearCheck[7], ConsoleColor.Red);
            PrintText("⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⡇", GameManager.Instance.clearCheck[8], ConsoleColor.Red);
            PrintText("⠀⠸⠿⠟⠛⠛⠛⠛⣿⣿⠇", GameManager.Instance.clearCheck[9], ConsoleColor.Red);
            PrintText("⠀⢸⡆⠀⠀⢸⡟⢘⣿⣿⡇", GameManager.Instance.clearCheck[10], ConsoleColor.Red);
            PrintText("⠀⢸⠃⠀⠀⠘⠃⠘⣿⣿⡇", GameManager.Instance.clearCheck[11], ConsoleColor.Red);
            PrintText("⠀⣼⣶⣾⣿⣿⣿⣿⣿⣿⣧", GameManager.Instance.clearCheck[12], ConsoleColor.Red);
            PrintText("⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿", GameManager.Instance.clearCheck[13], ConsoleColor.Red);
            PrintText("⠀⣿⡽⠹⠿⠿⠿⠿⣿⣿⣿", GameManager.Instance.clearCheck[14], ConsoleColor.Red);
            PrintText("⠀⡇⠀⠀⠀⠀⠀⠀⣿⣿⢿", GameManager.Instance.clearCheck[15], ConsoleColor.Red);
            PrintText("⠀⡏⠀⠀⠀⡶⢶⡄⣿⣿⣿", GameManager.Instance.clearCheck[16], ConsoleColor.Red);
            PrintText("⢰⠀⠀⠀⠀⣿⣿⡇⠉⣛⡋⡆", GameManager.Instance.clearCheck[17], ConsoleColor.Red);
            PrintText("⢸⣶⣶⣾⣿⣿⣿⣿⣶⣿⣿⡇", GameManager.Instance.clearCheck[18], ConsoleColor.Red);
            PrintText("⠈⠉⠉⠉⠉⠁⠀⠉⠉⠉⠉⠁", GameManager.Instance.clearCheck[18], ConsoleColor.Red);

            for(int i=0; i<19; i++)
            {
                PrintText(width / 2 + 6, height / 2 - 5 + i, "▼", GameManager.Instance.clearCheck[i], ConsoleColor.Red);
            }

            Console.ReadLine();
        }




    }
}
