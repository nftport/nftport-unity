using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace NFTPort.Utils
{
    public static class FormMaker
    {
        public class Params
        {
            public byte[] body;
            public string contentType;
        }
        static Params _params = new Params();
        public static Params Formeet(string filePath)
        {
            ////â‰§â— â€¿â— â‰¦âœŒ _sz_ Î //≧◠‿◠≦✌ _sz_ Ω
            var filetype = DetermineFileType.File(filePath);
            
            List<IMultipartFormSection> form= new List<IMultipartFormSection>
            {
                new MultipartFormFileSection("file", System.IO.File.ReadAllBytes(filePath), Path.GetFileName(filePath), filetype)
            };
            // generate a boundary then convert the form to byte[]
            byte[] boundary = UnityWebRequest.GenerateBoundary();
            byte[] formSections = UnityWebRequest.SerializeFormSections(form, boundary);
            // termination string consisting of CRLF--{boundary}--
            byte[] terminate = Encoding.UTF8.GetBytes(String.Concat("\r\n--", Encoding.UTF8.GetString(boundary), "--"));
            // Make complete body from the two byte arrays
            _params.body = new byte[formSections.Length + terminate.Length];
            Buffer.BlockCopy(formSections, 0, _params.body, 0, formSections.Length);
            Buffer.BlockCopy(terminate, 0, _params.body, formSections.Length, terminate.Length);
            // Set the content type - NO QUOTES around the boundary
            _params.contentType = String.Concat("multipart/form-data; boundary=", Encoding.UTF8.GetString(boundary));
            return _params;
            
        }
    }
}

