// UTAGE: Unity Text Adventure Game Engine (c) Ryohei Tokimura
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Utage
{
	/// <summary>
	/// ゲーム起動処理のサンプル
	/// </summary>
	public class UtageForAddressableAdvEngineStarter : MonoBehaviour
	{
		//開始シナリオ
		[SerializeField]
		string startScenario = "";

		//ダウンロード処理をするか
		[SerializeField]
		bool enableDownload = false;

		//ローカルにシナリオがある場合
		[SerializeField]
		AdvImportScenarios localScenarios = null;

		//起動時にロードする章のリスト
		List<string> ChapterKeyList { get { return chapterKeyList; } }
		[SerializeField]
		List<string> chapterKeyList = new List<string>();

		public string RootResourceDir { get { return rootResourceDir; } set { rootResourceDir = value; } }
		//リソースディレクトリのルートパス
		[SerializeField]
		string rootResourceDir;

		//ADVエンジン
		public AdvEngine Engine { get { return this.engine ?? (this.engine = FindObjectOfType<AdvEngine>() as AdvEngine); } }
		[SerializeField]
		AdvEngine engine;

		AdvImportScenarios Scenarios { get; set; }

		
		//エンジンをロード
		public async UniTask LoadEngineAsync()
		{
			AssetFileManager.InitLoadTypeSetting(AssetFileManagerSettings.LoadType.Local);
			if (!enableDownload)
			{
				Engine.DataManager.IsBackGroundDownload = false;
			}

			//開始ラベルを登録しておく
			if (!string.IsNullOrEmpty(startScenario))
			{
				Engine.StartScenarioLabel = startScenario;
			}

			if (localScenarios != null)
			{
				this.Scenarios = localScenarios;
			}
			else
			{
				//シナリオのロード
				await LoadScenarioAsync();
			}

			//シナリオとルートパスを指定して、エンジン起動
			//カスタムしてスクリプトを書くときは、最終的にここにくればよい
			Engine.BootFromExportData(this.Scenarios, RootResourceDir);
		}

		//シナリオをロードする
		async UniTask LoadScenarioAsync()
		{
			this.Scenarios = ScriptableObject.CreateInstance<AdvImportScenarios>();

			List<AssetFile> fileList = new List<AssetFile>();
			foreach ( var key in this.ChapterKeyList )
			{
				fileList.Add( AssetFileManager.Load(key, this) );
			}
			foreach (var file in fileList)
			{
				await  LoadChapterAsync(file);
			}
		}
		//シナリオをロードする
		async UniTask LoadChapterAsync(AssetFile file)
		{
			await UniTask.WaitWhile(() => !file.IsLoadEnd);

			AdvChapterData chapter = file.UnityObject as AdvChapterData;
			if (chapter == null)
			{
				Debug.LogError(file.FileName + " is  not scenario file");
				return;
			}
			this.Scenarios.AddChapter(chapter);
		}

		/// <summary>
		/// ラベルを設定する
		/// </summary>
		/// <param name="label"></param>
		public void SetScenarioLabel(string label)
		{
			startScenario = label;
		}
	}
}
