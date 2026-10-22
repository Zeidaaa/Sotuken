#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

public class EquipmentEditorValidator : AssetPostprocessor
{
    [MenuItem("Tools/装備マスターデータのID重複チェックを実行")]
    public static void CheckAllEquipmentIdsManual()
    {
        // デバッグログのクリア処理
        Assembly assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        System.Type logEntriesType = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo clearMethod = logEntriesType.GetMethod("Clear");
        clearMethod.Invoke(null, null);

        // 重複チェック
        RunValidation(true);
    }

    private static void RunValidation(bool showSuccessLog)
    {
        string[] guids = AssetDatabase.FindAssets("t:EquipmentMasterData");

        Dictionary<int, Dictionary<int, List<EquipmentMasterData>>> categoryMap =
            new Dictionary<int, Dictionary<int, List<EquipmentMasterData>>>();

        int totalCount = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            EquipmentMasterData asset = AssetDatabase.LoadAssetAtPath<EquipmentMasterData>(path);

            if (asset == null) continue;

            totalCount++;
            int prefix = asset.CategoryPrefix;
            int itemID = asset.EquipmentID % EquipmentMasterData.CATEGORYCOLUMN;

            if (!categoryMap.ContainsKey(prefix))
            {
                categoryMap[prefix] = new Dictionary<int, List<EquipmentMasterData>>();
            }

            if (!categoryMap[prefix].ContainsKey(itemID))
            {
                categoryMap[prefix][itemID] = new List<EquipmentMasterData>();
            }

            categoryMap[prefix][itemID].Add(asset);
        }

        bool hasError = false;

        foreach (var categoryPair in categoryMap)
        {
            int prefix = categoryPair.Key;
            foreach (var idPair in categoryPair.Value)
            {
                int itemID = idPair.Key;
                List<EquipmentMasterData> assetsWithSameId = idPair.Value;

                if (assetsWithSameId.Count > 1)
                {
                    hasError = true;
                    int fullId = (prefix * EquipmentMasterData.CATEGORYCOLUMN) + itemID;

                    string assetNames = "";
                    foreach (var a in assetsWithSameId)
                    {
                        assetNames += $"[{a.name}] ";
                    }

                    Debug.LogError($"[ID重複エラー] ID: {fullId} (カテゴリ: {prefix}, アイテムID: {itemID}) が以下の複数アセットで重複しています！ -> {assetNames}");
                }
            }
        }

        if (!hasError && showSuccessLog)
        {
            Debug.Log($"<color=green>[IDチェック成功] 検査対象 {totalCount} 件の装備マスターデータにIDの重複はありません。</color>");
        }
    }
}
#endif