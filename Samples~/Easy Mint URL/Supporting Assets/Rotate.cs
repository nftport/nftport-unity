namespace NFTPort.Samples.EasyMint_URL
{
    using UnityEngine;

    public class Rotate : MonoBehaviour
    {

        public float xspeed = 0.0f;
        public float yspeed = 0.0f;
        public float zspeed = 0.0f;
        
        void Update()
        {

            transform.Rotate(
                xspeed * Time.deltaTime,
                yspeed * Time.deltaTime,
                zspeed * Time.deltaTime
            );


        }
    }
}