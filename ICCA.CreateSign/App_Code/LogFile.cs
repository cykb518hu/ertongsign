using System;
using System.IO;

  /// <summary>
    /// Defines methods for writing log of file type.
    /// </summary>
    public class LogFile : LogBasic
    {
        /// <summary>
        /// Initializes a new instance of the LogFile class.
        /// </summary>
        public LogFile()
        {

        }

        /// <summary>
        /// The path of the log file.
        /// </summary>
        private string m_LogFilePath;

        /// <summary>
        /// Gets or sets the path of the log file.
        /// </summary>
        /// <value>The path of the log file.</value>
        public string M_LogFilePath
        {
            get
            {
                return m_LogFilePath;
            }
            set
            {
                m_LogFilePath = value;
            }
        }

        /// <summary>
        /// The name of the log file.
        /// </summary>
        private string m_LogFileName;

        /// <summary>
        /// Gets or sets the name of the log file.
        /// </summary>
        /// <value> The name of the log file.</value>
        public string M_LogFileName
        {
            get
            {
                return m_LogFileName;
            }
            set
            {
                m_LogFileName = value;
            }
        }

        /// <summary>
        /// The keep time of the log file.
        /// </summary>
        private int m_LogFileKeepTime;

        /// <summary>
        /// Gets or sets the keep time of the log file.
        /// </summary>
        /// <value>The keep time of the log file.</value>
        public int M_LogFileKeepTime
        {
            get
            {
                return m_LogFileKeepTime;
            }
            set
            {
                m_LogFileKeepTime = value;
            }
        }

        /// <summary>
        /// Write a log message.
        /// </summary>
        /// <param name="logInfo">The message that describes the log.</param>
        protected override void WriteLog(LogInfo logInfo)
        {
            // Create the subfolder
            if (!Directory.Exists(M_LogFilePath))
            {
                Directory.CreateDirectory(M_LogFilePath);
            }

            // Clear Old Log File.
            ClearOldLogFile(M_LogFilePath, M_LogFileKeepTime);

            // Generate log file's name.
            string strLogFileFullPath = Path.Combine(M_LogFilePath, M_LogFileName);
            StreamWriter swLogFile;

            // Determine whether the file is existed.
            if (File.Exists(strLogFileFullPath))
            {
                swLogFile = new StreamWriter(strLogFileFullPath, true);
            }
            else
            {
                swLogFile = File.CreateText(strLogFileFullPath);
            }

            // Record log
            swLogFile.WriteLine(logInfo.ToString());

            // Close log file
            swLogFile.Close();
        }

        ///==========删除指定天数前的日志文件=====================================
        /// 函 数 名：ClearOldLogFile
        /// 功能描述：删除指定天数前的日志文件
        /// 输入参数：strLogFilePath:日志文件路径 iLogFileKeepTime:保留天数
        /// 输出参数：void
        /// 创建日期：2014-05-10
        /// 修改日期：2014-05-10
        /// 作    者：William Xie
        /// 附加说明：
        ///=====================================================================
        public void ClearOldLogFile(string strLogFilePath, int iLogFileKeepTime)
        {
            if ("\\" != strLogFilePath.Substring(strLogFilePath.Length - 1, 1))
            {
                strLogFilePath = strLogFilePath.Trim() + "\\";
            }

            string[] LogFileList = Directory.GetFiles(Path.GetDirectoryName(strLogFilePath), "*.log");
            foreach(string strLogFileName in LogFileList)
            {
                string strLogFileDate = Path.GetFileName(strLogFileName).Substring(0,10);
                DateTime dtLogFileDate = DateTime.Parse(strLogFileDate);

                int iTemp = DateTime.Now.Subtract(dtLogFileDate).Days;

                if (iTemp > iLogFileKeepTime)
                {
                    //判断文件是不是存在
                    if (File.Exists(strLogFileName))
                    {
                        //如果存在则删除
                        File.Delete(strLogFileName);
                    }
                }
            }
        }
    }
