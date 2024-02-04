using BoneGame.Nocturnal.GameData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utage;

namespace BoneGame.Nocturnal.Planetarium.Book
{
    public class PageView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI Name;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI Information;

        public void DrawPage(BookMaster master)
        {
            Name.text = master.Name;
            _image.sprite = master.Image;
            Information.text = master.Information;
        }
    }
}