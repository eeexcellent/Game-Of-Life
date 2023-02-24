internal class Program
{
    public const char DEAD = '.';
    public const char LIVE = '*';
    // Game of Life
    static public char[,] Execute(char[,] cells)
    {
        char[,] nextGen = new char[cells.GetLength(0), cells.GetLength(1)];

        // Test on invalid values.
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                if (cells[i, j] != DEAD && cells[i, j] != LIVE)
                {
                    throw new ArgumentException("Game scope should contain only * and . symbols.");
                }
                nextGen[i, j] = cells[i, j];
            }
        }

        // Creates a next generation.
        for (int row = 0; row < cells.GetLength(0); row++)
        {
            for (int col = 0; col < cells.GetLength(1); col++)
            {
                int neighbors = CountNeighbors(cells, row, col);

                // Any live cell less than two or more than three live neighbours die.
                if (cells[row, col] == LIVE && (neighbors < 2 || neighbors > 3))
                {
                    nextGen[row, col] = DEAD;
                }

                // Any dead cell with three live neighbours becomes a live cell.
                if (cells[row, col] == DEAD && neighbors == 3)
                {
                    nextGen[row, col] = LIVE;
                }
            }
        }

        return nextGen;
    }
    private static int CountNeighbors(char[,] cells, int row, int col)
    {
        int counter = 0;

        // Calculate a number of neighbors.
        for (int i = row - 1; i < row + 2; i++)
        {
            for (int j = col - 1; j < col + 2; j++)
            {
                if (!((i < 0 || j < 0) || (i >= cells.GetLength(0) || j >= cells.GetLength(1))))
                {
                    if (cells[i, j] == LIVE)
                    {
                        counter++;
                    }
                }
            }
        }
        // No need to count the cell itself.
        return cells[row, col] == LIVE ? counter - 1 : counter;
    }

    private static void Main(string[] args)
    {
        // TESTING THE GAME.
        char[,] firstGen = new char[,]
        {
            {DEAD, DEAD, DEAD, DEAD, DEAD, DEAD},
            {DEAD, LIVE, LIVE, DEAD, DEAD, DEAD},
            {DEAD, LIVE, LIVE, DEAD, DEAD, DEAD},
            {DEAD, DEAD, DEAD, LIVE, LIVE, DEAD},
            {DEAD, DEAD, DEAD, LIVE, LIVE, DEAD},
            {DEAD, DEAD, DEAD, DEAD, DEAD, DEAD}
        };

        char[,] secondGen = new char[firstGen.GetLength(0), firstGen.GetLength(1)];
        try
        {
            secondGen = Execute(firstGen);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadKey();
            return;
        }

        // Displaying 1st generation.
        for (int i = 0; i < firstGen.GetLength(0); i++)
        {
            for (int j = 0; j < firstGen.GetLength(1); j++)
            {
                Console.Write(firstGen[i, j] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();


        // Displaying 2nd genereation.
        for (int i = 0; i < secondGen.GetLength(0); i++)
        {
            for (int j = 0; j < secondGen.GetLength(1); j++)
            {
                Console.Write(secondGen[i, j] + " ");
            }
            Console.WriteLine();
        }

        Console.ReadLine();
    }
}