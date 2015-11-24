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

            MainAsync(args, cts.Token).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args, CancellationToken token)
        {
            var client = new PinterestClient("AZ_ITS6Q0fFQbXJ90JGtSDXNMV-tFBkfLftbK6JCpEWkGoA_MwAAAAA");

            var board = await client.Boards.GetBoardAsync("rice_up/tableware");
            var pins = await client.Boards.GetPinsAsync("rice_up/tableware");
            var dynamicBoardPins = await client.Boards.GetPinsAsync<dynamic>("rice_up/tableware", new[] { "creator", "board" });

            var userInfo = await client.Me.GetUserAsync();
            var userPins = await client.Me.GetPinsAsync();
            var userBoards = await client.Me.GetBoardsAsync();

            var user = await client.Users.GetUserAsync("rice_up");
            var dynamicUser = await client.Users.GetUserAsync("rice_up", new[] { "username" });

            var pin = await client.Pins.GetPinAsync("332562753713076738");
            var dynamicPin = await client.Pins.GetPinAsync("332562753713076738", new[] { "url", "creator(username)" });

            //var newPin = await client.Pins.CreatePinAsync("krusenen/public-mc", "http://i.imgur.com/B1uwtac.jpg", "qwerqwer");

            //await client.Me.FollowBoardAsync("rice_up/tableware");
            //await client.Me.UnfollowBoardAsync("rice_up/tableware");
            //await client.Me.FollowUserAsync("rice_up");
            //await client.Me.UnfollowUserAsync("rice_up");

            //var pins = await client.Boards.GetPinsAsync("rice_up/tableware", limit:6);
            //var pins2 = await client.Boards.GetPinsAsync("rice_up/tableware", pins.NextPageCursor, 5);
            //var dynamicPins = await client.Boards.GetPinsAsync<dynamic>("rice_up/tableware", new [] {"url", "creator(username)"});


            Console.ReadKey();
        }
    }
}
