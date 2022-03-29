using UnityEngine;

//EXTRA Interface script for ease. 
//to call you can add using NFTPort on your script; and do NFTPort_Interface.instance.NFT'sFromContract (contractStr , Chain) ; 
namespace NFTPort
{
    [RequireComponent(typeof(NFTs_ownedbyanAccount))]
    [RequireComponent(typeof(NFTs_fromAContract))]
    [RequireComponent(typeof(Mint_Url))]
    [RequireComponent(typeof(NFTDetails))]
    
    public class NFTPort_Interface : MonoBehaviour
    {
        #region 
        private static NFTPort_Interface _instance;
        public static NFTPort_Interface Instance { get { return _instance; } }
        
        private void InterfaceInstance()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
        }
        #endregion
      
        private NFTs_ownedbyanAccount _nfTsOwnedbyanAccount;
        private NFTs_fromAContract _nfTsFromAContract;
        private Mint_Url _mintUrl;
        private NFTDetails _nftDetails;
        private void Awake()
        {
            InterfaceInstance();
            _nfTsOwnedbyanAccount = GetComponent<NFTs_ownedbyanAccount>();
            _nfTsFromAContract = GetComponent<NFTs_fromAContract>();
            _mintUrl = GetComponent<Mint_Url>();
            _nftDetails = GetComponent<NFTDetails>();
        }

        public void NFTsFromContract(string contract)
        {
            _nfTsFromAContract.Contract = contract;
            _nfTsFromAContract.Run();
        }

        public void NFTsOfAccount(string address)
        {
            _nfTsOwnedbyanAccount.Address = address;
            _nfTsOwnedbyanAccount.Run();
        }

        public void Mint(string fileurl, string mintTOAddress, string name)
        {
            _mintUrl.fileurl = fileurl;
            _mintUrl.MintToAddress = mintTOAddress;
            _mintUrl.Name = name;
            _mintUrl.Run();
        }

        public void NFTDetails(string contract_address, string token_id)
        {
            _nftDetails.Contract = contract_address;
            _nftDetails.Token_id = token_id;
            _nftDetails.Run();
        }
        

    }
}
