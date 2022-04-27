using System;


    public static class LogUtil
    {
        /// <summary>
        /// The name of the log file that record debug message.
        /// </summary>
        private const string LOG_DEBUG = "debug.log";
        /// <summary>
        /// The name of the log file that record common message.
        /// </summary>
        private const string LOG_COMMON = "common.log";
        /// <summary>
        /// The name of the log file that record warning message.
        /// </summary>
        private const string LOG_WARNING = "warning.log";
        /// <summary>
        /// The name of the log file that record error message.
        /// </summary>
        private const string LOG_ERROR = "error.log";

        /// <summary>
        /// The lock of the debug log file.
        /// </summary>
        private static Object m_ObjLockDebugLog = new Object();
        /// <summary>
        /// The lock of the common log file.
        /// </summary>
        private static Object m_ObjLockCommonLog = new Object();
        /// <summary>
        /// The lock of the warning log file.
        /// </summary>
        private static Object m_ObjLockWarningLog = new Object();
        /// <summary>
        /// The lock of the error log file.
        /// </summary>
        private static Object m_ObjLockErrorLog = new Object();

        /// <summary>
        /// The flag of whether the LogUtil class is initiated.
        /// </summary>
        private static bool m_IsInit = false;

        /// <summary>
        /// The path of the log file.
        /// </summary>
        private static string m_LogFilePath;
        /// <summary>
        /// Gets a value the path of the log file.
        /// </summary>
        /// <value>The path of the log file.</value>
        public static string M_LogFilePath
        {
            get
            {
                return m_LogFilePath;
            }
        }

        /// <summary>
        /// The path of the log file.
        /// </summary>
        private static string m_LogLevel;
        /// <summary>
        /// Gets a value the level of the log file.
        /// </summary>
        /// <value>The level of the log file. debug common warning error</value>
        public static string M_LogLevel
        {
            get
            {
                return m_LogLevel;
            }
        }

        /// <summary>
        /// The keep time of the log file.
        /// </summary>
        private static int m_LogKeepTime;
        /// <summary>
        /// Gets a value the keep time of the log file.
        /// </summary>
        /// <value>The keep time of the log file.</value>
        public static int M_LogKeepTime
        {
            get
            {
                return m_LogKeepTime;
            }
        }

        /// <summary>
        /// Initiates the LogUtil class.
        /// </summary>
        /// <param name="strLogFilePath">The path of the log file.</param>
        /// <param name="_logBackupFolder">The name of the folder for backup log file.</param>
        /// <param name="_logSize">The maximum size of the log file.</param>
        /// <param name="iLogKeepTime">The keep time of the log file.</param>
        public static void Initialize(string strLogFilePath, string strLogLevel, int iLogKeepTime)
        {
            // Determine whether the LogUtil class is Initiated.
            if (m_IsInit)
            {
                return;
            }

            // Initiates the LogUtil class.
            m_LogFilePath = strLogFilePath;
            m_LogLevel = strLogLevel;
            m_LogKeepTime = iLogKeepTime;

            // Sets the flag of initiation.
            m_IsInit = true;
        }

        /// <summary>
        /// Write a debug type's log message.
        /// </summary>
        /// <param name="strLogMessage">The message that describes the log.</param>
        public static void DebugLog(string strLogMessage)
        {
            // Determine whether the LogUtil class is Initiated.
            if (!m_IsInit)
            {
                throw new Exception("³õÊ¼»¯Ê§°Ü£¡");
            }

            // Sets log's information
            LogInfo logInfo = new LogInfo();

            logInfo.M_LogDateTime = DateTime.Now;
            logInfo.M_LogMessage = strLogMessage;

            lock (m_ObjLockDebugLog)
            {
                LogFile lfDebugLog = new LogFile();
                lfDebugLog.M_LogFilePath = m_LogFilePath;
                lfDebugLog.M_LogFileName = string.Format("{0}-{1}", DateTime.Now.ToString("yyyy-MM-dd"), LOG_DEBUG);
                lfDebugLog.M_LogFileKeepTime = m_LogKeepTime;
                lfDebugLog.DebugLog(logInfo);
            }
        }

        /// <summary>
        /// Write a common type's log message.
        /// </summary>
        /// <param name="strLogMessage">The message that describes the log.</param>
        public static void CommonLog(string strLogMessage)
        {
            // Determine whether the LogUtil class is Initiated.
            if (!m_IsInit)
            {
                throw new Exception("³õÊ¼»¯Ê§°Ü£¡");
            }

            // Sets log's information
            LogInfo logInfo = new LogInfo();

            logInfo.M_LogDateTime = DateTime.Now;
            logInfo.M_LogMessage = strLogMessage;

            lock (m_ObjLockCommonLog)
            {
                LogFile lfCommonLog = new LogFile();
                lfCommonLog.M_LogFilePath = m_LogFilePath;
                lfCommonLog.M_LogFileName = string.Format("{0}-{1}", DateTime.Now.ToString("yyyy-MM-dd"), LOG_COMMON);
                lfCommonLog.M_LogFileKeepTime = m_LogKeepTime;
                lfCommonLog.CommonLog(logInfo);
            }

        }

        /// <summary>
        /// Write a warning type's log message.
        /// </summary>
        /// <param name="strLogMessage">The message that describes the log.</param>
        public static void WarningLog(string strLogMessage)
        {
            // Determine whether the LogUtil class is Initiated.
            if (!m_IsInit)
            {
                throw new Exception("³õÊ¼»¯Ê§°Ü£¡");
            }

            // Sets log's information
            LogInfo logInfo = new LogInfo();

            logInfo.M_LogDateTime = DateTime.Now;
            logInfo.M_LogMessage = strLogMessage;


            lock (m_ObjLockWarningLog)
            {
                LogFile lfWarningLog = new LogFile();
                lfWarningLog.M_LogFilePath = m_LogFilePath;
                lfWarningLog.M_LogFileName = string.Format("{0}-{1}", DateTime.Now.ToString("yyyy-MM-dd"), LOG_WARNING);
                lfWarningLog.M_LogFileKeepTime = m_LogKeepTime;
                lfWarningLog.WarningLog(logInfo);
            }

        }

        /// <summary>
        /// Write a error type's log message.
        /// </summary>
        /// <param name="strLogMessage">The message that describes the log.</param>
        public static void ErrorLog(string strLogMessage)
        {
            // Determine whether the LogUtil class is Initiated.
            if (!m_IsInit)
            {
                throw new Exception("³õÊ¼»¯Ê§°Ü£¡");
            }

            // Sets log's information
            LogInfo logInfo = new LogInfo();

            logInfo.M_LogDateTime = DateTime.Now;
            logInfo.M_LogMessage = strLogMessage;

            // Write error.log
            lock (m_ObjLockErrorLog)
            {
                LogFile lfErrorLog = new LogFile();
                lfErrorLog.M_LogFilePath = m_LogFilePath;
                lfErrorLog.M_LogFileName = string.Format("{0}-{1}", DateTime.Now.ToString("yyyy-MM-dd"), LOG_ERROR);
                lfErrorLog.M_LogFileKeepTime = m_LogKeepTime;
                lfErrorLog.ErrorLog(logInfo);
            }
        }
    }
