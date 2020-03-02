using NetMQ;
using UnityEngine;
using UnityEditor;
using Utils;

namespace Synchro
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public class SingleEntryPoint
    {
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod()
        {
            Debug.Log("Setting NetMQ config");
            AsyncIO.ForceDotNet.Force();
            NetMQConfig.Cleanup();
        }
        
        
        static SingleEntryPoint()
        {
            Debug.Log("Registering OnPlayModeChanged to unload NetMQ properly");

#if UNITY_EDITOR
            EditorPlayMode.PlayModeChanged += OnPlayModeChanged;
#endif
        }
     
#if UNITY_EDITOR
        private static void OnPlayModeChanged(PlayModeState currentMode, PlayModeState changedMode)
        {
            if (currentMode == PlayModeState.Playing && changedMode == PlayModeState.AboutToStop)
            {
                NetMQConfig.Cleanup(false);
            }
        }
#endif
    }
}