using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace NFTPort.Utils
{
    public static class DetermineFileType 
    {
        public static string File(string filePath)
        {
            string contentType = "";
            var fileExtension = Path.GetExtension(filePath).Replace(".","").ToLower();
            ////â‰§â— â€¿â— â‰¦âœŒ _sz_ Î //≧◠‿◠≦✌ _sz_ Ω
            Debug.Log(fileExtension);

            if (
                fileExtension == "png" ||
                fileExtension == "jpeg" ||
                fileExtension == "jpg" ||
                fileExtension == "gif" 
                )
            {
                contentType = "image/" + fileExtension;
            }
            
            else if (
                    fileExtension == "svg" 
            )
            {
                contentType = "image/svg+xml";
            }
            
            else if (
                    fileExtension == "ogg" ||
                    fileExtension == "aac" ||
                    fileExtension == "mp3" ||
                    fileExtension == "opus" ||
                    fileExtension == "wav"
                    )
            {
                contentType = "audio/" + fileExtension;
            }
            
            else if (
                    fileExtension == "mp4" ||
                    fileExtension == "mpeg" ||
                    fileExtension == "mov" ||
                    fileExtension == "flv" ||
                    fileExtension == "mov" ||
                    fileExtension == "wmv" ||
                    fileExtension == "webm" ||
                    fileExtension == "mkv"
                    )
            {
                contentType = "video/" + fileExtension;
            }
            else if (
                    fileExtension == "avi"
                )
            {
                contentType = "video/x-msvideo";
            }
            
            else if (
                    fileExtension == "txt"
            )
            {
                contentType = "text/plain";
            }
            
            else if (
                    fileExtension == "csv"
                    )
            {
                contentType = "text" + fileExtension;
            }
            
            else
            {
                contentType = "application/" + fileExtension;
            }

            return contentType;
        }
    }
}

