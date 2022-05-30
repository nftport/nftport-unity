using UnityEngine.Events;

namespace NFTPort.Samples
{

    using UnityEngine;
    using UnityEngine.UI;
    using NFTPort;
    

    public class CustomMintSample : MonoBehaviour
    {
        [SerializeField] private Mint_Custom _mintCustom;
        [SerializeField] private Text output;
        [SerializeField] private Text walleText;
        [SerializeField] private Text wallePanelText;
        [SerializeField] private GameObject Panel;
        
        [SerializeField] private Animator animator;
        
        [SerializeField] private Transform objectTransform;
        [SerializeField] private Transform targetTransform;

        [SerializeField] private Vector3 velocity;

        [SerializeField] private UnityEvent Aftermint;

        private bool inZone = false;
        private bool isMinted = false;
        
        private void Start()
        {
            Panel.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") == true)
            {
                Panel.SetActive(true);
                inZone = true;
            }
        }
 
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Player") == true)
            {
                Panel.SetActive(false);
                inZone = false;
            }
        }

        private void Update()
        {
            if (inZone && !isMinted)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    MintEet();
                }
            }
            
            if(isMinted)
            {
                objectTransform.position = Vector3.SmoothDamp(objectTransform.position, targetTransform.position, ref velocity, 0.5f, 10);
            }
        }

        void MintEet()
        {
            _mintCustom 
                    
                .OnError(error => OnMintError(error))
                .OnComplete(minted => OnMintComplete(minted))
                .SetParameters(
                    //We are referencing it and have already set other parameters on the component in editor.
                        MintToAddress: Port.ConnectedPlayerAddress //connected via NFTPort Player Connect WebGL build feature
                        )
                .Run();
        }

        void OnMintComplete(Minted_model minted)
        {
            output.text = "NFTPort | Mint Success (⌐■_■) : at: " + minted.transaction_external_url;
            AfterMint();
        }
        void OnMintError(string error)
        {
            output.text = error;
        }

        public void PlayerWalletConnected()
        {
            walleText.text = Port.ConnectedPlayerAddress;
            wallePanelText.text = Port.ConnectedPlayerAddress;
        }

        void AfterMint()
        {
            animator.speed = 5f;
            isMinted = true;
            Aftermint.Invoke();
        }
        
        
    }
}


