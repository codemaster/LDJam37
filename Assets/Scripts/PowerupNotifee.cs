using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PowerupNotifee {

    /// <summary>
    /// Registers the powerup notifee.
    /// </summary>
	void RegisterPowerupNotifee();

    /// <summary>
    /// Notifies that "source" has gotten a powerup.
    /// </summary>
    /// <param name="source">Source.</param>
    void NotifyPowerup(GameObject source);

    /// <summary>
    /// Notifies that "source" has gotten a powerdown.
    /// </summary>
    /// <param name="source">Source.</param>
    void NotifyPowerDown(GameObject source);

}
