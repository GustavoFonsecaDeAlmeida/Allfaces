﻿using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using System;

namespace RedeSocial.WEB.Service
{
    public static class ConnectionString
    {
        static string account = CloudConfigurationManager.GetSetting("BlobStorage");
        
        public static CloudStorageAccount GetConnectionString()
        {
            string connectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", account);
            return CloudStorageAccount.Parse(connectionString);
        }
    }
}