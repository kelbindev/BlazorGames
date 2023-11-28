namespace LogicLayer.MineSweeper;
public class Level
{
    public string LevelName { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public List<Level> AllLevels()
    {
        return new List<Level>{
            new Level
        {
            LevelName = "Beginner",
            Width = 10,
            Height = 10
        },
        new Level
        {
            LevelName = "Intermediate",
            Width = 16,
            Height = 16
        },
            //COMMENT FIRST AS different width and height will cause error
            //new Level
            //{
            //    LevelName = "Expert",
            //    Width = 30,
            //    Height = 16
            //};
        };
    }
}
