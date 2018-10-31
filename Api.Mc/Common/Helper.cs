using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using Common;
namespace Api.Mc.Common
{
    /// <summary>
    /// Helper
    /// </summary>
    public static class Helper
    {
        private static string _me24ConnectionString = string.Empty;
        private static OracleConnection _me24OracleConnection = null;

        private static string _OraDCConnectionString = string.Empty;
        private static OracleConnection _OraDCOracleConnection = null;

        private static string _OraPtemsConnectionString = string.Empty;
        private static OracleConnection _OraPtemsOracleConnection = null;

        private static string _schemaName = string.Empty;

        /// <summary>
        /// SchemaName
        /// </summary>
        public static string SchemaName
        {
            get 
            {
                if (string.IsNullOrEmpty(_schemaName))
                    _schemaName = ConfigurationManager.AppSettings["SCHEMA_NAME"];
                return _schemaName; 
            }
            set { _schemaName = value; }
        } 
               
        /// <summary>
        /// ME24ConnectionString
        /// </summary>
        public static string ME24ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_me24ConnectionString))
                    _me24ConnectionString = ConfigurationManager.ConnectionStrings["ME24_CONNECTION_STRING"].ConnectionString;
                return _me24ConnectionString;
            }
            set { _me24ConnectionString = value; }
        }

        /// <summary>
        /// ME24OracleConnection
        /// </summary>
        public static OracleConnection ME24OracleConnection
        {
            get
            {
                if (_me24OracleConnection == null)
                    _me24OracleConnection = new OracleConnection(ME24ConnectionString);
                if (_me24OracleConnection.State == System.Data.ConnectionState.Closed)
                    _me24OracleConnection.Open();
                return _me24OracleConnection;
            }
            set 
            { _me24OracleConnection = value; }
        }
        public static string OraDCConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_OraDCConnectionString))
                    _OraDCConnectionString = ConfigurationManager.ConnectionStrings["ORA_CONNECTION_STRING_DC"].ConnectionString;
                return _OraDCConnectionString;
            }
            set { _OraDCConnectionString = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        ////public static OracleConnection OraDCOracleConnection
        ////{
        ////    get
        ////    {
        ////        if (_OraDCOracleConnection == null)
        ////            _OraDCOracleConnection = new OracleConnection(OraDCConnectionString);
        ////        if (_OraDCOracleConnection.State == System.Data.ConnectionState.Closed)
        ////            _OraDCOracleConnection.Open();
        ////        return _OraDCOracleConnection;
        ////    }
        ////    set
        ////    { _OraDCOracleConnection = value; }
        ////}

        public static OracleConnection OraDCOracleConnection
        {
            get
            {
                if (_OraDCOracleConnection == null)
                    _OraDCOracleConnection = new OracleConnection(OraDCConnectionString);
                if (_OraDCOracleConnection.State != ConnectionState.Open)
                {
                    try
                    {
                        _OraDCOracleConnection.Dispose();
                    }
                    catch
                    {
                    }

                    _OraDCOracleConnection = new OracleConnection(OraDCConnectionString);

                    _OraDCOracleConnection.Open();
                }
                return _OraDCOracleConnection;
            }
        }
        public static string OraPtemsConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_OraPtemsConnectionString))
                    _OraPtemsConnectionString = ConfigurationManager.ConnectionStrings["ORA_CONNECTION_STRING_PTEMS"].ConnectionString;
                return _OraPtemsConnectionString;
            }
            set { _me24ConnectionString = value; }
        }

        /// <summary>
        /// ME24OracleConnection
        /// </summary>
        public static OracleConnection OraPtemsOracleConnection
        {
            get
            {
                if (_OraPtemsOracleConnection == null)
                    _OraPtemsOracleConnection = new OracleConnection(OraPtemsConnectionString);
                if (_OraPtemsOracleConnection.State == System.Data.ConnectionState.Closed)
                    _OraPtemsOracleConnection.Open();
                return _OraPtemsOracleConnection;
            }
            set
            { _me24OracleConnection = value; }
        }
        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery (OracleCommand dbCommand)
        {
            int iResult = -1;
            try
            {
                OracleConnection connection = ME24OracleConnection;
                dbCommand.Connection = connection;

                iResult =  dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "ExecuteNonQuery: " + ex.Message);
            }
            return iResult;
        }

        /// <summary>
        /// ExecuteDataReader
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public static OracleDataReader ExecuteDataReader(OracleCommand dbCommand, OracleConnection oraConnection)
        {
            try
            {
                OracleConnection connection = oraConnection;

                dbCommand.Connection = connection;
                return dbCommand.ExecuteReader();
            }
            catch (Exception exception)
            {
                LogAPI.LogToFile(LogFileType.EXCEPTION, "ExecuteDataReader: " + exception.Message);
            }
            return null;
        }
    }
}