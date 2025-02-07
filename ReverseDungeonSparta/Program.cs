namespace ReverseDungeonSparta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 게임 시작
            AudioManager.PlayMenuBGM();
            GameManager.Instance.GameMenu();
        }
    }
}
