using System;
using System.Reflection;
using DiscordRPC;

namespace Discord_RPC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DiscordRpcClient client = new DiscordRpcClient("932956391180754944");
            Console.Title = "RPC-Server by ©PIN0L33KZ";
            
            Console.WriteLine($"interface for rpc-server by PIN0L33KZ V.{Assembly.GetExecutingAssembly().GetName().Version}");
            Console.WriteLine("-------------------------------------------------");

            while (true)
            {
                Console.ResetColor();

                Console.Write("rpc-server$ ");
                string input = Console.ReadLine();

                switch(input.ToLower())
                {
                    case "start server":
                        GetInfos(client);
                        break;

                    case "stop server":
                        StopClient(client);
                        break;

                    case "reload server":
                        ReloadClient(client);
                        break;

                    case "change imagekey":
                        ChangeImageKey(client);
                        break;

                    case "change gametitle":
                        ChangeGameTitle(client);
                        break;

                    case "change subtitle":
                        ChangeSubtitle(client);
                        break;

                    case "is open":
                        CheckIfClientIsInitialized(client);
                        break;

                    case "is displaying":
                        CheckIfClientIsDisplaying(client);
                        break;

                    case "help":
                        PrintHelp();
                        break;

                    default:
                        Console.WriteLine("> Unknown command. Use help to get the help menu.");
                        break;
                }

                Console.WriteLine("");
            }
        }

        private static void CheckIfClientIsInitialized(DiscordRpcClient client)
        {
            if(client.IsInitialized)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("> true");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("> false");
            }
        }

        private static void CheckIfClientIsDisplaying(DiscordRpcClient client)
        {
            if (client.CurrentPresence != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("> true");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("> false");
            }
        }

        private static void ChangeImageKey(DiscordRpcClient client)
        {
            Console.Write("please enter your imagekey: ");
            string imageKey = Console.ReadLine();


            try
            {
                if (client.IsInitialized && client.CurrentPresence != null)
                {
                    RichPresence newPresence = client.CurrentPresence;
                    newPresence.Assets.LargeImageKey = imageKey;
                    client.ClearPresence();
                    client.SetPresence(newPresence);

                    Console.WriteLine("> updated");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No client initialzed. Try 'start server' first.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error during connection.\n{ex.Message}");
            }
        }

        private static void ChangeGameTitle(DiscordRpcClient client)
        {
            Console.Write("please enter your gametitle: ");
            string details = Console.ReadLine();


            try
            {
                if (client.IsInitialized && client.CurrentPresence != null)
                {
                    RichPresence newPresence = client.CurrentPresence;
                    newPresence.Details = details;
                    client.ClearPresence();
                    client.SetPresence(newPresence);

                    Console.WriteLine("> updated");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No client initialzed. Try 'start server' first.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error during connection.\n{ex.Message}");
            }
        }

        private static void ChangeSubtitle(DiscordRpcClient client)
        {
            Console.Write("please enter your subtilte: ");
            string subtitle = Console.ReadLine();


            try
            {
                if (client.IsInitialized && client.CurrentPresence != null)
                {
                    RichPresence newPresence = client.CurrentPresence;
                    newPresence.State = subtitle;
                    client.ClearPresence();
                    client.SetPresence(newPresence);

                    Console.WriteLine("> updated");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No client initialzed. Try 'start server' first.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error during connection.\n{ex.Message}");
            }
        }

        private static void GetInfos(DiscordRpcClient client)
        {
            Console.Write("please enter your imagekey: ");
            string imageKey = Console.ReadLine();

            Console.Write("please enter your gametitle: ");
            string details = Console.ReadLine();

            Console.Write("please enter your subtilte: ");
            string state = Console.ReadLine();

            StartClient(client, imageKey, state, details);
        }

        private static void StartClient(DiscordRpcClient client, string imageKey, string state, string details)
        {
            try
            {
                if(client.IsInitialized == false)
                    client.Initialize();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error during connection.\n{ex.Message}");
            }

            client.SetPresence(new RichPresence()
            {
                State = state,
                Details = details,
                Assets = new Assets()
                {
                    LargeImageKey = imageKey,
                    LargeImageText = "Data provided by PIN0L33KZ's rpc-server."
                },
            });

            Console.WriteLine("> started");
        }

        private static void StopClient(DiscordRpcClient client)
        {
            if(client.IsInitialized)
            {
                client.ClearPresence();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("> stopped");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("> no client initialzed, try 'start server'");
            }
        }

        private static void ReloadClient(DiscordRpcClient client)
        {
            if (client.IsInitialized && client.CurrentPresence != null)
            {
                var presence = client.CurrentPresence;

                client.ClearPresence();
                client.SetPresence(presence);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("> reloaded");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("> no client running, try 'start server'");
            }
        }

        private static void PrintHelp()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("> showing help");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("start server - starts the rpc server.");
            Console.WriteLine("stop server - stops the rpc server.");
            Console.WriteLine("change imagekey - edit the current imagekey.");
            Console.WriteLine("change gametitle - edit the current gametitle.");
            Console.WriteLine("change subtitle - edit the current subtitle.");
            Console.WriteLine("is open - check if any client was open.");
            Console.WriteLine("is displaying - check if any rpc is active.");
            Console.WriteLine("----------------------------------------------");
        }
    }
}
