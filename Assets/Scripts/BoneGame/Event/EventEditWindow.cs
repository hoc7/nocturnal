using System.Collections.Generic;
using BoneGame.System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace BoneGame.Event
{
    public class EventEditWindow : OdinMenuEditorWindow
    {
        [MenuItem("MasterData/Event Window")]
        private static void OpenWindow()
        {
            GetWindow<EventEditWindow>().Show();
        }

        public List<EventMaster> EventMasters()
        {
            return MasterDataScriptableObject.FindMaster<EventMaster>();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(supportsMultiSelect: false)
            {
                { "マスターデータ", null, SdfIconType.SendDash },
                { "イベントフラグ", null, SdfIconType.Activity }
            };

            tree.AddAllAssetsAtPath("マスターデータ", "MasterData/EventMaster", typeof(EventMaster), true);
            tree.AddAllAssetsAtPath("イベントフラグ", "MasterData/EventFlag", typeof(EventFlag),true);
            tree.SortMenuItemsByName();
            return tree;
        }
    }
}