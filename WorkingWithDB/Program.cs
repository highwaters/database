using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingWithDB
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
             #if DEBUG == false
             String dbPathMyDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
             String dbPathAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
             String dbPath =  System.IO.Path.Combine(dbPathMyDocs, "LocalAppData");
             AppDomain.CurrentDomain.SetData("DataDirectory", dbPath);
            #endif
             Application.EnableVisualStyles();
             Application.SetCompatibleTextRenderingDefault(false);
             Application.Run(new Form3());
        }
       
    }
}
