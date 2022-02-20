using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ModIO;
using Newtonsoft.Json;
using SimpleSpriteAnimator;
using UnityEngine;

public class GameModLoader : MonoBehaviour
{

    public List<string> modDirectories = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        ModManager.onModBinaryInstalled += ModInstalled;
        ModManager.onModBinariesUninstalled += ModUninstalled;
        
    }

    private void ModUninstalled(ModfileIdPair[] pairs)
    {
        RefreshResources();
    }

    private void ModInstalled(ModfileIdPair pair)
    {
        RefreshResources();
    }

    public void RefreshResources()
    {
        ModManager.QueryInstalledMods(null, mods =>
        {
            foreach (var pair in mods)
            {
                var idPair = pair.Key;
                ModManager.GetModProfile(idPair.modId, profile =>
                {
                    if (profile.tagNames.ToList().Contains("Bundle"))
                    {
                        modDirectories.Add(ModManager.GetModInstallDirectory(idPair.modId, idPair.modfileId));
                    }
                }, null);
            }
        });
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}