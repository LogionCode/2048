using System;

namespace _2048
{
    class Table
    {
        static readonly Random random = new Random();
        Scoreboard scoreboard;
        Cell[] cells;
        int actions = 0;

        public Table()
        {
            scoreboard = new Scoreboard();
            cells = new Cell[16];
        }

        public void initialize() // refresh tiles
        {
            for (int index = 0; index < 16; index++)
                cells[index] = new Cell();

            actions = 0;
            spawn(2);
            show();

            while (!hasEnded())
                tick();
        }

        public void show() // toDO: include graphics and move from console to window
        {
            Console.Clear();
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                    Console.Write("{0} ", cells[row * 4 + col].toString());
                Console.WriteLine();
            }
            Console.WriteLine("Actions performed: {0}", actions);

        }

        public void tick()
        {
            short x = 0; int y = 0;
            char dir = ' ';

            show();

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
                else
                    dir = ' ';
            }
            move(x, y);

        }
        public bool move(int x, int y) // returns whether an action was made
        {
            if (hasEnded()) // if no tiles can move it's game over
                return false;

            int moves = x != 0 ? moveX(x) : moveY(y);

            if (moves == 0) // if no tiles moved it's not an action
                return false;

            spawn(1);
            actions++;
            return true; // action was performed
        }

        public bool hasEnded() // check if the game should end
        {
            //if (!isFull() || canMove()) // if there is a move to be made it's not
                //return false;

            int score = 0;
            for (int i = 0; i < 16; i++)
                score += cells[i].getValue();

            show();

            Console.WriteLine("Game has ended!");
            Console.WriteLine("Please give your name");

            string name = Console.ReadLine();
            scoreboard.addScore(new Record(name, score, actions));

            Record[] scores = scoreboard.readSorted();

            Console.WriteLine("\nScoreboard:"); // show 5 best scores
            for(int i = 0; i < 5 && i < scores.Length; i++)
                Console.WriteLine('\t' + scores[i].toString());

            Console.WriteLine("Press Enter to close");
            Console.Read(); // wait so scoreboard can be seen
            return true;
        }

        public bool isFull()
        {
            for (int i = 0; i < 14; i++)
                if (cells[i].isEmpty())
                    return false;
            return true;
        }

        void spawn(int x) // spawns x tiles with value 2/4 on table if possible
        {
            if (isFull()) // if called on full table it won't get stuck
                return;

            int index, lastIndex = -1, amount = 0;
            while (amount < x)
            {
                index = random.Next(16);
                if (lastIndex == index || !cells[index].isEmpty())
                    continue;

                if (random.Next(10) < 2) // 1/5 chance to be 4
                    cells[index].addValue(4);
                else
                    cells[index].addValue(2);
                lastIndex = index; amount++;
            }
        }

        int moveX(int x) // move horizontally
        {
            bool isDone = false;
            int moves = 0;

            while (!isDone)
            {
                isDone = true;
                for (int row = 0; row < 4; row++)
                {
                    if (x < 0)
                    {
                        for (int col = 1; col < 4; col++)
                            if (cells[row * 4 + col].moveTo(cells[row * 4 + col - 1]))
                            {
                                isDone = false; moves++;
                            }
                    }
                    else
                        for (int col = 2; col >= 0; col--)
                            if (cells[row * 4 + col].moveTo(cells[row * 4 + col + 1]))
                            {
                                isDone = false; moves++;
                            }
                }
            }
            return moves;
        }

        int moveY(int y) // move vertically
        {
            bool isDone = false;
            int moves = 0;

            while (!isDone)
            {
                isDone = true;
                for (int col = 0; col < 4; col++)
                {
                    if (y < 0)
                    {
                        for (int row = 1; row < 4; row++)
                            if (cells[row * 4 + col].moveTo(cells[(row - 1) * 4 + col]))
                            {
                                isDone = false; moves++;
                            }

                    }
                    else
                        for (int row = 2; row >= 0; row--)
                            if (cells[row * 4 + col].moveTo(cells[(row + 1) * 4 + col]))
                            {
                                isDone = false; moves++;
                            }
                }
            }
            return moves;
        }

        bool canMove() // check if any move can be made
        {
            for (int i = 0; i < 14; i++)
            {
                if (i - 4 >= 0 && cells[i].canMove(cells[i - 4])) // tile top of this 
                    return true;
                else if (i + 4 < 14 && cells[i].canMove(cells[i + 4])) // tile below this
                    return true;
                else if (i - 1 >= 0 && cells[i].canMove(cells[i - 1])) // tile left of this
                    return true;
                else if (i + 1 < 14 && cells[i].canMove(cells[i + 1])) // tile right of this
                    return true;
            }
            return false; // can't move to any tile
        }
    }
}

/*
 * Game rules:
 * - game starts with 2 randomly selected tiles getting filled with 2
 * - every action 1 tile gets filled with 2
 * - action is a move where at least 1 tile moves
 * - tile can move to empty tile
 * - tile can sum up if they are the same value
 * - tile can only sum up once per action (REMOVED - more fun to add few at once)
 * - game is lost when table is full and action can't be made // DONE - when table is full but an action can be made it's not yet lost
 */
