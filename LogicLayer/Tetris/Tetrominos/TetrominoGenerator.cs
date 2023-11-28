using LogicLayer.Tetris.Enum;

namespace LogicLayer.Tetris.Tetrominos;
public class TetrominoGenerator
{
    public TetrominoStyle Next(params TetrominoStyle[] unusableStyles)
    {
        Random rand = new Random(DateTime.Now.Millisecond);

        //Randomly generate one of the eight possible tetrominos
        var style = (TetrominoStyle)rand.Next(0, 6);

        //Re-generate the new tetromino until it is 
        //a style that is not one of the upcoming styles.
        while (unusableStyles.Contains(style))
            style = (TetrominoStyle)rand.Next(0, 6);

        return style;
    }

    public Tetromino CreateFromStyle(TetrominoStyle style, Grid grid)
    {
        return style switch
        {
            TetrominoStyle.Straight => new Straight(grid),
            TetrominoStyle.Block => new Block(grid),
            TetrominoStyle.TShaped => new TShaped(grid),
            TetrominoStyle.LeftZigZag => new LeftZigZag(grid),
            TetrominoStyle.RightZigZag => new RightZigZag(grid),
            TetrominoStyle.LShaped => new LShaped(grid),
            TetrominoStyle.ReverseLShaped => new ReverseLShaped(grid),
            _ => new Block(grid),
        };
    }
}
