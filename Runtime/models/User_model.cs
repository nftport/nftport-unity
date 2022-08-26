using System;

namespace NFTPort.Internal
{
    [Serializable]
    public class User_model 
{
  
    public string response;
    public Profile profile;
    public SubscriptionPeriod subscription_period;
    public DataUsageAndLimits data_usage_and_limits;
    public MintingUsageAndLimits minting_usage_and_limits;
    
    [Serializable]
    public class ContractDeploymentLimits
    {
        public int subscription_contracts_included;
    }
    
    [Serializable]
    public class ContractDeploymentUsage
    {
        public int total_contracts_deployed;
        public int subscription_contracts_deployed;
    }
    
    [Serializable]
    public class DataLimits
    {
        public int subscription_data_requests_included;
        public int max_data_requests_per_second;
        public int max_data_requests_per_month;
    }
    
    [Serializable]
    public class DataUsage
    {
        public int subscription_data_requests_made;
    }

    [Serializable]
    public class DataUsageAndLimits
    {
        public DataUsage data_usage;
        public DataLimits data_limits;
    }

    [Serializable]
    public class MintingLimits
    {
        public int subscription_mints_included;
    }

    [Serializable]
    public class MintingUsage
    {
        public int total_nfts_minted;
        public int subscription_nfts_minted;
    }

    [Serializable]
    public class MintingUsageAndLimits
    {
        public Polygon polygon;
        public Rinkeby rinkeby;
    }

    [Serializable]
    public class Polygon
    {
        public MintingUsage minting_usage;
        public MintingLimits minting_limits;
        public ContractDeploymentUsage contract_deployment_usage;
        public ContractDeploymentLimits contract_deployment_limits;
    }

    [Serializable]
    public class Profile
    {
        public string name;
        public string email;
        public DateTime joined_date;
    }

    [Serializable]
    public class Rinkeby
    {
        public MintingUsage minting_usage;
        public MintingLimits minting_limits;
        public ContractDeploymentUsage contract_deployment_usage;
        public ContractDeploymentLimits contract_deployment_limits;
    }

    [Serializable]
    public class SubscriptionPeriod
    {
        public DateTime start_date;
        public DateTime end_date;
    }

}
}

