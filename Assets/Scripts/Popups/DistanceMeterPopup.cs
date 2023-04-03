using TMPro;
using UnityEngine;

namespace Game 
{
    public class DistanceMeterPopup : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text;

        public void SetDistanceValue(float distance)
        {
            text.text = $"Distance : {distance}";
        }
    }
}

