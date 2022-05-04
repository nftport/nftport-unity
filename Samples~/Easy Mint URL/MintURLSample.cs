namespace NFTPort.Samples.EasyMint_URL{
    
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using NFTPort;
    using UnityEngine.UI;

    public class MintURLSample : MonoBehaviour
    {

        [SerializeField]
        Mint_URL mintURL;

        [SerializeField] 
        private Dropdown chainDropdown;

        [SerializeField] private Text fileUrlOfTheNFT;
        [SerializeField] private Text accountAddressToMintTo;
        [SerializeField] private Text nameOftheNFT;
        [SerializeField] private Text descriptionOfTheNFT;
        [SerializeField] private Text outputWindow;

        public Minted_model minted;
        
        public void Mint_Run()
        {
            mintURL
                .SetParameters(
                    FileURL: fileUrlOfTheNFT.text,
                    Name: nameOftheNFT.text,
                    Description: descriptionOfTheNFT.text,
                    MintToAddress: accountAddressToMintTo.text
                )
                .SetChain(GetChainFromDropDownSelection())
                .OnError(error=>Debug.Log(error))
                .OnComplete(Minted=> SuccesfullMint(Minted))
                .Run();

        }

        void SuccesfullMint(Minted_model _minted)
        {
            minted = _minted;

            outputWindow.text = "";
            outputWindow.text = minted.transaction_external_url.ToString();
        }
        

        #region Chain Dropdown

        Mint_URL.Chains GetChainFromDropDownSelection()
        {
            if (chainDropdown.value == 0)
                return Mint_URL.Chains.polygon;
            else 
                return Mint_URL.Chains.rinkeby;
        }

        void PopulateChainDropDownList()
        {
            chainDropdown.options.Clear();
            string[] enumChains = Enum.GetNames(typeof(Mint_URL.Chains));
            List<string> chainNames = new List<string>(enumChains);
            chainDropdown.AddOptions(chainNames);
        }

        #endregion
        
        private void Start()
        {
            PopulateChainDropDownList();
        }

        
        
    }

}