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
        [Command("toggle")]
        public async Task Toggle(CommandContext context)
        {
            Bot.Singleton.HasGameStarted = !Bot.Singleton.HasGameStarted;
            string message = $"HasGameStart: { Bot.Singleton.HasGameStarted}";
            await context.Channel.SendMessageAsync(message).ConfigureAwait(false);
        }

        [Command("interviewstart")]
        public async Task StartInterview(CommandContext context, string prompt)
        {
            Bot.Singleton.IsInterviewing = true;
            DiscordRole role = context.Guild.GetRole(909331568881983538);
            string helpMessage = "To respond to the interview message, reply with !r \"YOUR MESSAGE \"\n\n";
            helpMessage += "for example: :point_right:!r \"my name is jeff\":point_left:";

            foreach (var member in context.Guild.Members)
            {
                if (member.Value.Roles.Contains(role))
                {
                    await member.Value.SendMessageAsync(prompt).ConfigureAwait(false);

                    await member.Value.SendMessageAsync(helpMessage).ConfigureAwait(false);
                }
            }
        }

        [Command("interviewend")]
        public async Task EndInterview(CommandContext context)
        {
            // TODO: alert user of their selected response or that they did not send message

            Bot.Singleton.IsInterviewing = false;
            await Task.CompletedTask;
        }

        [Command("jury")]
        public async Task Jury(CommandContext context, DiscordRole toVoteRole, DiscordRole toKickRole)
        {
            // creates a poll to kick a player 
            // for every survivor (in toVoteRole)
                // send them a DM asking about which player (in toKickRole) to kick
                // if the user sends multi responses, record last

            // after time period, kick player who was voted out and give him loser role
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
