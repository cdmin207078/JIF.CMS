using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Data.SqlServer
{
    public class SqlBulkHelper
    {
        public void Insert(DataTable sqldb)
        {
            //数据批量导入sqlserver,创建实例
            SqlBulkCopy sqlbulk = new SqlBulkCopy("Data Source=*;Initial Catalog=hds0880056_db;User ID=*;Password=*;");
            sqlbulk.BatchSize = 10000;//每次导入数据的量
            sqlbulk.BulkCopyTimeout = 9999;//每次导入的时间限制
                                           //目标数据库表名
            sqlbulk.DestinationTableName = "QQ_Info";
            //数据集字段索引与数据库字段索引映射
            sqlbulk.ColumnMappings.Add("QQ", "QQ");
            sqlbulk.ColumnMappings.Add("QQType", "QQType");
            sqlbulk.ColumnMappings.Add("QQPrice", "QQPrice");
            sqlbulk.ColumnMappings.Add("QQTime", "QQTime");

            //导入
            sqlbulk.WriteToServer(sqldb);
            sqlbulk.Close();
        }
    }
}
