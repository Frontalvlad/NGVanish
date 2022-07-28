using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Localization;
using TerrariaApi.Server;
using TShockAPI;

namespace NGVanish
{
    [ApiVersion(2, 1)]
    public class NGVanish : TerrariaPlugin
    {
        public override string Name { get { return "NGVanish"; } }
        public override string Author { get { return "Frontalvlad"; } }
        public override string Description { get { return "Plugin specifically for NGVille server. Includes vanish for adminstrators and moderators."; } }
        public override Version Version { get { return Assembly.GetExecutingAssembly().GetName().Version; } }

        public NGVanish(Main game)
          : base(game)
        {
        }

        public override void Initialize()
        {
            Commands.ChatCommands.Add(new Command("ngvanish", new CommandDelegate(this.ToggleVanish), new string[2]
            {
                "ngvanish",
                "vanish"
            }));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
            base.Dispose(disposing);
        }

        private void ToggleVanish(CommandArgs args)
        {
            args.TPlayer.active = !args.TPlayer.active;
            NetMessage.SendData(14, -1, args.Player.Index, null, args.Player.Index, args.TPlayer.active.GetHashCode());
            if (args.TPlayer.active)
            {
                NetMessage.SendData(4, -1, args.Player.Index, null, args.Player.Index);
                NetMessage.SendData(13, -1, args.Player.Index, null, args.Player.Index);
            }
            NetMessage.SendData(119, -1, -1, Terraria.Localization.NetworkText.FromLiteral($"Vanish {(args.TPlayer.active ? "dis" : "en")}abled."), (int)new Color(227, 105, 63).PackedValue, args.Player.X + 8, args.Player.Y + 32);
            args.Player.SendSuccessMessage($"[i:547] [C/ffffff:Режим Vanish был] [C/e3693f:{(args.TPlayer.active ? "вык" : "вк")}лючен]");
        }
    }
}