using System;
using System.Collections.Generic;
using System.Linq;

namespace Week_2
{
    class Program
    {
        static int _playerCount = 1000000;
        static List<Player> _players;
        static List<Player> _morePlayers;

        // Variables that control the loop
        static int input = -1;
        static int input2 = -1;
        static bool validInput = false;
        static bool exit = false;
        static Random rand;
        static void Main(string[] args)
        {
            Console.WriteLine("Week 2 assignments....");

            // Necessary for the tests
            rand = new Random();
            _players = new List<Player>();
            _morePlayers = new List<Player>();
            _morePlayers = CreatePlayersWithParams(20, _morePlayers);

            // Setting up the 20 player list for tests (Random items etc.)
            foreach (Player p in _morePlayers)
            {
                p.Score = rand.Next(0, 10000000);
                p.Items = new List<Item>();
                for (int i = 0; i < rand.Next(1, 10); i++)
                {
                    p.Items.Add(new Item()
                    {
                        Id = Guid.NewGuid(),
                        Level = rand.Next(1, 10)
                    });
                }
            }

            // The loop
            while (!exit)
            {
                validInput = false;
                input = input2 = -1;

                /// <see cref="CheckGuids"/> https://github.com/vsillan/game-server-programming-course/blob/master/assignments/assignment_2.md#1-guid
                Console.WriteLine("\nOptions:\n1. Create million players and check that guids are unique");

                /// <see cref="GetItems"/>
                /// <see cref="GetItemsWithLinq"/> https://github.com/vsillan/game-server-programming-course/blob/master/assignments/assignment_2.md#3-linq
                Console.WriteLine("2. GetItems() and GetItemsWithLinq() from another list of 20 players");

                /// <see cref="ExtendingItemClass.GetHighestLevelItem"/> https://github.com/vsillan/game-server-programming-course/blob/master/assignments/assignment_2.md#2-extension-method
                /// <see cref="FirstItem"/>
                /// <see cref="FirstItemWithLinq"/> https://github.com/vsillan/game-server-programming-course/blob/master/assignments/assignment_2.md#4-linq-2
                Console.WriteLine("3. GetHighestLevelItem(), FirstItem() and FirstItemWithLinq() from the same list as Option 2... " +
                    "It's preferred to first run 2 to use the same index to confirm....");

                /// <see cref="ProcessEachItem"/> 
                /// https://github.com/vsillan/game-server-programming-course/blob/master/assignments/assignment_2.md#5-delegates
                /// https://github.com/vsillan/game-server-programming-course/blob/master/assignments/assignment_2.md#6-lambda
                Console.WriteLine("4. ProcessEachItem and Process each item with lambda.. goes through list of 20 players and stops prompts to continue after each");

                /// <see cref="HiscoreTest"/> 
                /// <see cref="Game.GetTop10Players()"/>
                /// https://github.com/vsillan/game-server-programming-course/blob/master/assignments/assignment_2.md#7-generics
                Console.WriteLine("5. Generic Top10 function test");

                Console.WriteLine("6. Exit....");

                validInput = Int32.TryParse(Console.ReadLine(), out input);
                if (!validInput)
                {
                    Console.WriteLine("Please use the integer options shown prior, invalid input");
                }
                switch (input)
                {
                    case 1:
                        CreatePlayers();
                        Console.WriteLine("\nPlayer count: " + _players.Count);
                        Console.WriteLine("Every player is unique?: " + CheckGuids());
                        break;
                    case 2:
                        do
                        {
                            validInput = ReadInputValidateIndex();
                        } while (!validInput);

                        Item[] playerItems = GetItems(_morePlayers[input2]);
                        Item[] playerItemsLinq = GetItemsWithLinq(_morePlayers[input2]);

                        Console.WriteLine("\nPlayer from index  " + input2 + " items:");
                        for (int i = 0; i < playerItems.Length; i++)
                        {
                            Console.WriteLine(playerItems[i].Id + " | " + playerItems[i].Level);
                        }

                        Console.WriteLine("\nPlayer from index  " + input2 + " items with linq:");
                        for (int i = 0; i < playerItemsLinq.Length; i++)
                        {
                            Console.WriteLine(playerItemsLinq[i].Id + " | " + playerItemsLinq[i].Level);
                        }
                        break;
                    case 3:
                        do
                        {
                            validInput = ReadInputValidateIndex();
                        } while (!validInput);
                        Console.WriteLine("Player from index: " + input2 + " Highest level item:");
                        PrintItem(_morePlayers[input2].GetHighestLevelItem());
                        Console.WriteLine("Player from index: " + input2 + " First item:");
                        PrintItem(FirstItem(_morePlayers[input2]));
                        Console.WriteLine("Player from index: " + input2 + " First item with Linq:");
                        PrintItem(FirstItemWithLinq(_morePlayers[input2]));
                        break;
                    case 4:
                        foreach (Player p in _morePlayers)
                        {
                            Console.WriteLine("Player: " + p.Id + " items: ");
                            Action<Item> printItem = delegate (Item i)
                            {
                                PrintItem(i);
                            };
                            ProcessEachItem(p, printItem);

                            Console.WriteLine("\n" + p.Id + " items lambda: ");
                            Action<Item> print = (Item i) =>
                            {
                                Console.WriteLine(i.Id + " | " + i.Level);
                            };
                            ProcessEachItem(p, print);
                            Console.WriteLine("\n\nAny key to continue.....\n\n");
                            Console.ReadLine();
                        }
                        break;
                    case 5:
                        HiscoreTest();
                        break;
                    case 6:
                        exit = true;
                        break;

                }
            }
        }
        public static void HiscoreTest()
        {
            List<PlayerForAnotherGame> _anotherGamePlayers = new List<PlayerForAnotherGame>() {
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                },
                new PlayerForAnotherGame() {
                    Score = rand.Next(1, 1000)
                }
            };
            Game<Player> jokupeli = new Game<Player>(_morePlayers);
            Game<PlayerForAnotherGame> jokutoinenpeli = new Game<PlayerForAnotherGame>(_anotherGamePlayers);
            Player[] top10 = jokupeli.GetTop10Players();
            PlayerForAnotherGame[] top10_forAnotherGame = jokutoinenpeli.GetTop10Players();

