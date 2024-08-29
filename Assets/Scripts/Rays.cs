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
        [Header("Settings")]
        [SerializeField] private RayTrigger rayUp;
        [SerializeField] private RayTrigger rayDown;
        [SerializeField] private RayTrigger rayLeft;
        [SerializeField] private RayTrigger rayRight;
        [SerializeField] private LineTrigger rayLineLeft;
        [SerializeField] private LineTrigger rayLineRight;
    }
}
