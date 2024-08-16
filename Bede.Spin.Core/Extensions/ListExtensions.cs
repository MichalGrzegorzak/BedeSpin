namespace Bede.Spin.Core.Extensions;

public static class ListExtensions
{
    public static List<List<T>> ChopListIntoRowsAndColumns<T>(this List<T> inputList, int rows, int columns)
    {
        var rowsAndColumns = new List<List<T>>();
        int idx = 0;
        
        foreach (var row in Enumerable.Range(1, rows))
        {
            var rowLine = new List<T>();
            
            foreach (var column in Enumerable.Range(1, columns))
            {
                rowLine.Add(inputList[idx]);
                idx++;
            }
            rowsAndColumns.Add(rowLine);
        }

        return rowsAndColumns;
    }
}