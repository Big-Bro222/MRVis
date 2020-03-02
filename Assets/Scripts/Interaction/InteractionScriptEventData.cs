using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScriptEventData
{
    /// <summary>
    /// Source of ManipulationEvent.
    /// </summary>
    public InteractionScript ManipulationSource { get; set; }

    /// <summary>
    /// Whether the Manipulation is a NearInteration or not.
    /// </summary>
    public bool IsNearInteraction { get; set; }

    /// <summary>
    /// Center of the <see cref="ManipulationHandler"/>'s Pointer in world space
    /// </summary>
    public Vector3 PointerCentroid { get; set; }

    /// <summary>
    /// Pointer's Velocity.
    /// </summary>
    public Vector3 PointerVelocity { get; set; }

    /// <summary>
    /// Pointer's Angular Velocity in Eulers.
    /// </summary>
    public Vector3 PointerAngularVelocity { get; set; }
}
