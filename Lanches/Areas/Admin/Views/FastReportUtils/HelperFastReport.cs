using System.Data;
using System.Reflection;

namespace Lanches.Areas.Admin.Views.FastReportUtils;

public class HelperFastReport
{
    public static DataTable GetDataTable<TEntity>(IEnumerable<TEntity> table, string name) where TEntity : class
    {
        var offset = 78;
        DataTable result = new(name);

        PropertyInfo[] infos = typeof(TEntity).GetProperties();
        foreach (PropertyInfo info in infos)
        {
            if(info.PropertyType.IsGenericType &&
                )
        }
    }
}
