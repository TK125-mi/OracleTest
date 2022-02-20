using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleTest
{
    class OracleTest
    {
        static void Main(string[] args)
        {
            string connectString = GetSysConf("ConString");

            Console.Write(OraSelect(connectString));
        }

        public static string GetSysConf(string inputParamName)
        {
            // config ファイルを指定
            string configFile = @"OracleTest.config";
            ExeConfigurationFileMap exeFileMap = new ExeConfigurationFileMap { ExeConfigFilename = configFile };
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(exeFileMap, ConfigurationUserLevel.None);

            string paramValue = "";
            string paramName = inputParamName;
            // configファイルのパラメータ名を指定
            paramValue = config.AppSettings.Settings[paramName].Value;
            Console.WriteLine(paramValue);

            //すべてのキーとその値を取得
            foreach (string key in config.AppSettings.Settings.AllKeys)
            {
                Console.WriteLine("{0} : {1}",
                    key, config.AppSettings.Settings[key].Value);
            }
            return paramValue;
        }

        public static string OraSelect(string connectString)
        {
            string rs = "";
               

            using (OracleConnection conn = new OracleConnection(connectString))
            {
                try
                {
                    conn.Open();
                    rs = "データベースに接続しました。";

                    string sql = "SELECT * FROM TABLE1";
                    OracleCommand cmd = new OracleCommand(sql, conn);
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["ID"] + " : "
                                            + reader["NAME"] + " : "
                                            + reader["SUB"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    rs = "失敗しました。" + ex.ToString();
                }
            }
            return rs;
        }
    }
}
