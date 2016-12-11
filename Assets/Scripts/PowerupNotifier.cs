using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PowerupNotifier
{
    /// <summary>
    /// Registers "notifee" in the notifiers list of notifees.
    /// </summary>
    /// <param name="notifee">Notifee.</param>
    void RegisterPowerupNotifee(PowerupNotifee notifee);

    /// <summary>
    /// Notifies all notifees that a powerup event has happened.
    /// </summary>
    void NotifyPowerup();

    /// <summary>
    /// Notifies all notifees that a powerdown event has happened.
    /// </summary>
    void NotifyPowerDown();

}
