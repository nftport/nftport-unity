namespace NFTPort.Samples.Ownership_Data{
    
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using NFTPort;
    using UnityEngine.UI;

    public class OwnershipDataSample : MonoBehaviour
    {
    
        #region NFTsOwnedByAnAccount
        
        [SerializeField]
        NFTs_OwnedByAnAccount NfTsOwnedByAnAccount;

        [SerializeField] 
        private Dropdown chainDropdown;

        [SerializeField] private Text accountAddressTest;
        [SerializeField] private Text contractFilter;
        [SerializeField] private Text outputWindow;
        
        public void NfTsOwnedByAnAccount_Run()
        {
            NfTsOwnedByAnAccount
                .SetChain(GetChainFromDropDownSelection())
                .SetAddress(accountAddressTest.text)
                .SetFilterFromContract(contractFilter.text)
                .OnComplete(NFTs=> NFTsOwnedByAnAccount_OutputUI(NFTs))
                .Run();
        }

        void NFTsOwnedByAnAccount_OutputUI(NFTs_OwnedByAnAccount_model.Root NFTsOfUser)
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
        

        #endregion

        private void Start()
        {
            PopulateChainDropDownList();
        }

        #region chainDropDown

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
        
        
    }

}