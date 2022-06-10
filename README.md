# Fishnet Quick Start Project

This is a written guide and unity project to get you up and running with FishNet

It covers:

* [Basic scene setup](#Basic-scene-setup)
* [Player Movement](#Player-movement)
* [Syncing Data](#Syncing-Data)
* [Network calls](#Network-Calls)
* [Flowing between scenes](#Scene-Management)

## Basic scene setup
These instructions assume you are starting with a blank/default new project and following along.  There is an example project with these instructions followed in the github repo that you can clone and compare to what you have made for troubleshooting purposes or to just look at if that is how you learn.

### Download fishnet and add to your project
* [Fishnet: Github and Import](https://github.com/FirstGearGames/FishNet) 
* --- OR ---
* [Unity Asset Store and Import](https://assetstore.unity.com/packages/tools/network/fish-net-networking-evolved-207815)

### Create the Manager Objects in your scene
1. *(optional)* Create a new folder called _Game in your assets folder so your assets are kept separate from imported assets
1. *(optional)* Move your Scenes folder into that new _Game folder
1. Create an empty game object at the root of your scene called NetworkManager and Add following FishNet scripts:
 	1. NetworkManager
	1. Player Spawner 
	1. (optional) Tugboat
1. Set the 'Spawnable Prefabs' to 'DefaultPrefabObjects'
1. Find a place the FishNet/Example/Prefabs/NetworkHudCanvas as a child of the NetworkManager you just created.

### Create the Player Prefab
1. Add an empty game object called 'Player' and add a child Capsule to graphically represent it and add the following FishNet scripts:
	1. Network Object

1. Create a prefab out of it ( create a prefab folder under _Game and drag the object you just created into, then delete the object in your scene.
1. Drag this player prefab into the player spawner component on your Basic scene network manager.

### Test
1. launch the game in your editor and make sure that once you click on 'start server', then 'start client' the player spawns.
1. You can test multiple versions of your game in multiple editors withou haveing to build using [Parelsync](https://github.com/VeriorPies/ParrelSync). I will not cover that here but it's the easiest way to dev multiplayer games that I know of.  You can also build the player and test that way.

![Editor View in Play mode](/images/basic-1.png)

If everything seems to be working and your project looks right move on to the next section: Player Movement


## Player movement
I'm making some decisions here that make this phase a bit more complex, but it will be more useful in later sections to set up the player using Unity's new input system, it's Actions and the standard unity character controller.  Deal with it, this is the future and it's better.

### Get the new input package and import it.
[Install the Input Package](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.3/manual/Installation.html)

### Add the input bits to the player object and configure the input system.
1. Create a new script on the player prefab called 'PlayerInputDriver' (or whatever you would like I suppose) and save it in a new _Game/Scripts folder.
1. [Follow the Quickstart](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.3/manual/QuickStartGuide.html)
adding the PlayerInput object to the player prefab you created earlier.  During Step 2 of that process add an addition jump action as shown. ![Jump Action](/images/jump-action.png). *Hint: for the binding use the 'listen' button and press space, then select what is shown.*
1. Edit the player input driver script you created to look like this
```

```

## Syncing Data

## Network Calls

## Scene Management

