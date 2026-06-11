using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTier;
using System.Data;
using System.Data.SqlClient;


    public class clsUtility
    {

        public static string ProjectName { get { return "MPREMIUM"; } }
        public static string ProjectAbbreviation { get { return "MPREMIUM"; } }
        public static string ProjectWebsite { get { return "https://mpremium.in/"; } }
        public static string Company { get { return "Maniraya"; } }
        public static string Session { get { return "2026-27"; } }

        public static string Day { get { return DateTime.Now.ToString("ddd, MMM dd, yyyy"); } }

    }

