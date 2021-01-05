using System.IO;

namespace ZTP.PlayerClassess
{
    public static class PlayerFactory
    {
        public static Player LoadPlayer()
        {
            if (File.Exists("GameData.xml"))
            {
                // TODO: load and parse xml data to user data
                int DataHitPoints = 2;
                int DataExperiencePoints = 5;
                int DataGold = 100;

                return new Player(DataHitPoints, DataExperiencePoints, DataGold);
            }
            else
            {
                return new Player(10000, 0, 0);
            }
        }
    }
}
