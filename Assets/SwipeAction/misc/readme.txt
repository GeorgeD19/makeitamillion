Thank You for purchasing Swipe Action.

Includes:
---------

CameraController.cs
PlayerController.cs
TouchRecorder.cs
AnimationBehavior.cs
ArrowBehavior.cs
CreatureBehavior.cs
SwipeBehavior.cs
SwipeTrailBehavior.cs

---------------------------------------
Updates:
---------------------------------------
[10/22/2012] v1.0
Initial release date

Please Email me at: m11hut@gmail.com if you encounter any bugs, or want a new feature, I will implement it.
---------------------------------------

Description:
------------

The purpose of this Swipe Action package is to provide a basis for specifically an Android based game project. Included with the package comes with a TouchRecorder, which is the main determiner of the direction of various finger swipes. Up, Up Right, Right, Down Right, Down, Down Left, Left, Up Left, Single Point and Undefined are the various determined deterections. This also allows for the SwipeTrail to be drawn onto the screen when swiping across the screen.

This package also comes with an on screen controller that moves the MainPlayer object accordingly, depending on the location at which the controller is pressed. As well as a melee mode toggle that allows for when swiping in a direction at a certain length, the MainPlayer will do a melee slash a long the direction of the swipe. Also, a range mode toggle is included, where similarly to the melee mode, a swipe will cause the MainPlayer to fire an arrow toward the ending swipe location. All of these features are multi-swipe enabled, meaning it is possible to both move and do either a melee or ranged attack at the same time, when on the Android device.

A Desktop Testmode is also included, which can be enabled or disabled on the PlayerController.cs inspector settings within Unity3D. This allows for being able to use the swiping features from the desktop with left mouse inputs. By default this is activated, make sure to deactivate before building your Android project. Also, within the MainPlayer inspector, the PlayerController script also changes of the orientation for where the GUI objects are drawn onto the screen.

Controls:
---------

On Desktop:
    (Desktop Testmode) Left Mousebutton: Simulates a finger swipe on the Screen

On Android:
    Your Fingers: Swiping your finger(s) across the screen will activate various swiping mechanics.

To Use within your own application:
---------------------------------------

1) If you desire particular outcomes to happen based off different Swipe Directions, you will
   need to edit the DirectionDetermination() function within PlayerController.cs. The PlayerController.cs
   script controls all of the actions the MainPlayer emits through the swiping inputs and gui components.

   - NOTE: Within a Portrait Perspective (Android Build), the Directions are rotated 90 degrees. 
           Meaning, the ShapeType.Right will now behave like the ShapeType.Up, ShapeType.Down will now behave
           like ShapeType.Left, ShapeType.DownRight -> ShapeType.UpRight, etc.. Make sure to put your desired                behavior within the proper case function.

   The Orientation settings will rotate the default GUI textures around the screen, in which Reverse Orientation    will reverse the objects a long the center of whichever Orientation is selected.

2) If you're compiling toward the Android / Handheld, make sure you disable the Desktop Test Mode toggle within      the MainPlayer -> Player Controller (Script) Inspector settings within Unity3D. Disabling this allows for
   the Multi-touch calculation to be functioning properly.

3) The Melee Swipe Toggle, if activated will create a MeleeSwipe object toward the center point of the Swipe         Length. Based off the points at which were the initial and final swiping positions. The Ranged Toggle will
   emit an arrow with a force equivalent to the length of the swipe. Which, the arrow fires toward the direction
   of the ending swipe point.

4) To Change the SwipeTrail, or any of the other prefabs, you can find them in the Resources -> Prefabs folder.
   The SwipeTrail uses a Trail Renderer, change it accordingly to your standards.

Please Contact: m11hut@gmail.com if you have any suggestions, bug reports or input.
A Reference To Chase Hutchens would be much appreciated if used in your project(s)