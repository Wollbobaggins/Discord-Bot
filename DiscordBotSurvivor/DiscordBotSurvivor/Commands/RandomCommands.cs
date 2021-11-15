using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;

namespace DiscordBotSurvivor.Commands
{
    public class RandomCommands : BaseCommandModule
    {
        [Command("landon")]
        public async Task Landon(CommandContext context)
        {
            // send a message that is specific to landon, this data could be populated when the bot messages new players

            await context.Channel.SendMessageAsync("x").ConfigureAwait(false);
        }
    }
}
