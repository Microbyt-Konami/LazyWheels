using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    public static class Physics2DPhysics2DEx
    {
        public static float CalculatePointCollisionBetween2Bounds(Bounds collisionBounds, Bounds playerBounds, Func<Vector3, float> GetComponentBound)
        {
            float collisionMin = GetComponentBound(collisionBounds.min);
            float collisionMax = GetComponentBound(collisionBounds.max);
            float playerMin = GetComponentBound(playerBounds.min);
            float playerMax = GetComponentBound(playerBounds.max);

            // obtener el punto máximo de los mínimos de X
            float minX = Mathf.Max(collisionMin, playerMin);

            // obtener el punto máximo de los máximos de X
            float maxX = Mathf.Min(collisionMax, playerMax);

            // SABER EL PUNTO EXACTO DONDE COLISIONA UN OBJETO

            // Al mínimo sumarle el máximo y obtener el total
            // Lo divido por 2 así estaré en la mitad de la colisión o pivote
            // Se lo resto al total de collider del que necesite obtener la posición exacta

            float average = (minX + maxX) / 2 - collisionMin;

            return average;
        }

        public static float CalculatePointCollisionXBetween2Bounds(Bounds collisionBounds, Bounds playerBounds)
        {
            return CalculatePointCollisionBetween2Bounds(collisionBounds, playerBounds, b => b.x);
        }

        public static float CalculatePointCollisionYBetween2Bounds(Bounds collisionBounds, Bounds playerBounds)
        {
            return CalculatePointCollisionBetween2Bounds(collisionBounds, playerBounds, b => b.y);
        }
    }
}
