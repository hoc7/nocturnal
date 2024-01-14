using System.Text;
using BoneGame.Message;
using BoneGame.SaveSystem;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.UI;

namespace BoneGame.System
{
    public abstract class ContinueCell : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI SaveDataIdText;
        [SerializeField] protected TextMeshProUGUI SaveDataTitle;
        public Button SaveDataDeleteButton;
        public Button BannerButton;
        protected int saveDataId;

        /// <summary>
        /// コンティニュー情報の描画
        /// </summary>
        /// <param name="saveData"></param>
        public virtual void Draw(SaveDataBase saveData)
        {
            if (saveData.SaveId != SaveManager.AutoSaveId)
            {
                SaveDataDeleteButton.gameObject.SetActive(true);
                StringBuilder builder = new StringBuilder("No.").Append(saveData.SaveId);
                SaveDataIdText.text = builder.ToString();
            }
            else
            {
                SaveDataDeleteButton.gameObject.SetActive(false);
                SaveDataIdText.text = "オートセーブ";
            }

            saveDataId = saveData.SaveId;
        }

        private void Start()
        {
            if (SaveDataDeleteButton != null)
            {
                SaveDataDeleteButton.OnClickAsObservable().Subscribe(_ => { OnClickDelete(); }).AddTo(this);
            }

            if (BannerButton != null)
            {
                BannerButton.OnClickAsObservable().Subscribe(_ => { OnClickButton(); }).AddTo(this);
            }
        }

        protected virtual void OnClickButton()
        {
            
        }

        protected virtual void OnClickDelete()
        {
            SaveDataDeleteMessage deleteMessage = new SaveDataDeleteMessage(this.saveDataId);
            Messenger.Publish(deleteMessage);
        }
    }
}