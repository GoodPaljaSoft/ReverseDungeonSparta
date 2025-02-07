namespace ReverseDungeonSparta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 게임 시작
            AudioManager.PlayerMenuBGM();
            GameManager.Instance.GameMenu();
        }
    }
}
