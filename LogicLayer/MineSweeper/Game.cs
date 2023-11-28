using LogicLayer.MineSweeper.Enum;
using System;
using Towel;

namespace LogicLayer.MineSweeper;
public class Game
{
    public int Width { get; set; } = 10;
    public int Height { get; set; } = 10;
    private double MineRatio = .1d;
    int MineCount => (int)(Width * Height * MineRatio);

    public Cell[,] GenerateBoard(int w = 10, int h = 10)
    {
        Random random = new Random();

        Width = w;
        Height = h;

        Cell[,] board = new Cell[Width, Height];
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                board[i, j] = new Cell();
            }
        }
        foreach (int m in random.NextUnique(MineCount, 0, Width * Height))
        {
            int i = m / Width;
            int j = m % Width;
            board[i, j].Value = (int)_Mine.mine;
            foreach (var tile in AdjacentTiles(j, i))
            {
                if (board[tile.Row, tile.Column].Value != (int)_Mine.mine)
                {
                    board[tile.Row, tile.Column].Value++;
                }
            }
        }
        return board;
    }

    public void ClickCell(int row, int column, Cell[,] Board)
    {
        if (Board[row, column].Value == (int)_Mine.mine)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    Board[i, j].Revealed = true;
                }
            }
            return;
        }
        Reveal(row, column, Board);
    }

    public void Reveal(int row, int column, Cell[,] Board)
    {
        Board[row, column].Revealed = true;
        if (Board[row, column].Value == 0)
        {
            foreach (var (Row, Column) in AdjacentTiles(column, row))
            {
                if (!Board[Row, Column].Revealed)
                {
                    Reveal(Row, Column, Board);
                }
            }
        }
    }

    IEnumerable<(int Row, int Column)> AdjacentTiles(int column, int row)
    {
        //    A B C
        //    D + E
        //    F G H

        /* A */
        if (row > 0 && column > 0) yield return (row - 1, column - 1);
        /* B */
        if (row > 0) yield return (row - 1, column);
        /* C */
        if (row > 0 && column < Width - 1) yield return (row - 1, column + 1);
        /* D */
        if (column > 0) yield return (row, column - 1);
        /* E */
        if (column < Width - 1) yield return (row, column + 1);
        /* F */
        if (row < Height - 1 && column > 0) yield return (row + 1, column - 1);
        /* G */
        if (row < Height - 1) yield return (row + 1, column);
        /* H */
        if (row < Height - 1 && column < Width - 1) yield return (row + 1, column + 1);
    }

    public bool WinCheck(Cell[,] Board)
    {
        int revealCount = 0;
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (Board[i, j].Revealed)
                {
                    revealCount++;
                }
            }
        }
        return revealCount == Width * Height - MineCount;
    }
}
