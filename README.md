A port of Faron Bracy's RogueSharp V3 tutorial using the RogueSharp V4 code. https://roguesharp.wordpress.com/. The project advances as far as Step 18, Creating Stairs in Faron's tutorial.
 
This port is fully integrated with Unity! Because of the Unity integration and some of my personal preferences, there are numerous changes to the original tutorial as written by Faron. Some of major changes:
 
 1. Uses Unity instead of RLNet Console;
 2. The code has been reorganized mostly in a MVC pattern. There are no dependencies in the Model and Controller logic against Unity. Only View logic has any reliance on Unity. This could make ports on other consoles/backends easier (maybe?).
 3. Uses diagonal movement in RogueSharp v4 inlcuding pathfinding.
 4. Most static references removed, passes data by reference. Personal preference, I'm not big on lots of static functions.
 5. Uses RogueSharp v4, not v3 as in the original tutorial.
 6. Object pooling used from Catlike coding, https://catlikecoding.com/unity/tutorials/. Necessary as the height/width sizes cause too many GameObects to be created. The View logic will only display scenes tiles/cells that are visible in the Camera. Changes are made dynamically as the player moves.

Thanks!
Enjoy