using System;

namespace NFTPort
{
    [Serializable]
    public class Txn_model 
    {
        public string response ;
        public Statistics statistics ;
        public Transactions[] transactions ;
        public string continuation ;
    }
    
    [Serializable]
    public class Statistics
    {
        public double one_day_volume ;
        public double one_day_change ;
        public int one_day_sales ;
        public double one_day_average_price ;
        public double seven_day_volume ;
        public double seven_day_change ;
        public int seven_day_sales ;
        public double seven_day_average_price ;
        public double thirty_day_volume ;
        public double thirty_day_change ;
        public int thirty_day_sales ;
        public double thirty_day_average_price ;
        public double total_volume ;
        public int total_sales ;
        public int total_supply ;
        public int total_minted ;
        public int num_owners ;
        public double average_price ;
        public double market_cap ;
        public double floor_price ;
        public string updated_date ;
    }

    [Serializable]
    public class Transactions
    {
        public string type ;
        public string transfer_from ;
        public string transfer_to ;
        public string contract_address ;
        public string mint_address;
        public string token_id ;
        public int quantity ;
        public string transaction_signature;
        public string transaction_hash ;
        public string block_hash ;
        public string slot_hash;
        public string slot_number;
        public int block_number ;
        public string transaction_date ;
        public string lister_address ;
        public Nft nft ;
        public ListingDetails listing_details ;
        public string marketplace ;
        public string buyer_address ;
        public string seller_address ;
        public PriceDetails price_details ;
    }
    
    [Serializable]
    public class Creator
    {
        public string account_address ;
        public string creator_share ;
        public string address;
        public object share;
        public bool verified;
    }

    [Serializable]
    public class ListingDetails
    {
        public string asset_type ;
        public string contract_address ;
        public string price ;
        public double price_usd ;
    }

    [Serializable]
    public class Royalty
    {
        public string account_address ;
        public string royalty_share ;
    }

    [Serializable]
    public class PriceDetails
    {
        public string asset_type ;
        public string contract_address ;
        public string price ;
        public double price_usd ;
    }

}
