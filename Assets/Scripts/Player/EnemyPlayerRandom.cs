public class EnemyPlayerRandom : EnemyPlayerSequential
{
    private Cell _destCell;

    protected override Direction GetDirection()
    {
        if (_destCell == null || _destCell == grid.currentCell)
        {
            while (_destCell == null || _destCell == grid.currentCell)
                _destCell = grid.GetRandomCell();
        }

        var dir = GetNextDirectionByCoords(grid.currentCell, _destCell);
        return dir;
    }
}
