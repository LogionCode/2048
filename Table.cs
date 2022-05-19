using System;

namespace _2048
{
    class Table
    {
        static Random random = new Random();
        Cell[] cells;

        public Table()
        {
            cells = new Cell[16];
            for (int index = 0; index < 16; index++)
                cells[index] = new Cell();

            spawn(2);
        }
        public bool isFull()
        {
            for (int i = 0; i < 14; i++)
                if (cells[i].isEmpty())
                    return false;
            return true;
        }
        public void show()
        {
            Console.Clear();
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                    Console.Write("{0} ", cells[row * 4 + col].toString());
                Console.WriteLine();
            }
        }

        public void move(int x, int y)
        {
            if (isFull())
                return;

            int moves = x != 0 ? moveX(x) : moveY(y);

            if(moves != 0) // if no tiles can move it's not a action
                spawn(1);
            freeMovement();
        }

        void spawn(int x)
        {
            if (isFull()) // if called on full table it won't get stuck
                return;

            int amount = 0, lastIndex = -1, index;
            while (amount < x)
            {
                index = random.Next(16);
                if (lastIndex == index || !cells[index].isEmpty())
                    continue;

                cells[index].addValue(2);
                lastIndex = index; amount++;
            }
        }

        void freeMovement()
        {
            for (int i = 0; i < 14; i++)
                cells[i].freeMove();
        }
        int moveX(int x)
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
                                isDone = false;
                                moves++;
                            }
                    }
                    else
                        for (int col = 2; col >= 0; col--)
                            if (cells[row * 4 + col].moveTo(cells[row * 4 + col + 1]))
                            {
                                isDone = false;
                                moves++;
                            }
                }
            }
            return moves;

        }

        int moveY(int y)
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
                                isDone = false;
                                moves++;
                            }

                    }
                    else
                        for (int row = 2; row >= 0; row--)
                            if (cells[row * 4 + col].moveTo(cells[(row + 1) * 4 + col]))
                            {
                                isDone = false;
                                moves++;
                            }
                }
            }
            return moves;
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
 * - tile can only sum up once per action
 * - game is lost when table is full // toDo when table is full but an action can be made it's not yet lost
 */
