using System;

namespace _2048
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table();
            while (!table.isFull())
            {
                string dir = "";
                int x = 0; int y = 0;

                table.show();

                while (dir.Length == 0)
                {
                    dir = Console.ReadLine();
                    if (dir == "a")
                        x--;
                    else if (dir == "d")
                        x++;
                    else if (dir == "w")
                        y--;
                    else if (dir == "s")
                        y++;
                    else
                        dir = "";
                }

                table.move(x, y);
            }
            Console.Read();
        }
    }
}

