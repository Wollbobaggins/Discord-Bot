using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DiscordBotSurvivor.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using Newtonsoft.Json;

namespace DiscordBotSurvivor
{
    public class Bot
    {
        /************************************************************/
        #region Properties

        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public static Bot Singleton { get; private set; }
        public bool HasGameStarted { get; set; }
        public bool IsInterviewing { get; set; }

        #endregion
        /************************************************************/
        #region Functions

        public async Task RunAsync()
        {
            Singleton = this;

            /******************************/
            // Set json file
            /******************************/

            string json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            {
                using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                {
                    json = await sr.ReadToEndAsync().ConfigureAwait(false);
                }
            }

            JsonConfig jsonConfig = JsonConvert.DeserializeObject<JsonConfig>(json);

            DiscordConfiguration discordConfig = new DiscordConfiguration
            {
                Token = jsonConfig.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = Microsoft.Extensions.Logging.LogLevel.Debug,
                Intents = DiscordIntents.All
                // UseInternalLogHandler = true // HACK: this param isn't available anymore
            };

            Client = new DiscordClient(discordConfig);

            /******************************/
            // Set commands and interactivity
            /******************************/

            Client.Ready += OnClientReady;

            //Client.Intents.AddIntent(DiscordIntents.All);

            Client.GuildMemberAdded += OnGuildMemberAdded;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(1)
            });

            RegisterCommands(jsonConfig);

            /******************************/
            // Finish Initialization
            /******************************/

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }

        private void RegisterCommands(JsonConfig jsonConfig)
        {
            CommandsNextConfiguration commandConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { jsonConfig.Prefix },
                //EnableDms = false,
                EnableMentionPrefix = true,
                DmHelp = true
            };

            Commands = Client.UseCommandsNext(commandConfig);

            //Commands.RegisterCommands<DebugCommands>();
            Commands.RegisterCommands<RandomCommands>();
            Commands.RegisterCommands<JeffCommands>();
            Commands.RegisterCommands<SurvivorCommands>();
        }

        #region Event Handler Functions

        // HACK: added DiscordClient param to make this run
        private Task OnClientReady(DiscordClient c, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }

        private async Task OnGuildMemberAdded(DiscordClient c, GuildMemberAddEventArgs args)
        {
            string message = $"Welcome, {args.Member.DisplayName}. ";
            DiscordRole role;

            if (HasGameStarted)
            {
                role = args.Guild.GetRole(909675969344831529);
                message += "The game has already started, you are assigned the role Loser :cry:";
            }
            else
            {
                role = args.Guild.GetRole(909331568881983538);
                message += "The game has not yet started, you are assigned the role Survivor :military_helmet: " +
                    ":man_swimming: :helicopter: :man_lifting_weights: :man_rowing_boat:";
            }
            await args.Member.GrantRoleAsync(role).ConfigureAwait(false);
            await args.Member.SendMessageAsync(message).ConfigureAwait(false);
            await Task.CompletedTask;
        }

        #endregion

        #endregion
        /************************************************************/
    }
}
