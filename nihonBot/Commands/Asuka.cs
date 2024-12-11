using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace NihonBot.Commands
{  
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
            var messageReference = new Discord.MessageReference(Context.Message.Id);

            if (string.IsNullOrEmpty(gifPath))
            {
                await ReplyAsync("すみません、GIFを見つけられませんでした。",
                                 messageReference: messageReference);
                return;
            }

            var today = DateTime.Today;
            var daysUntilThursday = ((int)DayOfWeek.Thursday - (int)today.DayOfWeek + 7) % 7;

            if (daysUntilThursday == 0)
            {
                await Context.Channel.SendFileAsync(gifPath,
                                                    "今日は木曜日です！アスカの日です！",
                                                    isSpoiler: false,
                                                    embed: null,
                                                    options: null,
                                                    isTTS: false,
                                                    allowedMentions: null,
                                                    messageReference: messageReference);
            }
            else
            {
                await Context.Channel.SendFileAsync(gifPath,
                                                    $"木曜日まであと {daysUntilThursday} 日です。",
                                                    isSpoiler: false,
                                                    embed: null,
                                                    options: null,
                                                    isTTS: false,
                                                    allowedMentions: null,
                                                    messageReference: messageReference);
            }
        }
    }
}  