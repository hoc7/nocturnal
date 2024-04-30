using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
#endif
namespace BoneGame.System
{
    public class MasterDataScriptableObject:SerializedScriptableObject
    {
        public int Id;
        public string Name;

        
#if UNITY_EDITOR
      
        [GUIColor(1, 1, 0)]
        [PropertyOrder(1000)]
        [Button("ファイル名更新", ButtonSizes.Large)]
        public virtual void Rename()
        {
            string assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());

            string newName = this.Id + "_" + this.Name;

            AssetDatabase.RenameAsset(assetPath, newName);
            AssetDatabase.SaveAssets();
        }
        
        /// <summary>
        /// マスターの検索
        /// </summary>
        /// <returns></returns>
        public static List<T> FindMaster<T>()
        {
            var typeName = typeof(T);
            StringBuilder builder = new StringBuilder();
            builder.Append("t:");
            builder.Append(typeName);
            var guids = UnityEditor.AssetDatabase.FindAssets(builder.ToString());

            List<T> masters = new List<T>();
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var objects = AssetDatabase.LoadAllAssetsAtPath(path).ToList();
                masters.AddRange(objects.OfType<T>().ToList());
            }

            return masters;
        }

        protected static List<T> SearchExistMaster<T>() where T : MasterDataScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T)}");
            var master = guids.Select(g => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(g))).ToList();
            return master;
        }
        
        [Button("新しいマスターの作成")]
        public static T CreateNewMasterAsset<T>() where T : MasterDataScriptableObject
        {
            int maxId = SearchExistMaster<T>().Select(_ => _.Id).Max();
            
            // 新しいインスタンスを作成
            T newObj = ScriptableObject.CreateInstance<T>();

            // ID を設定
            newObj.Id = maxId + 1;
            string masterName = typeof(T).Name;

            // ScriptableObject アセットをファイルに保存
            AssetDatabase.CreateAsset(newObj, $"Assets/MasterData/{masterName}/{typeof(T)}{newObj.Id}.asset");

            return newObj;
        }
#endif
    }
}