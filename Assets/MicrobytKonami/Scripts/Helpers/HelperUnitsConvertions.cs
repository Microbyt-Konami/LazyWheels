using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

namespace MicrobytKonami.Helpers
{
    public static class HelperUnitsConvertions
    {
        public static float MSToKmH(float ms) => ms * 60 * 60f / 1000f;
        public static float KmHToMS(float kmh) => kmh * 1000f / (60 * 60f);
    }
}
