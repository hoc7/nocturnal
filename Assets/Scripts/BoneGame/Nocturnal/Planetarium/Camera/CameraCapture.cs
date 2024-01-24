using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace BoneGame.Nocturnal.Planetarium
{
    public class CameraCapture : MonoBehaviour
    {
        [SerializeField] private Button Button;
        [SerializeField] private Camera MainCamera;
        [SerializeField] private RenderTexture renderTexture;
        [SerializeField] private PlayableDirector Director;
        [SerializeField] private Image CaptureImage;

        private void Start()
        {
            Button.OnClickAsObservable().Subscribe(_ =>
            {
                ExecCapture();
            }).AddTo(this);
        }

        public void ExecCapture()
        {
            Sprite sprite = CaptureToSprite();
            CaptureImage.sprite = sprite;
            Director.Play();
        }
        
        private RenderTexture Capture()
        {
            RenderTexture previous = RenderTexture.active;
            RenderTexture.active = renderTexture;
            MainCamera.targetTexture = renderTexture;
            MainCamera.Render();
            MainCamera.targetTexture = null;
            RenderTexture.active = previous;

            return renderTexture;
        }
        
        /// <summary>
        /// Texture2DのCapture
        /// </summary>
        /// <returns></returns>
        public Texture2D CaptureToTexture2D()
        {
            RenderTexture captured = Capture();

            Texture2D tex = new Texture2D(captured.width, captured.height, TextureFormat.RGB24, false);
            RenderTexture currentActiveRT = RenderTexture.active;
            RenderTexture.active = captured;
            tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
            tex.Apply();
            RenderTexture.active = currentActiveRT;

            return tex;
        }

        /// <summary>
        /// SpriteのCapture
        /// </summary>
        /// <returns></returns>
        public Sprite CaptureToSprite()
        {
            Texture2D tex = CaptureToTexture2D();
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);
            return sprite;
        }
    }
}