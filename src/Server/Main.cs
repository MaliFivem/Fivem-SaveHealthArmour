using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using System.Xml;
using Shared;
using LiteDB;
using System.Collections.Generic;
using System.Diagnostics;

namespace Server
{
    public class Main:BaseScript
    {
        public Main()
        {
            EventHandlers["chiedi"] += new Action<Player>(Chiedi);
            EventHandlers["salva"] += new Action<Player, int, int>(Salva);
            EventHandlers.Add("playerConnecting", new Action<Player, string, CallbackDelegate>(OnPlayerConnecting));
        }

        private void OnPlayerConnecting([FromSource] Player player, string playerName, CallbackDelegate kickCallback)
        {
            Giocatore giocatore = new Giocatore()
            {
                steamId = player.Identifiers["steam"],
                steamName = player.Name,
                license = player.Identifiers["license"],
            };
            if (Database.GetData<Giocatore>("steamId", player.Identifiers["steam"]) != null)
            {
                Giocatore giocatore2 = Database.GetData<Giocatore>("steamId", player.Identifiers["steam"]);
                if (player.Name == giocatore2.steamName)
                {
                    return;
                }
                else
                {
                    giocatore2.steamName = player.Name;
                    Database.Upsert(giocatore2);
                }

            }
            else
            {
                if (player.Identifiers["steam"] != null)
                {
                    Database.Upsert(giocatore);
                }
                else
                {
                    kickCallback("Devi avere Steam aperto!");
                    API.CancelEvent();
                }
            }
        }

        private void Salva([FromSource] Player player, int salute, int armatura)
        {
            Giocatore giocatore = Database.GetData<Giocatore>("steamId", player.Identifiers["steam"]);
            giocatore.vita[0] = salute;
            giocatore.vita[1] = armatura;
            Database.Upsert(giocatore);
        }

        private void Chiedi([FromSource] Player player)
        {
            Giocatore giocatore = Database.GetData<Giocatore>("steamId", player.Identifiers["steam"]);

            if (giocatore != null)
            {
                int[] vita = giocatore.ChiediVita();

                int salute = vita[0];
                int armatura = vita[1];

                player.TriggerEvent("rispondi", salute, armatura);
            }
        }
    }
}
