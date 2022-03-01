using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

//EXTRA Interface script for ease. 
//to call you can add using NFTPort on your script; and do NFTPort_Interface.NFT'sFromContract (contractStr , Chain) ; 
//by www.embracingearth.space
namespace NFTPort
{
    [RequireComponent(
        typeof(NFTs_ownedbyanAccount), 
        typeof(NFTs_fromAContract), 
        typeof(Mint_Url)

    )]
    public class NFTPort_Interface : Singleton<NFTPort_Interface>
    {
        private NFTs_ownedbyanAccount _nfTsOwnedbyanAccount;
        private NFTs_fromAContract _nfTsFromAContract;
        private Mint_Url _mintUrl;

        private void Awake()
        {
            _nfTsOwnedbyanAccount = GetComponent<NFTs_ownedbyanAccount>();
            _nfTsFromAContract = GetComponent<NFTs_fromAContract>();
            _mintUrl = GetComponent<Mint_Url>();
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

    
    }
}
