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
    public class JeffCommands : BaseCommandModule
    {
        [Command("data")]
        public async Task Data(CommandContext context)
        {
            // logs all data to discord
            await context.Channel.SendMessageAsync("asda").ConfigureAwait(false);
        }

        [Command("interview")]
        public async Task Interview(CommandContext context, string prompt)
        {
            // for every survivor
                // send them a DM about prompt
                // record the response SOMEHOW??? in a dictionary
                    // if the user sends multi responses, record more? or delete prev response

            // later broadcast the responses in the interview channel (this might require another role, crew?)
            
            await context.Channel.SendMessageAsync("x").ConfigureAwait(false);
        }

        [Command("jury")]
        public async Task Jury(CommandContext context, DiscordRole toVoteRole, DiscordRole toKickRole)
        {
            // creates a poll to kick a player 
            // for every survivor (in toVoteRole)
                // send them a DM asking about which player (in toKickRole) to kick
                // if the user sends multi responses, record last

            // after time period, kick player who was voted out
            // then report voting results to the interview channel

            await context.Channel.SendMessageAsync("x").ConfigureAwait(false);
        }

        [Command("challenge")]
        public async Task Challenge(CommandContext context)
        {
            // selects a challege from the challenge list
            // prints challenge

            await context.Channel.SendMessageAsync("x").ConfigureAwait(false);
        }

        [Command("challengefinal")]
        public async Task ChallengeFinal(CommandContext context)
        {
            // selects a challege from the challenge list for the final players
            // prints challenge

            await context.Channel.SendMessageAsync("x").ConfigureAwait(false);
        }
    }
}
