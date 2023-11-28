namespace LogicLayer.Tetris;
public class CellCollection
{
    private readonly List<Cell> _cells = new List<Cell>();

    //Checks if there are any occupied cells in the given row
    public bool HasRow(int row)
    {
        return _cells.Any(c => c.Row == row);
    }

    //Checks if there are any occupied celld in the given column
    public bool HasColumn(int column)
    {
        return _cells.Any(c => c.Column == column);
    }

    //Checks if there is an occupied cell at the given coordinates.
    public bool Contains(int row, int column)
    {
        return _cells.Any(c => c.Row == row && c.Column == column);
    }

    public void Add(int row, int column)
    {
        _cells.Add(new Cell(row, column));
    }

    //Adds several new cells, each with the given CSS class
    public void AddMany(List<Cell> cells, string cssClass)
    {
        foreach (var cell in cells)
        {
            _cells.Add(new Cell(cell.Row, cell.Column, cssClass));
        }
    }

    //Returns all occupied cells
    public List<Cell> GetAll()
    {
        return _cells;
    }

    /// Returns all occupied cell in a given row.
    public List<Cell> GetAllInRow(int row)
    {
        return _cells.Where(x => x.Row == row).ToList();
    }

    /// Gets the CSS class of an individual cell.
    public string GetCssClass(int row, int column)
    {
        var matching = _cells.FirstOrDefault(x => x.Row == row && x.Column == column);

        if (matching != null)
            return matching.CssClass;

        return "";
    }

    //Adds a CSS class to every cell in a given row
    public void SetCssClass(int row, string cssClass)
    {
        _cells.Where(x => x.Row == row)
              .ToList()
              .ForEach(x => x.CssClass = cssClass);
    }

    //Moves all "higher" cells down to fill in the specified completed rows.
    public void CollapseRows(List<int> rows)
    {
        //Get all cells in the completed rows
        var selectedCells = _cells.Where(x => rows.Contains(x.Row));

        //Add those cells to a temporary collection
        List<Cell> toRemove = new List<Cell>();
        foreach (var cell in selectedCells)
        {
            toRemove.Add(cell);
        }

        //Remove all cells in the temporary collection 
        //from the real collection.
        _cells.RemoveAll(x => toRemove.Contains(x));

        //"Collapse" the rows above the complete rows by moving them down.
        foreach (var cell in _cells)
        {
            int numberOfLessRows = rows.Where(x => x <= cell.Row).Count();
            cell.Row -= numberOfLessRows;
        }
    }

    // Gets the rightmost (highest Column value) cell in the collection.
    public List<Cell> GetRightmost()
    {
        List<Cell> cells = new List<Cell>();
        foreach (var cell in _cells)
        {
            if (!Contains(cell.Row, cell.Column + 1))
            {
                cells.Add(cell);
            }
        }

        return cells;
    }

    // Gets the leftmost (lowest Column value) cell in the collection.
    public List<Cell> GetLeftmost()
    {
        List<Cell> cells = new List<Cell>();
        foreach (var cell in _cells)
        {
            if (!Contains(cell.Row, cell.Column - 1))
            {
                cells.Add(cell);
            }
        }

        return cells;
    }

    // Gets the lowest (lowest Row value) cell in the collection. 
    public List<Cell> GetLowest()
    {
        List<Cell> cells = new List<Cell>();
        foreach (var cell in _cells)
        {
            if (!Contains(cell.Row - 1, cell.Column))
            {
                cells.Add(cell);
            }
        }

        return cells;
    }
}
