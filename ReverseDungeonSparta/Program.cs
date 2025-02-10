namespace ReverseDungeonSparta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ViewManager.PrintTower();
            // 게임 시작
            AudioManager.PlayMenuBGM();
            GameManager.Instance.GameMenu();
        }
    }
}
