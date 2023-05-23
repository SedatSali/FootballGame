using System;
using System.Collections.Generic;

namespace FootBallGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Team1: 11 players: Player11, Player12, ..., Player111, Player112
            // Formation 4-4-2
            Team team1 = new Team();
            for (int i = 1; i <= 11; i++)
            {
                if (i < 1)
                {
                    Goalkeeper player = new Goalkeeper
                    {
                        Name = "Player1" + i.ToString(),
                        Age = genRandNum(21),
                        Number = i,
                        Height = genRandNum(175)
                    };
                    team1.Players.Add(player);
                }
                else if (i <= 4)
                {
                    Defender player = new Defender
                    {
                        Name = "Player1" + i.ToString(),
                        Age = genRandNum(21),
                        Number = i,
                        Height = genRandNum(175)
                    };
                    team1.Players.Add(player);
                } else if (i > 4 && i <= 9) {
                    Midfield player = new Midfield
                    {
                        Name = "Player1" + i.ToString(),
                        Age = genRandNum(21),
                        Number = i,
                        Height = genRandNum(175)
                    };
                    team1.Players.Add(player);
                } else
                {
                    Striker player = new Striker
                    {
                        Name = "Player1" + i.ToString(),
                        Age = genRandNum(21),
                        Number = i,
                        Height = genRandNum(175)
                    };
                    team1.Players.Add(player);
                }   
            }
            team1.Coach = new Coach { Name = "Coach1", Age = 41 };

            // Team2: 11 players: Player21, Player22, ..., Player211, Player212
            // Formation 4-4-2
            Team team2 = new Team();
            for (int i = 1; i <= 11; i++)
            {
                if (i < 1)
                {
                    Goalkeeper player = new Goalkeeper
                    {
                        Name = "Player2" + i.ToString(),
                        Age = genRandNum(21),
                        Number = i,
                        Height = genRandNum(175)
                    };
                    team2.Players.Add(player);
                }
                else if (i <= 4)
                {
                    Defender player = new Defender
                    {
                        Name = "Player2" + i.ToString(),
                        Age = genRandNum(21),
                        Number = i,
                        Height = genRandNum(175)
                    };
                    team2.Players.Add(player);
                }
                else if (i > 4 && i <= 9)
                {
                    Midfield player = new Midfield
                    {
                        Name = "Player2" + i.ToString(),
                        Age = genRandNum(21),
                        Number = i,
                        Height = genRandNum(175)
                    };
                    team2.Players.Add(player);
                }
                else
                {
                    Striker player = new Striker
                    {
                        Name = "Player2" + i.ToString(),
                        Age = genRandNum(21),
                        Number = i,
                        Height = genRandNum(175)
                    };
                    team2.Players.Add(player);
                }
            }
            team2.Coach = new Coach { Name = "Coach2", Age = 42 };

            // Game
            Person[] assistantReferees = new Person[2];
            assistantReferees[0] = new Person
            {
                Name = "AssistantReferee1",
                Age = 31
            };

            assistantReferees[1] = new Person
            {
                Name = "AssistantReferee2",
                Age = 32
            };

            string userAnswer = "";
            do
            {
                Console.Clear();    
                mainGameLogic(team1, team2, assistantReferees);
                Console.WriteLine("Do you want to play another game? (type \"n\" to stop)");
                userAnswer = Console.ReadLine();
            } while (userAnswer.ToLower() != "n");
        }

        public static void mainGameLogic(Team team1, Team team2, Person[] assistantReferees)
        {
            List<Goal> Goals = new List<Goal>();
            for (int i = 0; i < genRandNumBetween(1, 4); i++)
            {
                Goal newGoal = new Goal
                {
                    Minute = genRandNumBetween(1, 90),
                    Player = team1.Players[genRandNumBetween(0, (team1.Players.Count) - 1)]
                };
                Goals.Add(newGoal);
            }

            for (int i = 0; i < genRandNumBetween(1, 4); i++)
            {
                Goal newGoal = new Goal
                {
                    Minute = genRandNumBetween(1, 90),
                    Player = team2.Players[genRandNumBetween(0, (team2.Players.Count) - 1)]
                };
                Goals.Add(newGoal);
            }

            Game game = new Game
            {
                Team1 = team1,
                Team2 = team2,
                Referee = new Referee { Name = "Referee1", Age = 35 },
                AssistantReferees = assistantReferees,
                Goals = Goals
            };
            game.Result = game.calculateResult();
            game.Winner = game.getWinnerTeam();


            Console.WriteLine(game.getGameResults());
        }

        private static int genRandNum(int value)
        {
            Random random = new Random();
            int jitterRange = (int)(value * 0.1);
            int randomNumber = random.Next(value - jitterRange, value + jitterRange + 1);
            return randomNumber;
        }

        public static int genRandNumBetween(int minValue, int maxValue)
        {
            Random random = new Random();
            return random.Next(minValue, maxValue + 1);
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class FootballPlayer : Person
    {
        public int Number { get; set; }
        public double Height { get; set; }
        public string Role { get; set; }
    }

    public class Goalkeeper : FootballPlayer {
        public Goalkeeper()
        {
            Role = "Goalkeeper";
        }
    }

    public class Defender : FootballPlayer {
        public Defender()
        {
            Role = "Defender";
        }
    }

    public class Midfield : FootballPlayer {
        public Midfield()
        {
            Role = "Midfield";
        }
    }

    public class Striker : FootballPlayer {
        public Striker()
        {
            Role = "Striker";
        }
    }

    public class Coach : Person { }

    public class Referee : Person { }

    public class Team
    {
        public Coach Coach { get; set; }
        public List<FootballPlayer> Players = new List<FootballPlayer>();

        public double GetAveragePlayerAge()
        {
            int totalAge = 0;

            foreach (var player in Players)
            {
                totalAge += player.Age;
            }

            return (double)totalAge / Players.Count;
        }
    }

    public class Goal
    {
        public int Minute { get; set; }
        public FootballPlayer Player { get; set; }
    }

    public class Game
    {
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public Referee Referee { get; set; }
        public Person[] AssistantReferees { get; set; }
        public List<Goal> Goals = new List<Goal>();
        public string Result { get; set; }
        public Team Winner { get; set; }

        public string getGameResults()
        {
            string gameResult = "Team1 vs Team2 results:\n  Goals:";
            Goals.Sort((goal1, goal2) => goal1.Minute.CompareTo(goal2.Minute));

            for (int i = 0; i < Goals.Count; i++)
            {
                gameResult += "\n  #" + (i+1) + "\n  - min: " + Goals[i].Minute + "\n  - by player: " + Goals[i].Player.Name + "\n  - role: " + Goals[i].Player.Role;
            }

            gameResult += "\nTeam1 - Team2 : " + Result;

            return gameResult;
        }

        public Team getWinnerTeam()
        {
            string[] results = Result.Split('-');
            if (int.Parse(results[0]) > int.Parse(results[1]))
            {
                return Team1;
            } else
            {
                return Team2;
            }
        }

        public string calculateResult()
        {   
            int team1Goals = 0, team2Goals = 0;
            for (int i = 0; i < Goals.Count; i++)
            {
                if (Goals[i].Player.Name.Contains("Player1"))
                {
                    team1Goals++;
                } else
                {
                    team2Goals++;
                }
            }

            return team1Goals.ToString() + "-" + team2Goals.ToString();
        }
    }
}
