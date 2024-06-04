using StarterKit.Database;
using StarterKit.Utils;
using VampireCommandFramework;

namespace StarterKit.Commands
{
    internal class KitCommands
    {
        [Command("kit", description: "Give new vampire start kit.", adminOnly: false)]
        public static void KitCommand(ChatCommandContext ctx, string KitName = "StartKit")
        {
            if (DB.EnabledKitCommand)
            {
                var PlatformId = ctx.User.PlatformId;
                if (DB.StartKits.TryGetValue(KitName.ToLower(), out var Kit))
                {
                    if (DB.UsedKits.TryGetValue(PlatformId, out var Used))
                    {
                        if (!Used)
                        {
                            Helper.GiveStartKit(ctx, Kit);
                            DB.UsedKits[PlatformId] = true;
                            ctx.Reply($"Enjoy your <color=#ffffffff>free gear pack</color> and let the battle begin!");
                        }
                        else
                        {
                            ctx.Reply($"You have already used the starter kit.");
                        }
                    }
                }
                else
                {
                    ctx.Reply($"Kit with name [{KitName}] not found.");
                }
            }
            else
            {
                ctx.Reply($"Command disabled by admins.");
            }
        }
    }
}
