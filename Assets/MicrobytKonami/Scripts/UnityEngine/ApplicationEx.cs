using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
    public static class ApplicationEx
    {
        public static bool supportsAccelerometer
        {
            get
            {
#if UNITY_EDITOR
#if UNITY_ANDROID || UNITY_IOS
                return true;
#else
                return false;
#endif
#else
                return Application.supportsAccelerometer
#endif
            }
        }
    }
}