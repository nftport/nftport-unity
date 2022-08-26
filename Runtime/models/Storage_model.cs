using System;
using System.Collections.Generic;

[Serializable]
public class Storage_model
{
    public string response;
    public List<Storage> storage;

    [Serializable]
    public class Storage
    {
        public string response;
        public string storage_type;
        public string file_name;
        public string metadata_uri;
        public string ipfs_uri;
        public string ipfs_url;
        public string uploaded_date;
        public string content_type;
        public int file_size;
        public double file_size_mb;
    }
}
