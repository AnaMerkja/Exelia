using System;
using System.Diagnostics;
using System.Linq;

public class SudokuValidator
{
    public static bool ValidateSudoku(int[][] sudoku)
    {
        int n = sudoku.Length;

        // Check if the input is valid
        if (n == 0 || Math.Sqrt(n) % 1 != 0)
            return false;

        // Check rows and columns
        for (int i = 0; i < n; i++)
        {
            if (!IsValidRow(sudoku, i) || !IsValidColumn(sudoku, i))
                return false;
        }

        // Check little squares
        int sqrtN = (int)Math.Sqrt(n);
        for (int i = 0; i < n; i += sqrtN)
        {
            for (int j = 0; j < n; j += sqrtN)
            {
                if (!IsValidLittleSquare(sudoku, i, j, sqrtN))
                    return false;
            }
        }

        return true;
    }

    private static bool IsValidRow(int[][] sudoku, int row)
    {
        return sudoku[row].OrderBy(x => x).SequenceEqual(Enumerable.Range(1, sudoku.Length));
    }

    private static bool IsValidColumn(int[][] sudoku, int col)
    {
        return sudoku.Select(row => row[col]).OrderBy(x => x).SequenceEqual(Enumerable.Range(1, sudoku.Length));
    }

    private static bool IsValidLittleSquare(int[][] sudoku, int startRow, int startCol, int size)
    {
        var values = Enumerable.Range(1, size * size);
        return sudoku.Skip(startRow).Take(size)
            .SelectMany(row => row.Skip(startCol).Take(size))
            .OrderBy(x => x)
            .SequenceEqual(values);
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        int[][] goodSudoku1 = {
                new int[] {7,8,4,  1,5,9,  3,2,6},
                new int[] {5,3,9,  6,7,2,  8,4,1},
                new int[] {6,1,2,  4,3,8,  7,5,9},

                new int[] {9,2,8,  7,1,5,  4,6,3},
                new int[] {3,5,7,  8,4,6,  1,9,2},
                new int[] {4,6,1,  9,2,3,  5,8,7},

                new int[] {8,7,6,  3,9,4,  2,1,5},
                new int[] {2,4,3,  5,6,1,  9,7,8},
                new int[] {1,9,5,  2,8,7,  6,3,4}
            };


        int[][] goodSudoku2 = {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            };

        int[][] badSudoku1 =  {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
            };

        int[][] badSudoku2 = {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},
                new int[] {1,2,3,4},
                new int[] {1}
            };


        Console.WriteLine($"Is the Sudoku1 valid? {SudokuValidator.ValidateSudoku(goodSudoku1)}");
        Console.WriteLine($"Is the Sudoku2 valid? {SudokuValidator.ValidateSudoku(goodSudoku2)}");
        Console.WriteLine($"Is the Sudoku3 valid? {SudokuValidator.ValidateSudoku(badSudoku1)}");
        Console.WriteLine($"Is the Sudoku4 valid? {SudokuValidator.ValidateSudoku(badSudoku2)}");
    }
}