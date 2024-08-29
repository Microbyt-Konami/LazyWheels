using MicrobytKonami.LazyWheels.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class Rays : MonoBehaviour
    {
        //Fields
        [field: SerializeField, Header("Settings")] public RayTrigger RayUp { get; private set; }
        [field: SerializeField] public RayTrigger RayDown { get; private set; }
        [field: SerializeField] public RayTrigger RayLeft { get; private set; }
        [field: SerializeField] public RayTrigger RayRight { get; private set; }
        [field: SerializeField] public LineTrigger RayLineLeft { get; private set; }
        [field: SerializeField] public LineTrigger RayLineRight { get; private set; }
    }
}
