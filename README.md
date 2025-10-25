# Sloshy Street

Ths is an arcade game prototype about drunk driving. The core gameplay loop is seen through the *GameManager*, *SpawnManager*, and *PlayerController* scripts.

When inspecting this project, the starting scene can be found in `Assets/Scenes/SampleScene.unity`. You can add new obstacles or other drunk effects 
by either extending the `SpawnManager` or adding it to the switch statement in `GameManager.UpdateBAC`.

Play the game: https://brendanhutchins.itch.io/sloshy-street
