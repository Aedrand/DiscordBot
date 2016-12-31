using Discord;
using Discord.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace DiscordBot
{
    class BotClass
    {
        DiscordClient client;
        CommandService commands;
        IList<Builder> builders;

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

            commands = client.GetService<CommandService>();

            builders = new List<Builder>();

            setupAddCommands();
            setupRegister();

            commands.CreateCommand("active")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage(listToString());
                });

            commands.CreateCommand("play").Do(async (e) =>
            {
                await e.Channel.SendMessage("lmao");
                
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

        private void setupAddCommands()
        {
            commands.CreateGroup("add", adCom =>
           {
               adCom.CreateCommand("build")
               .Parameter("Buildname", ParameterType.Required)
               .Description("Adds a new build.")
               .Do(async (e) =>
               {
                   if (userRegistered(e.User.Name) == true)
                   {
                       findInList(e.User.Name).setBuild(new Build(e.GetArg("Buildname")));
                       await e.Channel.SendMessage("```Created new build for " + e.User.Name +
                           " called " + e.GetArg("Buildname") + ".```");
                   }
                   else
                   {
                       await e.Channel.SendMessage("```Could not create new build. User is not registered.```");
                   }
               });

               adCom.CreateCommand("description")
              .Parameter("desc", ParameterType.Required)
              .Description("Adds a description of the user's build.")
              .Do(async (e) =>
              {
                  if (userRegistered(e.User.Name) == true)
                  {
                      if (findInList(e.User.Name).getBuild() != null)
                      {
                          findInList(e.User.Name).getBuild().setDescription(e.GetArg("desc"));
                          await e.Channel.SendMessage("```Description set for " + e.User.Name + ".```");
                      }
                      else
                      {
                          await e.Channel.SendMessage("```" + e.User.Name + " does not have a registered build.```");
                      }
                  }
                  else
                  {
                      await e.Channel.SendMessage("```" + e.User.Name + " has not been registered.```");
                  }
              });

               adCom.CreateCommand("cpu")
               .Parameter("cpuName", ParameterType.Required)
               .Description("Adds a cpu to the user's build.")
               .Do(async (e) =>
               {
                   if (userRegistered(e.User.Name) == true)
                   {
                       if (findInList(e.User.Name).getBuild() != null)
                       {
                           findInList(e.User.Name).getBuild().setCentralPU(e.GetArg("cpuName"));
                           await e.Channel.SendMessage("```" + e.User.Name + " has added " + e.GetArg("cpuName") +
                               " as their CPU.```");
                       }
                       else
                       {
                           await e.Channel.SendMessage("```" + e.User.Name + " does not have a registered build.```");
                       }
                   }
                   else
                   {
                       await e.Channel.SendMessage("```" + e.User.Name + " has not been registered.```");
                   }
               });

               adCom.CreateCommand("motherboard")
              .Parameter("mbName", ParameterType.Required)
              .Description("Adds a motherboard to the user's build.")
              .Do(async (e) =>
              {
                  if (userRegistered(e.User.Name) == true)
                  {
                      if (findInList(e.User.Name).getBuild() != null)
                      {
                          findInList(e.User.Name).getBuild().setMoBoard(e.GetArg("mbName"));
                          await e.Channel.SendMessage("```" + e.User.Name + " has added " + e.GetArg("mbName") +
                              " as their motherboard.```");
                      }
                      else
                      {
                          await e.Channel.SendMessage("```" + e.User.Name + " does not have a registered build.```");
                      }
                  }
                  else
                  {
                      await e.Channel.SendMessage("```" + e.User.Name + " has not been registered.```");
                  }
              });

               adCom.CreateCommand("gpu")
              .Parameter("gpuName", ParameterType.Required)
              .Description("Adds a gpu to the user's build.")
              .Do(async (e) =>
              {
                  if (userRegistered(e.User.Name) == true)
                  {
                      if (findInList(e.User.Name).getBuild() != null)
                      {
                          findInList(e.User.Name).getBuild().setGraphicPU(e.GetArg("gpuName"));
                          await e.Channel.SendMessage("```" + e.User.Name + " has added " + e.GetArg("gpuName") +
                              " as their GPU.```");
                      }
                      else
                      {
                          await e.Channel.SendMessage("```" + e.User.Name + " does not have a registered build.```");
                      }
                  }
                  else
                  {
                      await e.Channel.SendMessage("```" + e.User.Name + " has not been registered.```");
                  }
              });

               adCom.CreateCommand("powersupply")
              .Parameter("suppName", ParameterType.Required)
              .Description("Adds a power supply to the user's build.")
              .Do(async (e) =>
              {
                  if (userRegistered(e.User.Name) == true)
                  {
                      if (findInList(e.User.Name).getBuild() != null)
                      {
                          findInList(e.User.Name).getBuild().setPowerSupp(e.GetArg("suppName"));
                          await e.Channel.SendMessage("```" + e.User.Name + " has added " + e.GetArg("suppName") +
                              " as their power supply.```");
                      }
                      else
                      {
                          await e.Channel.SendMessage("```" + e.User.Name + " does not have a registered build.```");
                      }
                  }
                  else
                  {
                      await e.Channel.SendMessage("```" + e.User.Name + " has not been registered.```");
                  }
              });
           });

            commands.CreateCommand("showbuild")
                .Description("Shows the build of the specified user.")
                .Parameter("username")
                .Do(async (e) =>
                {
                    Build bu = findInList(e.GetArg("username")).getBuild();
                    if (userRegistered(e.User.Name) == true)
                    {
                        if (findInList(e.User.Name).getBuild() != null)
                        {
                            await e.Channel.SendMessage("```" + e.GetArg("username") + "'s Build:" + Environment.NewLine
                                + bu.getBuildName() + Environment.NewLine + "CPU: " + bu.getCentralPU() + Environment.NewLine
                                + "Motherboard: " + bu.getMoBoard() + Environment.NewLine + "GPU: " + bu.getGraphicPU() + Environment.NewLine
                                + "Power Supply: " + bu.getPowerSupp() + Environment.NewLine + bu.getDescription()
                                + "```");
                        }
                        else
                        {
                            await e.Channel.SendMessage("```" + e.User.Name + " does not have a registered build.```");
                        }
                    }
                    else
                    {
                        await e.Channel.SendMessage("```" + e.User.Name + " has not been registered.```");
                    }
                });
        }

        private void setupRegister()
        {
            commands.CreateCommand("register")
               .Description("Adds a builder to the list.")
               .Do(async (e) =>
               {
                   Debug.WriteLine(e.User.Name);
                   if (builders == null || userRegistered(e.User.Name) == false)
                   {
                       builders.Add(new Builder(e.User.Name));
                       await e.Channel.SendMessage("```" + e.User.Name + " has been registered.```");
                   }
                   else
                   {
                       await e.Channel.SendMessage("```User has already been registered.```");
                   }
               });
        }

        private String listToString()
        {
            String full = "";
            for (int i = 0; i < builders.Count; i++)
            {
                full += builders[i].getName() + " ";
            }
            Debug.WriteLine(full);
            return full;
        }

        private Boolean userRegistered(String name)
        {
            for (int i = 0; i < builders.Count; i++)
            {
                if (builders[i].getName().Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

        private Builder findInList(String name)
        {
            for (int i = 0; i < builders.Count; i++)
            {
                if (builders[i].getName().Equals(name))
                {
                    return builders[i];
                }
            }
            return null;
        }
    }
}
