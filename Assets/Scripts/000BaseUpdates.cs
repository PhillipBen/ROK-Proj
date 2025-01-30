using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    /*
    Updates:

    1.0.0:
    -Base Game Created.

    1.0.1:
    -Added User Class
    --Added userID and userBaseName which sets up the variables the database needs to communicate.

    1.0.2:
    -Added Tile Grid. Horizontal Squares. Cuts off tiles at min & max lengths. Performs well enough.
    -Added Coordinate search function.
    -Added Circular Toggle Button to go from player to Kingdom and back home (your home).
    -Added Tile clicking, where the gameobject is selected when the tile is selected. The properties of the user's base still needs to be added to the tile so the selection can have any effect.
    -Added Tile GUI, 3 types: Thick Black, light thin, invisible. Note that this still needs to be added to user settings. Call via TileProperties.UpdateTileSpriteType(ID);

    1.0.3:
    -Tile selection properly moves to player base based on selected player ID.
    -Make player base building tiles + wall.
    -Make the building tiles selectable.

    1.0.4:
    -NOTE: A kingdom has many players. A player has a User (formal login details) attribute, and an InGameProperties attribute.
    -Universe GUI
    -Player GUI
    --Building Blank Popup GUI
    -Test data is being loaded via DataManager

    1.0.5:
    -Player EXP was added and working.
    --NOTE: When tile is clicked, PlayerEXP will need to be gotten from the Player class from that tile.
    -Fixed clicking bugs with UI popups that appeared from certain actions.
    -Added Resource button input and rounding so totals are properly rounded.

    1.0.6:
    -Added TimeManager. Sim time updates are a min of 1 per sec.
    -Added PlayerResourecs, which controlls local player resources.

    1.0.7:
    -UI Button Clicking bugs fixed.
    -Decided on a Gem Conversion number for Time --> Gems
    -Added Builders (1 to start)
    -Added working Building GUI and functionality.
    --Init build and leveling up is working.
    --Tile graphics is working
    --All GUI tabs relating to base building GUI is working.

    1.0.8:
    -Fixed a couple simple building bugs
    -Adds resource production amounts getting from player base.
    --Upon build & upgrade, the prod. amounts are also updated.
    -Reworked the BuildingSlot to be specific to the GameObject, and BuildingManager to contain all the generic details
    -Added BuffManager, which lists buffs (make sure to start at 1.0).
    -Added City hall and it's related variables.
    -Added Mail & all its functionality.

    1.0.9:
    -Added Barracks.
    -Added Unit Training.
    -Added Units listed in Army Manager.
    -Added Barbarians
    -Added Unit movement & Proper Rotational Looks.
    -Finally Added Unit Attacks
    Bug Fixes:
    -On first build of a building, sometimes an 'out of bounds' error occurs. Happens when another building gets clicked when a GUI tab is open. Fixed by removing the ability to select new buildings when GUI is open (Input Manager).
    





    TODO:
    -Add Resources to army and remove 50% on loosing a hit (victor gains 50%) + check new max cap after lost units.
    -Add GUI for attack damage (And HP Bar)
    -Add a Combat Aura so you know you are under attack. (Red screen)
    -Add Combat logs (Log stats, and send mail when complete)
    -Add Kills as Kill Points (Test on field, but eventually set to only when in city.)
    
    Bugs:
    -When moving screen in Kingdom map mode, some tiles are offput by 1 y coordinate. It only has problems with the top and bottom y tiles, never the left or right x tiles. Why this happens, idk. Fixed with a '1 second of no movement' all tile refresh.


    */

    /* Template:
{
    //##### Beg of Variables #####
    //##### End of Variables #####


    //##### Beg of Main Functions #####
    //##### End of Main Functions #####


    //##### Beg of Getters/Setters #####
    //##### End of Getters/Setters #####

    Rounding to specific Dec Point: System.Math.Round(21 / 2, 2)

    */
}
