using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarMaintainCommon
{
    public class LogHelper
    {
        //public static readonly ILog loginfo = log4net.LogManager.GetLogger("loginfo");

        //public static readonly ILog logerror = log4net.LogManager.GetLogger("logerror");
        //信息类日志对象
        private static ILog _loginfo = null;

        private static ILog loginfo
        {
            get
            {
                if (_loginfo == null)
                {
                    try
                    {
                        string configPath = Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath, "log4net.config");
                        FileInfo ff = new FileInfo(configPath);
                        log4net.Config.XmlConfigurator.Configure(ff);
                        Type type = MethodBase.GetCurrentMethod().DeclaringType;
                        _loginfo = LogManager.GetLogger("loginfo");
                    }
                    catch (Exception e)
                    {
                        throw new Exception("loginfo出错：" + e.Message);
                    }
                }
                return _loginfo;
            }
        }
        //错误类日志对象
        private static ILog _logerror = null;

        private static ILog logerror
        {
            get
            {
                if (_logerror == null)
                {
                    try
                    {
                        string configPath = Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath, "log4net.config");
                        FileInfo ff = new FileInfo(configPath);
                        log4net.Config.XmlConfigurator.Configure(ff);
                        Type type = MethodBase.GetCurrentMethod().DeclaringType;
                        _logerror = LogManager.GetLogger("logerror");
                    }
                    catch (Exception e)
                    {
                        throw new Exception("logerror出错：" + e.Message);
                    }
                }
                return _logerror;
            }
        }
        //自定义日志对象
        private static ILog _logCustom = null;

        private static ILog logCustom
        {
            get
            {
                if (_logCustom == null)
                {
                    try
                    {
                        string configPath = Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath, "log4net.config");
                        FileInfo ff = new FileInfo(configPath);
                        log4net.Config.XmlConfigurator.Configure(ff);
                        Type type = MethodBase.GetCurrentMethod().DeclaringType;
                        _logCustom = LogManager.GetLogger("CustomLog");
                    }
                    catch (Exception e)
                    {
                        throw new Exception("logCustom出错：" + e.Message);
                    }
                }
                return _logCustom;
            }
        }

        /// <summary>
        /// 更改记录的日志文件
        /// </summary>
        /// <param name="log"></param>
        /// <param name="subFolder">子目录，计划为各独立业务使用，
        /// 如宝钢会员接口，使用BaoGang文件夹，内部再增加一层日期文件夹
        /// </param>
        /// <param name="logFileName"></param>
        private static void ChangeCustomLogFileName(ILog log,string subFolder, string logFileName)
        {                        
            
            //string strPath = Path.Combine("d:\\测试\\", @"LOG\" +subFolder+"\\"+DateTime.Now.ToString("yyyyMMdd"))+"\\";
            try
            {
                string strPath = AppDomain.CurrentDomain.BaseDirectory + @"LOG\TRANSLOG\" + subFolder + @"\" + DateTime.Now.ToString("yyyyMMdd");
                string strFilename = strPath + @"\" + logFileName;

                var repository = LogManager.GetRepository();
                var appenders = repository.GetAppenders();
                var targetApder = appenders.First(p => p.Name == "CustomFileAppender") as log4net.Appender.RollingFileAppender;
                targetApder.File = strFilename;
                targetApder.MaxFileSize = 50 * 1024 * 1024;
                targetApder.ActivateOptions();
            }
            catch (Exception e)
            {
                throw new Exception("ChangeCustomLogFileName出错：" + e.Message);
            }
        }
        /// <summary>
        /// 使用Log4Net 写信息类日志
        /// </summary>
        /// <param name="info"></param>
        public static void WriteLog(string info)
        {
            //log4net.Config.XmlConfigurator.Configure();
            try
            {
                if (loginfo.IsInfoEnabled)
                {
                    loginfo.Info(info);
                }
            }
            catch (Exception e)
            {
                throw new Exception("WriteLog出错：" + e.Message);
            }
        }
        /// <summary>
        /// 使用Log4Net 写异常类日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="se"></param>
        public static void WriteLog(string info, Exception se)
        {
            try
            {
                if (logerror.IsErrorEnabled)
                {
                    logerror.Error(info, se);
                }
            }
            catch (Exception e)
            {
                throw new Exception("WriteLog出错：" + e.Message);
            }
        }

        public static void DynamicWriteLog(string subFolder, string filename, string info)
        {
            try
            {
                ChangeCustomLogFileName(logCustom, subFolder, filename);
                logCustom.Info(info);
            }
            catch (Exception e)
            {
                throw new Exception("DynamicWriteLog出错：" + e.Message);
            }
        }

        public static void LogStart()
        {
            //log4net.Config.XmlConfigurator.Configure();
        }

        public static void ShutDown()
        {
            LogManager.Shutdown();
        }

        /// <summary>  
        /// 自定义方法写入日志到文本文件   非Log4Net
        /// </summary>  
        /// <param name="action">动作</param>  
        /// <param name="strMessage">日志内容</param>  
        /// <param name="time">时间</param>  
        public static void WriteTextLog(string subFolder, string filename, string action, string strMessage, DateTime time)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"LOG\TRANSLOG\"+ subFolder + @"\" + DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fileFullPath = path + @"\" + filename;

            StringBuilder str = new StringBuilder();
            str.Append("Time:    " + time.ToString() + "\r\n");
            str.Append("Action:  " + action + "\r\n");
            str.Append("Message: " + strMessage + "\r\n");
            str.Append("-----------------------------------------------------------\r\n\r\n");
            StreamWriter sw;
            if (!File.Exists(fileFullPath))
            {
                sw = File.CreateText(fileFullPath);
            }
            else
            {
                sw = File.AppendText(fileFullPath);
            }
            sw.WriteLine(str.ToString());
            sw.Close();
        }
    }
}
