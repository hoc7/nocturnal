using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BoneGame.System.UI
{
    public class Transition : SingletonMonoBehaviour<Transition>
    {
        /// <summary>
        /// DontDestroyOnLoadを呼ぶか否か
        /// </summary>
        /// <returns></returns>
        override protected bool IsDontDestroyOnLoad()
        {
            return true;
        }
        
        [SerializeField] private Image Image;
        private bool IsForce;
    
        
        /// <summary>
        /// 画面を黒くする もうすでに黒い場合はそのまま
        /// </summary>
        public void FadeIn(bool isForce = false , float time = 0.5f, bool isWhite = false)
        {
            // すでに黒い場合はそのまま
            if(Image.color.a >= 0.9f)
            {
                return;
            }
            
            if (IsForce)
            {
                if(isForce == false ) return;
            }
            if (isWhite)
            {
                Image.color = new Color(1f,1,1f,0f);
            }
            else
            {
                Image.color = new Color(0f,0,0f,0f);
            }

            IsForce = isForce;
            //Image.DOFade(1f, time);
         
        }

        /// <summary>
        /// 画面を元に戻す
        /// </summary>
        public void FadeOut(bool isForce = false, float time = 0.5f,bool isWhite = false)
        {
            // すでにしろい場合はそのまま
            if(Image.color.a <= 0.1f)
            {
                return;

            }
            
            if (IsForce)
            {
                if(isForce == false ) return;
            }

        
            
            if (isWhite)
            {
                Image.color = Color.white;
            }
            else
            {
                Image.color = Color.black;
            }

            IsForce = false; 
            //Image.DOFade(0f,time);
        }
    }
}