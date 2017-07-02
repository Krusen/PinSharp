using System;
using Newtonsoft.Json;

namespace PinSharp.Models.Counts
{
    public class Counts : IBoardCounts, IPinCounts, IUserCounts
    {
        public int Boards { get; set; }
        public int Collaborators { get; set; }
        public int Comments { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public int Pins { get; set; }
        public int Saves { get; set; }

        [Obsolete("Use 'Saves' instead. This property will be removed in a future version")]
        public int Repins => Saves;
    }
}
