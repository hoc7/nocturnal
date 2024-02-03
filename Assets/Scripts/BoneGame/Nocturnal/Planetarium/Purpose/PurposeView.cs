using BoneGame.Event;
using TMPro;
using UnityEngine;

namespace BoneGame.Nocturnal.Planetarium.Purpose
{
    public class PurposeView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _purposeText;

        public void DrawPurpose(PurposeMasterBase masterBase)
        {
            _purposeText.text = masterBase.GetText();
        }
    }
}