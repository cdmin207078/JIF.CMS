using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.CMS.Core.Extensions
{
    public static class StringExtensions
    {
        //
        // 摘要:
        //     将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。
        //
        // 参数:
        //   format:
        //     复合格式字符串。
        //
        //   args:
        //     一个对象数组，其中包含零个或多个要设置格式的对象。
        //
        // 返回结果:
        //     format 的副本，其中格式项已替换为 args 中相应对象的字符串表示形式。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     format 或 args 为 null。
        //
        //   T:System.FormatException:
        //     format 无效。- 或 - 格式项的索引小于零，或者大于或等于 args 数组的长度。
        public static string Formats(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        //
        // 摘要:
        //     将指定字符串中的格式项替换为三个指定对象的字符串表示形式。
        //
        // 参数:
        //   format:
        //     复合格式字符串。
        //
        //   arg0:
        //     要设置格式的第一个对象。
        //
        //   arg1:
        //     要设置格式的第二个对象。
        //
        //   arg2:
        //     要设置格式的第三个对象。
        //
        // 返回结果:
        //     format 的副本，其中的格式项已替换为 arg0、arg1 和 arg2 的字符串表示形式。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     format 为 null。
        //
        //   T:System.FormatException:
        //     format 无效。- 或 - 格式项的索引小于零，或者大于二。
        public static string Formats(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(format, arg0, arg1, arg2);
        }
        //
        // 摘要:
        //     将指定字符串中的一个或多个格式项替换为指定对象的字符串表示形式。
        //
        // 参数:
        //   format:
        //     复合格式字符串。
        //
        //   arg0:
        //     要设置格式的对象。
        //
        // 返回结果:
        //     format 的副本，其中的任何格式项均替换为 arg0 的字符串表示形式。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     format 为 null。
        //
        //   T:System.FormatException:
        //     format 中的格式项无效。- 或 - 格式项的索引不为零。
        public static string Formats(this string format, object arg0)
        {
            return string.Format(format, arg0);
        }
        //
        // 摘要:
        //     将指定字符串中的格式项替换为两个指定对象的字符串表示形式。
        //
        // 参数:
        //   format:
        //     复合格式字符串。
        //
        //   arg0:
        //     要设置格式的第一个对象。
        //
        //   arg1:
        //     要设置格式的第二个对象。
        //
        // 返回结果:
        //     format 的副本，其中的格式项替换为 arg0 和 arg1 的字符串表示形式。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     format 为 null。
        //
        //   T:System.FormatException:
        //     format 无效。- 或 - 格式项的索引不为零或一。
        public static string Formats(this string format, object arg0, object arg1)
        {
            return string.Format(format, arg0, arg1);
        }
    }
}
