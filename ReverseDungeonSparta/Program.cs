namespace ReverseDungeonSparta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ViewManager.PrintTower();
            // 게임 시작
            Console.SetWindowSize(200, 30);         //콘솔창 크기 지정
            Console.SetBufferSize(200, 100);
            AudioManager.PlayMenuBGM();
            GameManager.Instance.GameMenu();
        }
    }
}
