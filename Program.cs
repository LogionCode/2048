using System;

namespace _2048
{
    internal class Program
    {
        static void Main()
        {
            Table table = new Table();
            table.initialize();

            while (!table.hasEnded())
            {
                int x = 0; int y = 0;

                table.show();

                char dir = ' ';
                while (dir == ' ')
                {
                    dir = Console.ReadKey(true).KeyChar; // toDo: do it better 
                    if (dir == 'a')
                        x--;
                    else if (dir == 'd')
                        x++;
                    else if (dir == 'w')
                        y--;
                    else if (dir == 's')
                        y++;
                    else if (dir == 'r')
                    {
                        table.initialize();
                        dir = ' ';
                    }
                    else
                        dir = ' ';
                }
                table.move(x, y);
            }


        }
    }
}

