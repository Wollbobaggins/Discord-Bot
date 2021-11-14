using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Newtonsoft.Json;

namespace DiscordBotSurvivor
{
    public class Bot
    {
        /************************************************************/
        #region Properties

        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        #endregion
        /************************************************************/
        #region Functions

        public async Task RunAsync()
        {
            string json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            {
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                {
                    json = await sr.ReadToEndAsync().ConfigureAwait(false);
                }
            }

            JsonConfig jsonConfig = JsonConvert.DeserializeObject<JsonConfig>(json);

            DiscordConfiguration config = new DiscordConfiguration
            {
                Token = jsonConfig.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug
                // UseInternalLogHandler = true // HACK: this param isn't available anymore
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;

            CommandsNextConfiguration commandConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { jsonConfig.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true
            };

            Commands = Client.UseCommandsNext(commandConfig);

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }

        // HACK: added DiscordClient param to make this run
        private Task OnClientReady(DiscordClient c, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

        #endregion
        /************************************************************/
    }
}
