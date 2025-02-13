using System.Runtime.InteropServices;

namespace ReverseDungeonSparta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //시작하기 전 콘솔 창 설정
            Console.SetBufferSize(120, 300);            //버퍼 사이즈 지정 //넉넉하게 하지 않으면 터짐
            Console.SetWindowSize(120, 30);             //콘솔창 크기 지정
            
            Console.Title = "REVERSE DUNGEON : SPARTA"; //콘솔창 제목 지정

            ViewManager.width = Console.WindowWidth;
            ViewManager.height = Console.WindowHeight;

            GameManager.Instance.IntroScene();
            // 게임 시작
            AudioManager.PlayMenuBGM();
            GameManager.Instance.TitleSMenu();
        }
    }
}
