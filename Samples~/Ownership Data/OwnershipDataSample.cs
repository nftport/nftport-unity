namespace NFTPort.Samples.Ownership_Data{
    
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using NFTPort;
    using UnityEngine.UI;

    public class OwnershipDataSample : MonoBehaviour
    {
        
        //--------------------------------------------------------------//
        #region NFTsOwnedByAnAccount
        
        [SerializeField]
        NFTs_OwnedByAnAccount NfTsOwnedByAnAccount;

        [SerializeField] 
        private Dropdown chainDropdown;

        [SerializeField] private Text accountAddressText;
        [SerializeField] private Text contractFilter;
        [SerializeField] private Text outputWindow;
        
        public void NfTsOwnedByAnAccount_Run()
        {
            NfTsOwnedByAnAccount
                .SetChain(GetChainFromDropDownSelection())
                .SetAddress(accountAddressText.text)
                .SetFilterFromContract(contractFilter.text)
                .OnComplete(NFTs=> NFTsOwnedByAnAccount_OutputUI(NFTs))
                .Run();
        }

        void NFTsOwnedByAnAccount_OutputUI(NFTs_model NFTsOfUser)
        {
            outputWindow.text = "";
            
            //Populate NFT names in Output window
            foreach (var UserNFT in NFTsOfUser.nfts)
            {
                if (UserNFT.name == null)
                    UserNFT.name = "NFT Name not Set";
                
                //Populate UI
                outputWindow.text = outputWindow.text + UserNFT.name + "\n";

            }
            
            //Other ways to use
            //var x = NFTsOfUser.metadata.attributes[0].trait_type;

        }

        #region Chain Dropdown Address

        NFTs_OwnedByAnAccount.Chains GetChainFromDropDownSelection()
        {
            if (chainDropdown.value == 0)
                return NFTs_OwnedByAnAccount.Chains.ethereum;
            else if(chainDropdown.value == 1)
                return NFTs_OwnedByAnAccount.Chains.polygon;
            else 
                return NFTs_OwnedByAnAccount.Chains.rinkeby;
        }

        void PopulateChainDropDownList()
        {
            chainDropdown.options.Clear();
            string[] enumChains = Enum.GetNames(typeof(NFTs_OwnedByAnAccount.Chains));
            List<string> chainNames = new List<string>(enumChains);
            chainDropdown.AddOptions(chainNames);
        }

        #endregion

        #endregion
        
        //--------------------------------------------------------------//
        
        #region NFTs of a Contract

        [SerializeField]
        NFTs_OfAContract NFTsOfAContract;

        [SerializeField] private Dropdown chainDropdown_Contract;
        [SerializeField] private Text contractAddressText;

        public void NFTsOfAContract_Run()
        {
            NFTsOfAContract
                .SetChain(GetChainFromDropDownSelect_Contract())
                .SetContractAddress(contractAddressText.text)
                .OnComplete(NFTs=> NFTsAContract_OutputUI(NFTs))
                .Run();
        }

        void NFTsAContract_OutputUI(NFTs_model NFTsOfContract)
        {
            outputWindow.text = "";
            
            //Populate NFT names in Output window
            foreach (var ContractNFT  in NFTsOfContract.nfts)
            {
                if (ContractNFT.metadata.name == null)
                    ContractNFT.metadata.name = "NFT Name not Set";
                
                //Populate UI
                outputWindow.text = outputWindow.text + ContractNFT.metadata.name + "\n";

            }
            
            //Other ways to use
            //var x = ContractNFT.metadata.attributes[0].trait_type;

        }

        #region Chain DropDown contract
        NFTs_OfAContract.Chains GetChainFromDropDownSelect_Contract()
        {
            if (chainDropdown_Contract.value == 0)
                return NFTs_OfAContract.Chains.ethereum;
            else if(chainDropdown_Contract.value == 1)
                return NFTs_OfAContract.Chains.polygon;
            else 
                return NFTs_OfAContract.Chains.rinkeby;
        }

        void PopulateChainDropDownList_Contract()
        {
            chainDropdown_Contract.options.Clear();
            string[] enumChains = Enum.GetNames(typeof(NFTs_OfAContract.Chains));
            List<string> chainNames = new List<string>(enumChains);
            chainDropdown_Contract.AddOptions(chainNames);
        }
        

        #endregion
        
        #endregion

        private void Start()
        {
            PopulateChainDropDownList();
            PopulateChainDropDownList_Contract();
        }
        
    }

}