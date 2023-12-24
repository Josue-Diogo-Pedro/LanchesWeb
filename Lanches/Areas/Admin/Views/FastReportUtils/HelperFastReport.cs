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
                info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                result.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType)));
            }
            else
            {
                result.Columns.Add(new DataColumn(info.Name, info.PropertyType));  
            }
        }
        foreach (var el in table)
        {
            DataRow row = result.NewRow();
            foreach (PropertyInfo info in infos)
            {
                if (info.PropertyType.IsGenericType &&
                    info.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    object t = info.GetValue(el);
                    if(t is null)
                    {
                        t = Activator.CreateInstance(Nullable.GetUnderlyingType(info.PropertyType));
                    }
                    row[info.Name] = t;
                }
                else
                {
                    if(info.PropertyType == typeof(byte[]))
                    {
                        var imageData = (byte[])info.GetValue(el);
                        var bytes = new byte[imageData.Length - offset];
                        Array.Copy(imageData, offset, bytes, 0, imageData.Length);
                        row[info.Name] = bytes;
                    }
                    else
                    {
                        row[info.Name] = info.GetValue(el);
                    }
                }
            }
            result.Rows.Add(row);
        }
        return result;
    }
}
