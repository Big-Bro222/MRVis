#if UNITY_EDITOR
using System.Diagnostics;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Synchro
{
    class SynchroPreBuild : IPreprocessBuildWithReport
    {
        public int callbackOrder
        {
            get { return 0; }
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            string assetFolder = "/Asset";
            string path = Application.dataPath;
            int pos = path.IndexOf(assetFolder);
            
            if (pos !=-1)
                path = path.Substring(0, pos);
            Process ExternalProcess = new Process();
            ExternalProcess.StartInfo.FileName = path+"/Tools/MessagePack.bat";
            Debug.Log("Build Synchro serialize"+ExternalProcess.StartInfo.FileName);
            ExternalProcess.Start();
            ExternalProcess.WaitForExit();        }
    }
}
#endif
