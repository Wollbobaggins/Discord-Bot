using System;

namespace DiscordBotSurvivor
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Bot bot = new Bot();

            bot.RunAsync().GetAwaiter().GetResult();
        }
    }
}
