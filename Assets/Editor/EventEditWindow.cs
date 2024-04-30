using System.Collections.Generic;
using BoneGame.Dialogue;
using BoneGame.Event;
using BoneGame.System;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

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
            { "1.マスターデータ", null, SdfIconType.SendDash },
            { "2.イベントフラグ", null, SdfIconType.Activity },
            { "3.会話データ", null, SdfIconType.Diagram2 }
        };
        tree.Add("1.マスターデータ", new EventCreateEditor());
        tree.Add("2.イベントフラグ", new EventFlagCreateEditor());
        tree.Add("3.会話データ", new DialogueCreateEditor());

        tree.AddAllAssetsAtPath("1.マスターデータ", "MasterData/EventMaster", typeof(EventMaster), true);
        tree.AddAllAssetsAtPath("2.イベントフラグ", "MasterData/EventFlag", typeof(EventFlag), true);
        tree.AddAllAssetsAtPath("3.会話データ", "MasterData/DialogueData", typeof(DialogueData), true);

        tree.SortMenuItemsByName();
        return tree;
    }
}

public class EventCreateEditor
{
    [Button("新規マスターデータの作成")]
    public void CreateMaster()
    {
        var master = MasterDataScriptableObject.CreateNewMasterAsset<EventMaster>();
        master.Rename();
    }
}

public class EventFlagCreateEditor
{
    [Button("新規フラッグの作成")]
    public void CreateMaster()
    {
        var master = MasterDataScriptableObject.CreateNewMasterAsset<EventFlag>();
        master.Rename();
    }
}

public class DialogueCreateEditor
{
    [Button("新規会話データの作成")]
    public void CreateMaster()
    {
        var master = MasterDataScriptableObject.CreateNewMasterAsset<DialogueData>();
        master.Rename();
    }
}