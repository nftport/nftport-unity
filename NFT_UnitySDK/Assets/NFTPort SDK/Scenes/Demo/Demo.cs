using UnityEngine;
using TMPro;
using NFTPort;
using UnityEngine.UI;
//www.embracingearth.space

public class Demo : MonoBehaviour
{
    #region defines
    [SerializeField] Text addressInput;
    [SerializeField] Text ContractInput;
    [SerializeField] private TextMeshProUGUI chaininput;
    [SerializeField] private TextMeshProUGUI NFTNames;
    
    [SerializeField] private Text Minturl;
    [SerializeField] private Text Mintto;
    [SerializeField] private Text MintName;
    
    
    [SerializeField] private NFTs_ownedbyanAccount _nfTsOwnedbyanAccount;
    [SerializeField] private NFTs_fromAContract _nfTsFromAContract;
    
    #endregion


    public void Go_AddressFetch()
    {
        //shows how to call using NFTPortInterfaceScript
        NFTPort_Interface.Instance.NFTsOfAccount(addressInput.text);
    }

    public void UpdateUI_AddressFetch()
    {
        NFTNames.text = "";
        //shows how to refrence the data model
        for (int i = 0; i < _nfTsOwnedbyanAccount._ownedbyAddreddModel.nfts.Count; i++)
        {
            NFTNames.text = NFTNames.text + '\n' +
                            _nfTsOwnedbyanAccount._ownedbyAddreddModel.nfts[i].name;
        }

    }


    public void Go_AContractFetch()
    {
        //shows how to call using NFTPortInterfaceScript
        NFTPort_Interface.Instance.NFTsFromContract(ContractInput.text);
    }

    public void UpdateUI_ContractFetch() 
    {
        NFTNames.text = "";
        //shows how to refrence the data model
        for (int i = 0; i < _nfTsFromAContract._fromContractModel.nfts.Count; i++)
        {
            NFTNames.text = NFTNames.text + '\n' +
                            _nfTsFromAContract._fromContractModel.nfts[i].token_id;
           
        }
    }

    public void Mint()
    {
        NFTPort_Interface.Instance.Mint(Minturl.text, Mintto.text, MintName.text);
    }

}