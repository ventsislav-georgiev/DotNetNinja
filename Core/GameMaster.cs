using DotNetNinja.Core.Map;
using System.Windows.Forms;

namespace DotNetNinja.Core
{
    internal static class GameMaster
    {
        public static Point SaveSpot = new Point(2, 8);

        public static string Teleport(KeyEventArgs e)
        {
            LevelType? gameLevel = null;
            switch (e.KeyCode)
            {
                case Keys.Q:
                    gameLevel = LevelType.Start;
                    break;

                case Keys.W:
                    gameLevel = LevelType.Level1;
                    break;

                case Keys.E:
                    gameLevel = LevelType.Level2;
                    break;

                case Keys.R:
                    gameLevel = LevelType.Level3;
                    break;

                case Keys.T:
                    gameLevel = LevelType.Level4;
                    break;

                case Keys.Y:
                    gameLevel = LevelType.Level5;
                    break;

                case Keys.U:
                    gameLevel = LevelType.Level6;
                    break;

                case Keys.I:
                    gameLevel = LevelType.Level7;
                    break;

                case Keys.O:
                    gameLevel = LevelType.Level8;
                    break;

                case Keys.P:
                    gameLevel = LevelType.Level9;
                    break;

                case Keys.A:
                    gameLevel = LevelType.Level10;
                    break;

                case Keys.S:
                    gameLevel = LevelType.Level11;
                    break;

                case Keys.D:
                    gameLevel = LevelType.Level12;
                    break;

                case Keys.F:
                    gameLevel = LevelType.Level13;
                    break;

                case Keys.G:
                    gameLevel = LevelType.Level14;
                    break;

                case Keys.H:
                    gameLevel = LevelType.Level15;
                    break;

                case Keys.J:
                    gameLevel = LevelType.Level16;
                    break;
            }

            if (gameLevel.HasValue && e.Modifiers.HasFlag(Keys.Shift))
            {
                Sounds.PlayBackgroundSound(gameLevel.Value);
                return gameLevel.ToString().ToLower();
            }
            return null;
        }
    }
}