using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PinSharp.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            System.Console.CancelKeyPress += (s, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            MainAsync(args, cts.Token).Wait();
        }

        static async Task MainAsync(string[] args, CancellationToken token)
        {
            var client = new PinterestClient("AWLN14hJ6-5LSnWqWNkQ8pf5BwK0FBecYJArhbhCpEWkGoA_MwAAAAA");

            var board = await client.Boards.GetBoardAsync("rice_up", "tableware");
            var pins = await client.Boards.GetPinsAsync("rice_up", "tableware");

            var userInfo = await client.Me.GetUserAsync();
            var userPins = await client.Me.GetPinsAsync();

            var user = await client.Users.GetUserAsync("rice_up");

            var boards = await client.Me.GetBoardsAsync();

            var pin = await client.Pins.GetPinAsync("332562753713076738");
            var fields = new List<string> {"url", "creator(username)"};
            var customPin = await client.Pins.GetPinAsync("332562753713076738", fields);

            Console.WriteLine(board.Name);

            Console.ReadKey();
        }
    }
}
