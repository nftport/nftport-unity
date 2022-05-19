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

            if (
                fileExtension == "png" ||
                fileExtension == "jpeg" ||
                fileExtension == "jpg" ||
                fileExtension == "gif" ||
                fileExtension == "bmp" ||
                fileExtension == "tiff" ||
                fileExtension == "aces" 
                
                )
            {
                contentType = "image/" + fileExtension;
            }
            
            else if (
                fileExtension == "glb" ||
                fileExtension == "gltf"
            )
            {
                contentType = "model/gltf-binary";
            }
            
            else if (
                fileExtension == "obj" ||
                fileExtension == "mtl" ||
                fileExtension == "step" ||
                fileExtension == "stl" 
            )
            {
                contentType = "model/" + fileExtension;
            }
            
            else if (
                fileExtension == "fbx"
            )
            {
                contentType = "application/octet-stream";
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
                    fileExtension == "csv" ||
                    fileExtension == "calendar" ||
                    fileExtension == "css" ||
                    fileExtension == "html" ||
                    fileExtension == "javascript" ||
                    fileExtension == "markdown" ||
                    fileExtension == "xml" 
                    )
            {
                contentType = "text/" + fileExtension;
            }
            
            else
            {
                contentType = "application/" + fileExtension;
            }

            return contentType;
        }
    }
}

