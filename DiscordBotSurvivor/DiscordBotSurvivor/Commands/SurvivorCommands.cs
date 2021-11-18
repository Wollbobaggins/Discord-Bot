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
    public class SurvivorCommands : BaseCommandModule
    {
        [Command("r")]
        public async Task Response(CommandContext context, string prompt)
        {
            await context.Channel.SendMessageAsync("x" + prompt).ConfigureAwait(false);
        }
    }
}
