using System;
using System.Collections.Generic;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using static CitizenFX.Core.Native.API;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core.Native;
using Shared;

namespace Client
{
    public class Main:BaseScript
    {
        public Main()
        {
            EventHandlers.Add("playerSpawned", new Action<Vector3>(OnPlayerSpawned));
            EventHandlers.Add("rispondi", new Action<int, int>(Rispondi));
            Tick += Salvataggio;
        }

        private void OnPlayerSpawned([FromSource] Vector3 pos)
        {
            TriggerServerEvent("chiedi");
        }

        public async Task Salvataggio()
        {
            await Delay(12000);
            Screen.LoadingPrompt.Show("Salvataggio vita", LoadingSpinnerType.SocialClubSaving);
            await Delay(3000);
            Screen.LoadingPrompt.Hide();

            TriggerServerEvent("salva", Game.PlayerPed.Health, Game.PlayerPed.Armor);
        }

        private void Rispondi(int salute, int armatura)
        {
            Game.PlayerPed.Health = salute;
            Game.PlayerPed.Armor = armatura;
        }
    }
}
