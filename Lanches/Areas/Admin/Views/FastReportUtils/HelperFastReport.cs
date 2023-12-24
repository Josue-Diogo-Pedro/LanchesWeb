using System.Data;

namespace Lanches.Areas.Admin.Views.FastReportUtils;

public class HelperFastReport
{
    public static DataTable GetDataTable<TEntity>(IEnumerable<TEntity> table, string name) where TEntity : class
    {
        var offset = 78;
    }
}
