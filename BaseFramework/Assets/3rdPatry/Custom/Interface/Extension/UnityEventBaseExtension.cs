using UnityEngine.UI;
using UnityEngine.Events;

namespace CustomExtension
{

    public static class UnityEventBaseExtension
    {

        public static void ChangeValuewithoutNotify(this Toggle tog, bool isOn, UnityAction<bool> call = null)
        {
            var cacheEvent = tog.onValueChanged;
            if (call != null)
                tog.onValueChanged.RemoveListener(call);
            else
                tog.onValueChanged.RemoveAllListeners();

            tog.isOn = isOn;

            if (call != null)
                tog.onValueChanged.AddListener(call);
            else
                tog.onValueChanged = cacheEvent;
        }

    }

}