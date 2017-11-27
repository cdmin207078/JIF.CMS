using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Logging;

namespace JIF.CMS.Test.Core.Tests
{
    /// <summary>
    /// CommonLogTest 的摘要说明
    /// </summary>
    [TestClass]
    public class CommonLogTest
    {
        [TestMethod]
        public void 不同级别日志_不同输出文件_不指定Logger名称_Test()
        {
            /*
             * 使用配置文件中 root 节点配置信息,记录日志. 若无 root 配置信息, 则无法记录日志
             */
            var logger = LogManager.GetLogger("");

            logger.Info("log world");
            logger.Info("给你我的喜与悲不止为那山与水, 分不清是梦与醒忘不掉是你身影, 穿过岁月春与秋尝尽世间爱与愁");

            try
            {
                var a = 1;
                var b = 0;
                var c = a / b;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        [TestMethod]
        public void 根据LoggerName_使用不用输出文件_记录日志()
        {
            /*
             * 若配置文件中, 同时配置了 root 节点, 则在使用logger记录时, 同时使用 root 配置再记录一次
             */
            var loggerInfo = LogManager.GetLogger("app-info");
            var loggerError = LogManager.GetLogger("app-error");

            //loggerInfo.Info("loggerInfo info");
            //loggerInfo.Error("loggerInfo error");

            loggerError.Info("loggerError info");
            loggerError.Error("loggerError error");
        }
    }
}