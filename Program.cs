using System;
using System.Collections.Generic;
using System.Threading;

namespace TicTacToe_cours
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> game = new Dictionary<string, string>()
            {
                {"A1"," "},
                {"A2"," "},
                {"A3"," "},
                {"B1"," "},
                {"B2"," "},
                {"B3"," "},
                {"C1"," "},
                {"C2"," "},
                {"C3"," "}
            };
            List<string> locations = new List<string>(game.Keys); // la commande "List<string> liste = dict.Keys.ToList();" ne fonctionne pas

            Console.WriteLine("Jeu du Tic Tac Toe");
            Display(game);

            string choice;

            Random random = new Random(); // on prépare le choix aléatoire pour l'IA

            while (locations.Count != 0) // la fonction "liste.Any()" ne fonctionne pas
            {
                // Le joueur joue
                while (true)
                {
                    Console.WriteLine("[Joueur] Choisir un emplacement : ");
                    choice = Console.ReadLine();
                    if (locations.Contains(choice))
                    {
                        break;
                    }
                }
                game[choice] = "X";
                locations.Remove(choice);
                Display(game);

                if (EndGame(game, choice))
                {
                    Console.WriteLine("[Joueur] a gagner !");
                    break;
                }

                // L'IA joue
                Console.WriteLine("[IA] Je réfléchis...");
                System.Threading.Thread.Sleep(1000);

                int index = random.Next(locations.Count);
                Console.WriteLine("[IA] Je joue en {0}", locations[index]);

                game[locations[index]] = "O";
                locations.Remove(locations[index]);
                Display(game);

                if (EndGame(game, choice))
                {
                    Console.WriteLine("[IA] a gagner !");
                    break;
                }
            }

            Console.WriteLine("Partie finie.");
        }

        static void Display(Dictionary<string, string> game)
        {
            List<string> lines = new List<string>
            {
                "A",
                "B",
                "C"
            };

            List<string> columns = new List<string>
            {
                "1",
                "2",
                "3"
            };

            string sep = "   +---+---+---+"; // la commande "Enumerable.Repeat("text", n);" ne fonctionne pas

            string coord;

            Console.WriteLine("     1   2   3 ");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(sep);
                Console.Write(" {0} ", lines[i]);

                for (int j = 0; j < 3; j++)
                {
                    coord = lines[i] + columns[j];
                    Console.Write("| {0} ", game[coord]);
                }
                Console.Write("|\n");
            }
            Console.WriteLine(sep);

        }
        static bool Count3Marks(Dictionary<string, string> game, string symbole, List<string> l)
        {
            int marksCount = 0;

            foreach (string coord in l)
            {
                if (game[coord] == symbole)
                {
                    marksCount++;
                }
            }

            if (marksCount == 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool EndGame(Dictionary<string, string> game, string choice)
        {
            string symbole = game[choice];

            List<string> lines = new List<string>
            {
                "A",
                "B",
                "C"
            };

            List<string> columns = new List<string>
            {
                "1",
                "2",
                "3"
            };

            char l = choice[0];
            char c = choice[1];

            // initialisation différentes possibilité de gagner
            List<string> line = new List<string>
            {
                l + "1",
                l + "2",
                l + "3"
            };

            List<string> col = new List<string>
            {
                "A" + c,
                "B" + c,
                "C" + c
            };

            List<string> diag1 = new List<string>
            {
                "A1",
                "B2",
                "C3"
            };

            List<string> diag2 = new List<string>
            {
                "A3",
                "B2",
                "C1"
            };


            if (Count3Marks(game, symbole, line) || Count3Marks(game, symbole, col) || Count3Marks(game, symbole, diag1) || Count3Marks(game, symbole, diag2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
