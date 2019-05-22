﻿using UnityEngine;

public static class RigidBody2Dext
{
    public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force)
    {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        // Normalize without computing magnitude again
        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else
        {
            // From Rigidbody.AddExplosionForce doc:
            // If you pass a non-zero value for the upwardsModifier parameter, the direction
            // will be modified by subtracting that value from the Y component of the centre point.
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }
        //Vector2 ForceToApply = Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir;
        if(rb.position != explosionPosition) rb.AddForce(new Vector2(explosionForce, 0) * explosionDir, mode);
    }
}
