using Discord.Commands;
using System.Threading.Tasks;

namespace NihonBot.Commands
{
    public class Pon : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task PonAsync()
        {
            await ReplyAsync("ポン！");
        }
    }

    public class Pin : ModuleBase<SocketCommandContext>
    {
        [Command("pong")]
        public async Task PonAsync()
        {
            await ReplyAsync("ピン！");
        }
    }

    public class Asuka : ModuleBase<SocketCommandContext>
    {
        private readonly GifRepository _gifRepository;

        public Asuka(GifRepository gifRepository)
        {
            _gifRepository = gifRepository;
        }
        
        [Command("asuka")]
        public async Task GifAsync()
        {
            var gifPath = _gifRepository.GetRandomGif();

            if (string.IsNullOrEmpty(gifPath))
            {
                await ReplyAsync("すみません、GIFを見つけられませんでした。");
                return;
            }

            var today = DateTime.Today;
            var daysUntilThursday = ((int)DayOfWeek.Thursday - (int)today.DayOfWeek + 7) % 7;

            if (daysUntilThursday == 0)
            {
                await Context.Channel.SendFileAsync(gifPath, "今日は木曜日です！アスカの日です！");
            }
            else
            {
                await Context.Channel.SendFileAsync(gifPath, $"木曜日まであと {daysUntilThursday} 日です。");
            }
        }
    }
} 