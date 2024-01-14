using System.Text;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace BoneGame.System
{
    public abstract class SaveCell : MonoBehaviour
    {
        public Button Button;
        /// <summary>
        /// コンティニュー情報の描画
        /// </summary>
        /// <param name="saveData"></param>
        public virtual void Draw()
        {

        }

        private void Start()
        {
            Button.OnClickAsObservable().Subscribe(_ =>
            {
            }).AddTo(this);
        }
    }
}