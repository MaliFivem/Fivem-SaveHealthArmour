using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class Giocatore
    {
        public int _id { get; set; }
        public string steamId { get; set; }
        public string steamName { get; set; }
        public string license { get; set; }
        public int[] vita { get; set; } = new int[2] { 100, 0 };

        public int[] ChiediVita()
        {
            return new int[] { vita[0], vita[1] };
        }
    }
}
