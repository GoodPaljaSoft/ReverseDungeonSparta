using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NPOI.OpenXmlFormats.Wordprocessing;

namespace ReverseDungeonSparta
{
    internal static class AudioManager
    {
        static IWavePlayer bgmPlayer;
        static AudioFileReader bgmReader;

        static IWavePlayer SE_Player;
        static AudioFileReader SE_Reader;

        static float bgmVolume = 0.2f;
        static float seVolme = 0.1f;

        static bool isBGM_Player = false;
        static bool isPlayerDie = false;


        #region 음악 경로 지정 관련 필드 모음
        static string pathMusicFolder = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName, "music\\");

        static string pathMenuBGM = pathMusicFolder + "MenuBGM.wav";                 //메뉴 배경 음악
        static string pathBattleBGM = pathMusicFolder + "BattleBGM.wav";                //배틀 배경 음악
        static string pathPlayerDieBGM = pathMusicFolder + "PlayerDieBGM.mp3";          //플레이어 사망 시 배경 음악

        static string pathMoveMenuSE = pathMusicFolder + "MenuMove.mp3";                //메뉴가 나타날 때마다 나오는 기본 효과음
        static string pathWalkSE = pathMusicFolder + "Walk.wav";                //로딩 창 걷는 효과음
        static string pathItemUpgradeSE = pathMusicFolder + "ItemUpgrade.mp3";                //플레이어가 아이템 조합 할 때 효과음
        static string pathUseUseItemSE = pathMusicFolder + "PlayerUseUseItem.mp3";                //플레이어가 사용 아이템 사용할 때 효과음
        static string pathOnDamageSE = pathMusicFolder + "OnDamage.mp3";                //플레이어가 사용 아이템 사용할 때 효과음
        static string pathItemEquippedSE = pathMusicFolder + "ItemEquipped.mp3";        //아이템 장착 및 해제시 나오는 효과음
        static string pathDungeonClearSE = pathMusicFolder + "DungeonClear.mp3";        //던전 클리어 시 나오는 효과음
        static string pathDungeonFailedSE = pathMusicFolder + "DungeonFailed.mp3";      //던전 실패 시 나오는 효과음
        static string pathLevelUpSE = pathMusicFolder + "PlayerLevelUp.mp3";            //플레이어의 레벨이 오를 경우 나오는 효과음
        static string pathPlayerDieSE = pathMusicFolder + "PlayerDie.mp3";              //플레이어 사망 시 나오는 효과음
        static string pathAttackSlashSE = pathMusicFolder + "AttackSlash.mp3";          //베는 공격을 했을 때 나오는 효과음
        static string pathAttackArrowSE = pathMusicFolder + "AttackArrow.wav";           //화살이 날라가는 효과음
        static string pathAttackClubSE = pathMusicFolder + "AttackClub.wav";             //몽둥이로 때리는 효과음
        static string pathAttackFireSE = pathMusicFolder + "AttackFire.mp3";             //화염 마법을 쓰는 효과음
        static string pathHealingSE = pathMusicFolder + "Healing.mp3";                   //힐링 효과음
        #endregion


        #region 효과음 실행 메서드 모음
        //delayTime은 1000 기준 1초
        public static async void PlayMoveMenuSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathMoveMenuSE);
        }       //메뉴 이동 효과음
        public static async void PlayItemUpgradeSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathItemUpgradeSE);
        }       //아이템 강화 효과음
        public static async void PlayWalkSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathWalkSE);
        }       //아이템 강화 효과음
        public static async void PlayOnDamageSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathOnDamageSE);
        }       //피격 데미지
        public static async void PlayerUseUseItemSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathUseUseItemSE);
        }       //플레이어가 사용 아이템 사용 시 효과음
        public static async void PlayItemEquippedSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathItemEquippedSE);
        }      //장비 장착 및 해제 효과음
        public static async void PlayDungeonClearSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathDungeonClearSE);
        }       //던전 성공 효과음
        public static async void PlayDungeonFailedSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathDungeonFailedSE);
        }       //던전 실패 효과음
        public static async void PlayLevelUpSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathLevelUpSE);
        }       //레벨 업 효과음
        public static async void PlayPlayerDieSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathPlayerDieSE);
        }       //플레이어 사망 효과음
        public static async void PlayAttackSlashSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathAttackSlashSE);
        }       //검 효과음
        public static async void PlayAttackArrowSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathAttackArrowSE);
        }       //화살 효과음
        public static async void PlayAttackClubSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathAttackClubSE);
        }       //몽둥이 효과음
        public static async void PlaypathAttackFireSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathAttackFireSE);
        }       //화염 마법 효과음
        public static async void PlayHealingSE(int delayTime)
        {
            await Task.Delay(delayTime);
            SettingSE(pathHealingSE);
        }       //힐 효과음
        #endregion


        #region 배경음악 실행 메서드 모음
        public static void PlayMenuBGM()
        {
            BGM_Start(pathMenuBGM);
        }       //기본 메뉴 배경음악 실행
        public static void PlayBattleBGM()
        {
            BGM_Start(pathBattleBGM);
        }       //베틀 배경음악 실행
        public static void PlayPlayerDieBGM()
        {
            BGM_Start(pathPlayerDieBGM);
        }       //플레이어가 사망 배경음악 실행
        #endregion

        private static object seLock = new object();
        //효과음을 세팅하고 실행하는 메서드
        static void SettingSE(string filePath)
        {
            lock (seLock)
            {
                StopPlayerAndReader(SE_Player, SE_Reader); // 이미 사용 중인 플레이어 정리

                SE_Player = new WaveOutEvent();
                SE_Reader = new AudioFileReader(filePath) { Volume = seVolme };

                SE_Player.Init(SE_Reader);
                SE_Player.Play();
            }
        }


        //배경음악을 집어넣는 메서드
        static void SettingBGM(string filePath)
        {
            StopPlayerAndReader(bgmPlayer, bgmReader);

            bgmPlayer = new WaveOutEvent();     //배경음악 플레이어 생성
            bgmReader = new AudioFileReader(filePath)       //배경음악 파일 불러오기
            {
                Volume = bgmVolume    //볼륨 조절
            };
            bgmPlayer.Init(bgmReader);      //배경음악 플레이어에 음악 집어넣기

            bgmPlayer.Play();

            bgmPlayer.PlaybackStopped += (sender, arg) =>
            {
                if (isPlayerDie == false && isBGM_Player == false)
                {
                    SettingBGM(filePath);
                }
            };
        }


        //BGM종료
        public static void StopBGM()
        {
            isPlayerDie = true;
            StopPlayerAndReader(bgmPlayer, bgmReader);
            isPlayerDie = false;
        }


        //연결된 플레이어와 리더를 해제하는 메서드
        static void StopPlayerAndReader(IWavePlayer wavePlayer, AudioFileReader audioFileReader)
        {
            // 이미 null 혹은 Dispose된 플레이어인지 추가 확인
            if (wavePlayer != null)
            {
                if (wavePlayer.PlaybackState != PlaybackState.Stopped)
                {
                    wavePlayer.Stop();
                }
                wavePlayer.Dispose();
            }
            audioFileReader?.Dispose();
        }


        //배경 음악 실행을 시작하는 메서드
        static async Task BGM_Start(string path)
        {
            isBGM_Player = true;
            SettingBGM(path);
            isBGM_Player = false;
            while (true)
            {
                if(isPlayerDie == true)
                {
                    StopPlayerAndReader(bgmPlayer, bgmReader);
                    isPlayerDie = false;
                    break;
                }

                await Task.Delay(100);
            }
        }
    }
}
