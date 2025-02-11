namespace ReverseDungeonSparta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 게임 시작
            Console.SetWindowSize(120, 30);         //콘솔창 크기 지정
            Console.SetBufferSize(120, 200);
            Console.Title = "REVERSE DUNGEON : SPARTA";
            ViewManager.width = Console.WindowWidth;
            ViewManager.height = Console.WindowHeight;

            AudioManager.PlayMenuBGM();
            //GameManager.Instance.GameMenu();
            GameManager.Instance.TitleSMenu();
        }
    }
}
