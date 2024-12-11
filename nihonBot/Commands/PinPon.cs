using Discord.Commands;
using System.Threading.Tasks;

namespace NihonBot.Commands
{
    public class Pon : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PonAsync()
        {
            var messageReference = new Discord.MessageReference(Context.Message.Id);
            await ReplyAsync("ポン！",
                             messageReference: messageReference);
        }
    }

    public class Pin : ModuleBase<SocketCommandContext>
    {
        [Command("pong")]
        public async Task PonAsync()
        {
            var messageReference = new Discord.MessageReference(Context.Message.Id);
            await ReplyAsync("ピン！",
                             messageReference: messageReference);
        }
    }
}
 