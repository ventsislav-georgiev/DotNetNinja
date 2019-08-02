using DotNetNinja.Core.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows.Forms;
using WMPLib;

namespace DotNetNinja.Core
{
    internal static class Sounds
    {
        private static readonly WindowsMediaPlayer player = new WindowsMediaPlayer();
        private static readonly SoundPlayer healthUp = new SoundPlayer(ContentResolver.GetEmbeddedResourceStream(@"Content\Sounds\eatHealth.wav"));
        private static readonly SoundPlayer manaUp = new SoundPlayer(ContentResolver.GetEmbeddedResourceStream(@"Content\Sounds\manaDrink.wav"));
        private static readonly SoundPlayer defenseUp = new SoundPlayer(ContentResolver.GetEmbeddedResourceStream(@"Content\Sounds\DefenseUp.wav"));
        private static readonly SoundPlayer knowledgeUp = new SoundPlayer(ContentResolver.GetEmbeddedResourceStream(@"Content\Sounds\powerUp.wav"));
        private static readonly SoundPlayer levelUp = new SoundPlayer(ContentResolver.GetEmbeddedResourceStream(@"Content\Sounds\levelUp.wav"));
        private static readonly SoundPlayer bossFight = new SoundPlayer(ContentResolver.GetEmbeddedResourceStream(@"Content\Sounds\bossFight.wav"));
        private static readonly SoundPlayer studentFight = new SoundPlayer(ContentResolver.GetEmbeddedResourceStream(@"Content\Sounds\studentFight.wav"));
        private static readonly SoundPlayer doorOpen = new SoundPlayer(ContentResolver.GetEmbeddedResourceStream(@"Content\Sounds\doorOpen.wav"));
        private static readonly SoundPlayer magic = new SoundPlayer(ContentResolver.GetEmbeddedResourceStream(@"Content\Sounds\magicSound.wav"));
        private static readonly SoundPlayer pickUp = new SoundPlayer(ContentResolver.GetEmbeddedResourceStream(@"Content\Sounds\pickup.wav"));
        private static readonly SoundPlayer start = new SoundPlayer(ContentResolver.GetEmbeddedResourceStream(@"Content\Sounds\start.wav"));

        static Sounds()
        {
            healthUp.Load();
            manaUp.Load();
            defenseUp.Load();
            knowledgeUp.Load();
            levelUp.Load();
            bossFight.Load();
            studentFight.Load();
            doorOpen.Load();
            magic.Load();
            pickUp.Load();
            start.Load();
        }

        public static void Play(string path)
        {
            var filePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(path));
            if (filePath != player.URL)
            {
                if (!File.Exists(filePath))
                {
                    using (var stream = File.OpenWrite(filePath))
                    {
                        ContentResolver.GetEmbeddedResourceStream(path).CopyTo(stream);
                    }
                }

                StopSound();
                player.URL = filePath;
                player.settings.setMode("loop", true);
                player.controls.play();
            }
        }

        public static void PlayBackgroundSound(LevelType levelType)
        {
            switch (levelType)
            {
                case LevelType.Start:
                case LevelType.Level1:
                case LevelType.Level2:
                case LevelType.Level3:
                    Play(@"Content\Sounds\level1.mp3");
                    break;

                case LevelType.Level4:
                    Play(@"Content\Sounds\boss.mp3");
                    break;

                case LevelType.Level5:
                case LevelType.Level6:
                case LevelType.Level7:
                    Play(@"Content\Sounds\level2.mp3");
                    break;

                case LevelType.Level8:
                    Play(@"Content\Sounds\boss.mp3");
                    break;

                case LevelType.Level9:
                case LevelType.Level10:
                case LevelType.Level11:
                    Play(@"Content\Sounds\level3.mp3");
                    break;

                case LevelType.Level12:
                    Play(@"Content\Sounds\boss.mp3");
                    break;

                case LevelType.Level13:
                case LevelType.Level14:
                    Play(@"Content\Sounds\level4.mp3");
                    break;

                case LevelType.Level15:
                    Play(@"Content\Sounds\boss.mp3");
                    break;

                case LevelType.Level16:
                    Play(@"Content\Sounds\boss5.mp3");
                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        public static void StopSound()
        {
            player.controls.stop();
        }

        public static void HealthUp()
        {
            healthUp.Play();
        }

        public static void ManaUp()
        {
            manaUp.Play();
        }

        public static void DefenseUp()
        {
            defenseUp.Play();
        }

        public static void KnowledgeUp()
        {
            knowledgeUp.Play();
        }

        public static void LevelUp()
        {
            levelUp.Play();
        }

        public static void BossFight()
        {
            bossFight.Play();
        }

        public static void StudentFight()
        {
            studentFight.Play();
        }

        public static void DoorOpen()
        {
            doorOpen.Play();
        }

        public static void Magic()
        {
            magic.Play();
        }

        public static void Pickup()
        {
            pickUp.Play();
        }

        public static void Win()
        {
            Play(@"Content\Sounds\winner.mp3");
        }

        public static void End()
        {
            Play(@"Content\Sounds\end.mp3");
        }
    }
}