using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int[][] suduku = {
             new int[] {7,8,4, 1,5,9, 3,2,6},
             new int[] {5,3,9, 6,7,2, 8,4,1},
             new int[] {6,1,2, 4,3,8, 7,5,9},

             new int[] {9,2,8, 7,1,5, 4,6,3},
             new int[] {3,5,7, 8,4,6, 1,9,2},
             new int[] {4,6,1, 9,2,3, 5,8,7},

             new int[] {8,7,6, 3,9,4, 2,1,5},
             new int[] {2,4,3, 5,6,1, 9,7,8},
             new int[] {1,9,5, 2,8,7, 6,3,4}
          };

        Console.WriteLine(ValidateSudoku(suduku) ? "Sudoku is valid" : "Invalid Sudoku");

    }

    public static bool ValidateSudoku(int[][] grid)
    {
        int N = grid.Length;
        if(!IsValidDimension(N))
            return false;
        return ValidateRows(grid) && ValidateColumns(grid) && AreSubgridsValid(grid);
    }

    private static bool IsValidDimension(int N)
    {
        var sqrtN = Math.Sqrt(N);
        return sqrtN % 1 == 0;
    }

    private static bool ValidateRows(int[][] grid)
    {
        foreach (var row in grid)
        {
            if (!ContainsAllUniqueNumbers(row))
                return false;
        }
        return true;
    }

    private static bool ValidateColumns(int[][] grid)
    {
        int N = grid.Length;

        for (int col = 0; col < N; col++)
        {
            var column = grid.Select(row => row[col]).ToArray();
            if (!ContainsAllUniqueNumbers(column))
                return false;
        }
        return true;
    }

    private static bool AreSubgridsValid(int[][] grid)
    {
        int N = grid.Length;
        int sqrtN = (int)Math.Sqrt(N);

        for (int rowStart = 0; rowStart < N; rowStart += sqrtN)
        {
            for (int colStart = 0; colStart < N; colStart += sqrtN)
            {
                if (!IsSubgridValid(grid, rowStart, colStart, sqrtN)) return false;
            }
        }
        return true;
    }

    private static bool IsSubgridValid(int[][] grid, int rowStart, int colStart, int size)
    {
        var subgrid = new HashSet<int>();

        for (int r = rowStart; r < rowStart + size; r++)
        {
            for (int c = colStart; c < colStart + size; c++)
            {
                subgrid.Add(grid[r][c]);
            }
        }
        return ContainsAllUniqueNumbers(subgrid.ToArray());
    }

    private static bool ContainsAllUniqueNumbers(int[] set)
    {
        int N = set.Length;
        return set.OrderBy(n => n).SequenceEqual(Enumerable.Range(1, N));
    }
}
