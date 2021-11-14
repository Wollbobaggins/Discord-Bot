using System;

namespace DiscordBotSurvivor
{
    class MainClass
    {
        /************************************************************/
        #region Functions

        public static void Main(string[] args)
        {
            Bot bot = new Bot();

            bot.RunAsync().GetAwaiter().GetResult();
        }

        #endregion
        /************************************************************/
    }
}
