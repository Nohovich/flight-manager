using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main_Project
{
    public static class FlyingCenterConfig
    {
        public static int _24hours = 1000 * 60 * 60 * 24;
        /// <summary>
        /// test data best
        /// </summary>
        public static bool testMode = false;
        /// <summary>
        /// working data base
        /// </summary>
        public static string ConString { 
            get
            {
                if(testMode == true)
                    return @"data source=DESKTOP-BRBTNVK\SQLEXPRESS; initial catalog= Main project test; integrated security = true";
                return @"data source=DESKTOP-BRBTNVK\SQLEXPRESS; initial catalog= Main project; integrated security = true";
            }
        }
    }
}
