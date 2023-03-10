#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExportSetting : MonoBehaviour
{
    [MenuItem("Assets/ExportWithSettings")]
    static void Export()
    {
        string[] assetPaths = {
            //"ProjectSettings/AudioManager.asset",
            //"ProjectSettings/ClusterInputManager.asset",
            //"ProjectSettings/DynamicsManager.asset",
            //"ProjectSettings/EditorBuildSettings.asset",
            //"ProjectSettings/EditorSettings.asset",
            //"ProjectSettings/GraphicsSettings.asset",
            //"ProjectSettings/InputManager.asset",
            //"ProjectSettings/NavMeshAreas.asset",
            //"ProjectSettings/NetworkManager.asset",
            //"ProjectSettings/Physics2DSettings.asset",
            "ProjectSettings/ProjectSettings.asset",
            //"ProjectSettings/QualitySettings.asset",
            "ProjectSettings/TagManager.asset"
            //"ProjectSettings/TimeManager.asset",
            //"ProjectSettings/UnityConnectSettings.asset"
        };

        string exportPath = "Settings.unitypackage";

        AssetDatabase.ExportPackage(assetPaths, exportPath, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse);

    }
}

#endif