            Console.WriteLine(jokupeli.GetType().ToString() + " top 10: ");
            for (int i = 0; i < top10.Length; i++)
            {
                Console.WriteLine((i + 1) + " : " + top10[i].Score.ToString());
            }
            Console.WriteLine(jokutoinenpeli.GetType().ToString() + " top 10: ");
            for (int i = 0; i < top10_forAnotherGame.Length; i++)
            {
                Console.WriteLine((i + 1) + " : " + top10_forAnotherGame[i].Score.ToString());
            }
        }
        public static bool ReadInputValidateIndex()
        {
            Console.WriteLine("\nPlease select index.... (0-19): ");
            return Int32.TryParse(Console.ReadLine(), out input2) && (input2 >= 0 ? input2 < 20 ? true : false : false);
        }
        public static void CreatePlayers()
        {
            for (int i = 0; i < _playerCount; i++)
            {
                Guid newId = Guid.NewGuid();
                _players.Add(new Player()
                {
                    Id = newId
                });
            }
        }

        public static List<Player> CreatePlayersWithParams(int count, List<Player> playerList)
        {
            for (int i = 0; i < count; i++)
            {
                Guid newId = Guid.NewGuid();
                playerList.Add(new Player()
                {
                    Id = newId
                });
            }
            return playerList;
        }

        public static bool CheckGuids()
        {
            return _players.Distinct().Count() == _players.Count();
        }

        public static Item[] GetItems(Player player)
        {
            int count = _morePlayers[_morePlayers.IndexOf(player)].Items.Count;

            Item[] itemArray = new Item[count];
            for (int i = 0; i < count; i++)
            {
                itemArray[i] = player.Items[i];
            }
            return itemArray;
        }

        public static Item[] GetItemsWithLinq(Player player) => player.Items.ToArray();

        public static Item FirstItem(Player player)
        {
            return player.Items[0];
        }

        public static Item FirstItemWithLinq(Player player) => player.Items.First();

        public static void ProcessEachItem(Player player, Action<Item> process)
        {
            foreach (Item i in player.Items)
            {
                process(i);
            }
        }

        public static void PrintItem(Item item)
        {
            Console.WriteLine(item.Id.ToString() + " | " + item.Level.ToString());
        }

    }

    public interface IPlayer
    {
        int Score { get; set; }
    }

    public class Player : IPlayer
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public List<Item> Items { get; set; }


    }

    public class Item
    {

        public delegate Item ItemDel();
        public Guid Id { get; set; }
        public int Level { get; set; }
    }

    public static class ExtendingItemClass
    {
        public static Item GetHighestLevelItem(this Player player)
        {
            Item toReturn = player.Items[0];
            foreach (Item i in player.Items)
            {
                if (i.Level > toReturn.Level)
                {
                    toReturn = i;
                }
            }
            return toReturn;
        }
    }

    public class Game<T> where T : IPlayer
    {
        private List<T> _players;

        public Game(List<T> players)
        {
            _players = players;
        }

        /*
            Utilizing C# native implementation of quicksort and resizing the array to correct size
         */
        public T[] GetTop10Players()
        {
            T[] ret = new T[10];
            ret = _players.OrderByDescending(obj => obj.Score).ToArray();
            Array.Resize(ref ret, 10);
            return ret;
        }
    }

    public class PlayerForAnotherGame : IPlayer
    {
        public int Score { get; set; }
    }
}