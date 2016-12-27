using Discord;
using Discord.Commands;
using System;

namespace DiscordBot
{
    class BotClass
    {
        DiscordClient client;


        public BotClass()
        {
            

            client = new DiscordClient(input =>
            {
                input.LogLevel = LogSeverity.Info;
                input.LogHandler = Log;
            });

            client.UsingCommands(x =>
            {
                x.PrefixChar = '!';
                x.AllowMentionPrefix = true;
            });

            var commands = client.GetService<CommandService>();

            commands.CreateCommand("test").Do(async (e) =>
            {
                await e.Channel.SendMessage("Test");
            });

            commands.CreateCommand("play").Do(async (e) =>
            {
                await e.Channel.SendMessage("FUCK YOU");
            });

            client.ExecuteAndWait(async () =>
            {
                await client.Connect("MjMwMDcxNTM5OTE4MzA3MzM5.C0HHBA.ATque64ZipWfSAitA2FMMV0f7ao", TokenType.Bot);
            });

        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
