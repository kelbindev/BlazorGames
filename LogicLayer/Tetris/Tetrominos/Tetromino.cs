using LogicLayer.Tetris.Enum;

namespace LogicLayer.Tetris.Tetrominos;
public class Tetromino
{
    public Tetromino(Grid grid)
    {
        Grid = grid;
        CenterPieceRow = grid.Height;
        CenterPieceColumn = grid.Width / 2;
    }
    // Represents the grid on which this tetromino can move.
    public Grid Grid { get; set; }

    // The current orientation of this tetromino. 
    // Tetrominos rotate about their center.
    public TetrominoOrientation Orientation { get; set; }
        = TetrominoOrientation.LeftRight;

    // The X-coordinate of the center piece.
    public int CenterPieceRow { get; set; }

    // The Y-coordinate of the center piece.
    public int CenterPieceColumn { get; set; }

    // The style of this tetromino, e.g. Straight, Block, T-Shaped, etc.
    public virtual TetrominoStyle Style { get; }

    // The CSS class that is unique to this style of tetromino.
    public virtual string CssClass { get; }

    // A collection of all spaces currently occupied by this tetromino.
    // This collection is calculated by each style.
    public virtual CellCollection CoveredCells { get; }

    public bool CanMoveLeft()
    {
        //For each of the covered spaces, 
        //get the space immediately to the left
        foreach (var cell in CoveredCells.GetLeftmost())
        {
            if (Grid.Cells.Contains(cell.Row, cell.Column - 1))
                return false;
        }

        //If any of the covered spaces are currently in the leftmost column,
        //the piece cannot move left.
        if (CoveredCells.HasColumn(1))
            return false;

        return true;
    }

    public bool CanMoveRight()
    {
        //For each of the covered spaces, 
        //get the space immediately to the right
        foreach (var cell in CoveredCells.GetRightmost())
        {
            if (Grid.Cells.Contains(cell.Row, cell.Column + 1))
                return false;
        }

        //If any of the covered spaces are currently in the rightmost column,
        //the piece cannot move right.
        if (CoveredCells.HasColumn(Grid.Width))
            return false;

        return true;
    }
    public bool CanMoveDown()
    {
        //For each of the covered spaces, get the space immediately below
        foreach (var coord in CoveredCells.GetLowest())
        {
            if (Grid.Cells.Contains(coord.Row - 1, coord.Column))
                return false;
        }

        //If any of the covered spaces are currently in the lowest row, 
        //the piece cannot move down.
        if (CoveredCells.HasRow(1))
            return false;

        return true;
    }

    /// Returns whether or not the tetromino can move in any direction (down, left, right).
    public bool CanMove()
    {
        return CanMoveDown() || CanMoveLeft() || CanMoveRight();
    }

    public void MoveLeft()
    {
        if (CanMoveLeft()) CenterPieceColumn--;
    }

    public void MoveRight()
    {
        if (CanMoveRight()) CenterPieceColumn++;
    }

    public void MoveDown()
    {
        if (CanMoveDown())
            CenterPieceRow--;
    }
    /// Make the tetromino drop all the way to its lowest possible point.
    public int Drop()
    {
        int scoreCounter = 0;
        while (CanMoveDown())
        {
            MoveDown();
            scoreCounter++;
        }

        return scoreCounter;
    }
    // Rotates the tetromino around the center piece. 
    // Tetrominos always rotate clockwise.
    public void Rotate()
    {
        switch (Orientation)
        {
            case TetrominoOrientation.UpDown:
                Orientation = TetrominoOrientation.RightLeft;
                break;

            case TetrominoOrientation.RightLeft:
                Orientation = TetrominoOrientation.DownUp;
                break;

            case TetrominoOrientation.DownUp:
                Orientation = TetrominoOrientation.LeftRight;
                break;

            case TetrominoOrientation.LeftRight:
                Orientation = TetrominoOrientation.UpDown;
                break;
        }

        var coveredSpaces = CoveredCells;

        //If the new rotation of the tetromino means it would be outside the
        //play area, shift the center cell so as to 
        //keep the entire tetromino visible.
        if (coveredSpaces.HasColumn(-1))
        {
            CenterPieceColumn += 2;
        }
        else if (coveredSpaces.HasColumn(12))
        {
            CenterPieceColumn -= 2;
        }
        else if (coveredSpaces.HasColumn(0))
        {
            CenterPieceColumn++;
        }
        else if (coveredSpaces.HasColumn(11))
        {
            CenterPieceColumn--;
        }
    }
}
