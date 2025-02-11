namespace ReverseDungeonSparta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //테스트 출력
            //ViewManager.PrintTower();
            // 게임 시작
            Console.SetWindowSize(120, 30);         //콘솔창 크기 지정
            Console.SetBufferSize(120, 200);
            AudioManager.PlayMenuBGM();
            GameManager.Instance.GameMenu();
        }
    }
}
