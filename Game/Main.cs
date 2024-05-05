using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class Game
    {
        private static void Main(string[] args)
        {
            int PacmanX = 1;
            int PacmanY = 1;
            int EnemyX = 8;
            int EnemyY = 5;
            int score = 0;
            char[,] map = Map.ReadMap("map.txt");
            Pacman pac = new Pacman();
            Console.WriteLine(pac);
            while (true)
            {
                Console.Clear();
                
                Map.WriteMap(map);

                Score.WriteScore(score, map);

                Pacman.startPosition(PacmanX, PacmanY);
                
                Enemy.startPosition(EnemyX, EnemyY);

                int[] direction = Pacman.DirectionPacman(Console.ReadKey());

                Enemy.MoveEnemy(ref EnemyX, ref EnemyY, map);

                Pacman.MovePacman(direction, ref PacmanX, ref PacmanY, ref map, ref score, ref EnemyX, ref EnemyY);


            }
        }
    
    }
    class Pacman
    {
        public static void startPosition(int PacmanX, int PacmanY)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(PacmanX, PacmanY);
            Console.Write("@");
        }
        public static int[]  DirectionPacman(ConsoleKeyInfo key)
        {
            int[] direction = { 0, 0 };

            if (key.Key == ConsoleKey.UpArrow) { direction[1] -= 1; }
            else if (key.Key == ConsoleKey.DownArrow) { direction[1] += 1; ; }
            else if (key.Key == ConsoleKey.LeftArrow) { direction[0] -= 1; ; }
            else if (key.Key == ConsoleKey.RightArrow) { direction[0] += 1; };

            return direction;
        }
        public static int MovePacman(int[] direction, ref int PacmanX, ref int PacmanY, ref char[,] map, ref int score, ref int EnemyX, ref int EnemyY)
        {

            int nextPacmanPositionX = PacmanX + direction[0];
            int nextPacmanPositionY = PacmanY + direction[1];

            if (map[nextPacmanPositionX, nextPacmanPositionY] != '#')
            {
                PacmanX = nextPacmanPositionX;
                PacmanY = nextPacmanPositionY;
            }
            if (map[nextPacmanPositionX, nextPacmanPositionY] == '.')
            {
                PacmanX = nextPacmanPositionX;
                PacmanY = nextPacmanPositionY;
                map[nextPacmanPositionX, nextPacmanPositionY] = ' ';
                score++;
            }
            if (PacmanX == EnemyX && PacmanY == EnemyY)
            {
                Console.Clear();
                Console.WriteLine("Game Over!");
                Console.ReadKey();
                PacmanX = 1;
                PacmanY = 1;
                EnemyX = 8;
                EnemyY = 5;
                score = 0;
                map = Map.ReadMap("map.txt");
            }
            return score;
        }
    }
    class Score
    {
        public static void WriteScore(int score, char[,] map)
        {
            Console.SetCursorPosition(map.GetLength(0)+1, 0);
            Console.WriteLine(score);
        }
        
    }
    class Enemy
    {
        public static void startPosition(int EnemyX, int EnemyY)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(EnemyX, EnemyY);
            Console.Write("*");
        }
        public static void MoveEnemy(ref int EnemyX, ref int EnemyY, char[,] map)
        {
            Random random = new();
            int direction = random.Next(4);

            int nextEnemyPositionX = EnemyX;
            int nextEnemyPositionY = EnemyY;

            if (direction == 0) { nextEnemyPositionY -= 1; }
            else if (direction == 1) { nextEnemyPositionY += 1; ; }
            else if (direction == 2) { nextEnemyPositionX -= 1; ; }
            else if (direction == 3) { nextEnemyPositionX += 1; };

            if (map[nextEnemyPositionX, nextEnemyPositionY] != '#')
            {
                EnemyX = nextEnemyPositionX;
                EnemyY = nextEnemyPositionY;
            }
        }

    }
    class Map
    {
        public static char[,] ReadMap(string path)
        {
            string[] file = File.ReadAllLines(path);
            char[,] map = new char[MaxLengthLineMap(file), file.Length];
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = file[y][x];
                }
            }
            return map;
        }
        public static int MaxLengthLineMap(string[] lines)
        {
            int maxLenght = lines[0].Length;
            foreach(var line in lines)
                if(line.Length > maxLenght)
                {
                    maxLenght = line.Length;
                }
            return maxLenght;
        }
        public static void WriteMap(char[,] map)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(map[x, y]);
                }
                Console.Write("\n");
            }
        }
    }
}
