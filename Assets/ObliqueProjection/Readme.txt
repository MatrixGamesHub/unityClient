== OBLIQUE PROJECTION ==

= What is it? =
With this component you can turn a camera into an oblique projection camera (see http://en.wikipedia.org/wiki/Cabinet_projection). This is useful for technical illustrations or 2.5d games where you do not want perspective, but also no distortion at the front of your objects.
Every object that is facing towards the camera will stay unstretched at the front, while still allowing you to show its sides.

= How to set it up =
You can take a look at the example scene to see the camera in action. If you want to set it up from scratch, just attach the oblique projection component to any camera. For good results, set the near clipping plane to 0, and the far to 100-500.
Set the x and y of the oblique projection to something between -1 and 1, or use a ratio of 0.5 and any angle. A good size is between 10 and 200.
For normal usage, only have your camera rotated by 90 degree steps.

= Limitations =
- For very low sizes (close zoom) clipping can occur. It can be resolved by moving the camera back or forward accordingly.
- This camera does not support Unity's mouse click system (e.g. http://docs.unity3d.com/Documentation/ScriptReference/MonoBehaviour.OnMouseDown.html). This is due to the way Unity handles the mouse clicks. However you can use raycasting to manually handle it (Unity implicitly uses raycasting for this feature anyway). See the example camera controller for code to do this.
- Just like with the orthographic camera, deferred rendering is not possible (see http://forum.unity3d.com/threads/117884-Deferred-lightning-impossible-on-Orthographic-Camera).

= Properties of ObliqueProjection =
- projectionScale : Vector2 - This controls how much you see of the top and side of your objects.
- Angle, Ratio : float - An alternative way to adjust the projection. Will set projectionScale. Angle is the direction you see objects from, Ratio is how much they stretch into the distance.
- size : float - How much of the scene you can see. Works like "size" of an orthographic camera.

= Methods of ObliqueProjection =
- SetFromAngleAndRatio - Allows you to set angle and ratio in one go.
- ScreenPointToRay - Use this instead of the camera to get the correct ray.
- ScreenToWorld - Use this instead of the camera to correctly transform a vector.

= Contact =
For feedback, suggestions, help, etc. you can contact me at wiltschek@gmx.net