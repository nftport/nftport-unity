using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NFTPort.Editor
{
    ////â‰§â— â€¿â— â‰¦âœŒ _sz_ Î© //≧◠‿◠≦✌ _sz_ Ω
    public static class PkgInfo
    {
        public static string GetInstalledPackageVer()
        {
            string path = "Packages/com.nftport.nftport/package.json";
            if (File.Exists(path))
            {
                var targetFile = File.ReadAllText(path);

                if (targetFile != null)
                {
                    PkgJson pkgJson = JsonConvert.DeserializeObject<PkgJson>(targetFile);
                    return pkgJson.version;
                }
            }
            return String.Empty;
        }
    }
}
