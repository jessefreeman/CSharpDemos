//  
// Copyright (c) Jesse Freeman. All rights reserved.  
// 
// Licensed under the Microsoft Public License (MS-PL) License. 
// See LICENSE file in the project root for full license information. 
// 
// Contributors
// --------------------------------------------------------
// This is the official list of Pixel Vision 8 contributors:
//  
// Jesse Freeman - @JesseFreeman
// Christer Kaitila - @McFunkypants
// Pedro Medeiros - @saint11
// Shawn Rakowski - @shwany
// 

using PixelVisionSDK;
using PixelVisionSDK.Chips;

// The Mouse Demo shows off how to capture mouse input and display a cursor on the screen.
// Pixel Vision 8 requires the runner to supply mouse data via the ControllerChip.You will
// need to implement the IMouseInput interface and register a custom Mouse Class with the
// ControllerChip in order for this demo to work.

/// <summary>
///     We are going to extend the GameChip class and override its Init(), Update() and Draw() methods.
/// </summary>
public class MouseDemo : GameChip
{

    // We need to create some fields to store the mouse cursor's sprites, dimensions, position, and offset.
    private readonly int[] cursorSprites = {4, 5, 10, 11};
    private readonly int cursorWidth = 2;
    private readonly int fontOffsetX = 128;
    private readonly Vector mousePos = new Vector(-1, 0);

    /// <summary>
    ///     The Init() method is part of the game's lifecycle and called a game starts. We are going to
    ///     use this method to configure background color, ScreenBufferChip and draw some text to the display.
    /// </summary>
//    public override void Init()
//    {
//
//        // Before we start, we need to set a background color and rebuild the ScreenBufferChip. The screen buffer 
//        // allows us to draw our fonts into the background layer to save on draw calls.
//        apiBridge.ChangeBackgroundColor(32);
//        apiBridge.RebuildScreenBuffer();
//
//        // By default, the DisplayChip wraps sprite on the screen. We will need to disable this, so our cursor 
//        // doesn't appear on the opposite side of the display when it is too close to the edge.
//        apiBridge.ToggleDisplayWrap(false);
//
//        // This default text will help display the current state of the mouse. We'll render it into the 
//        // ScreenBufferChip to cut down on sprite draw calls.
//        apiBridge.DrawFontToBuffer("MOUSE POSITION", 1, 1, "large-font", 0);
//        apiBridge.DrawFontToBuffer("BUTTON 1 DOWN", 1, 3, "large-font", 0);
//        apiBridge.DrawFontToBuffer("BUTTON 2 DOWN", 1, 4, "large-font", 0);
//
//    }
//
//    /// <summary>
//    ///     The Update() method is part of the game's life cycle. The engine calls Update() on every frame before
//    ///     the Draw() method. It accepts one argument, timeDelta, which is the difference in milliseconds since
//    ///     the last frame. We are going to keep track of the mouse's position on each frame.
//    /// </summary>
//    public override void Update(float timeDelta)
//    {
//
//        // The APIBridge exposes a property for the mouse's x and y position. We'll store this in a field and 
//        // retrieve it during Draw() execution of the GameChip's life cycle.
//        mousePos.x = apiBridge.mouseX;
//        mousePos.y = apiBridge.mouseY;
//
//        // While this step may appear to be wasteful, it's important to separate any calculation logic from 
//        // render logic. This optimization technique will ensure the best performance for Pixel Vision 8 games 
//        // and free up the Draw() method to only focus on rendering.
//
//    }
//
//    /// <summary>
//    ///     The Draw() method is part of the game's life cycle. It is called after Update() and
//    ///     is where all of our draw calls should go. We'll be using this to render font characters and the
//    ///     mouse cursor to the display.
//    /// </summary>
//    public override void Draw()
//    {
//
//        // It's important to clear the display on each frame. There are two ways to do this. Here 
//        // we are going to use the DrawScreenBuffer() way to copy over the existing buffer and clear
//        // all of the previous pixel data.
//        apiBridge.DrawScreenBuffer();
//
//        // We are going to detect if the mouse is on the screen. When the cursor is within the bounds 
//        // of the DisplayChip, we will show its x and y position. 
//        if (mousePos.x < 0 || mousePos.y < 0)
//        {
//            apiBridge.DrawFont("OFFSCREEN", fontOffsetX, 8, "large-font", 0);
//        }
//
//        else
//        {
//
//            // Pixel Vision 8 automatically returns a -1 value for the mouse x and y position if it is out of the bounds of the DisplayChip 
//            // or if a mouse was is not registered with the ControllerChip. 
//
//            // Since the mouse within the display, let's show its current x and y position.
//            apiBridge.DrawFont("(" + mousePos.x.ToString("D3") + "," + mousePos.y.ToString("D3") + ")", fontOffsetX, 8, "large-font", 0);
//
//            // We also need to draw it to the display. We'll be using the DrawSprites() method to take the four cursor sprites and render them in a 2 x 2 grid.
//            apiBridge.DrawSprites(cursorSprites, mousePos.x, mousePos.y, cursorWidth, false, false, true, 0);
//
//        }
//
//        // For the last bit of code we are just going to display whether the left or right mouse button is being held down by using the 
//        // GetMouseButton() method on the APIBridge.
//        apiBridge.DrawFont(apiBridge.GetMouseButton(0).ToString(), fontOffsetX - 8, 24, "large-font", 0);
//        apiBridge.DrawFont(apiBridge.GetMouseButton(1).ToString(), fontOffsetX - 8, 32, "large-font", 0);
//
//    }

}