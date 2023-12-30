using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
namespace System
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
#endif
    }
}