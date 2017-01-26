using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOIV_WPF.Storage
{
    class StorageUtility
    {
        private static StorageUtility INSTANCE;
        public static StorageUtility getInstance()
        {
            if(INSTANCE == null)
            {
                INSTANCE = new StorageUtility();
            }

            return INSTANCE;
        }

        private String importedFolderPath;

        public StorageUtility()
        {

        }

        public String ImportFolderPath
        {
            get
            {
                return importedFolderPath;
            }

            set
            {
                importedFolderPath = value;
            }
        }
    }
}
