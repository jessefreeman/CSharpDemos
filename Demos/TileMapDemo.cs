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

using PixelVisionSDK.Engine.Chips.Data;
using PixelVisionSDK.Engine.Chips.Game;
using PixelVisionSDK.Engine.Utils;

/// <summary>
///     We are going to extend the GameChip class and override its Init(), Update() and Draw() methods.
/// </summary>
public class TilemapDemo : GameChip
{

    // These vectors represent directions we can scroll the map in.
    private readonly Vector[] dir =
    {
        new Vector(1, 0),
        new Vector(-1, 0),
        new Vector(0, 1),
        new Vector(0, -1),
        new Vector(1, 1),
        new Vector(-1, 1),
        new Vector(1, -1),
        new Vector(-1, -1)
    };

    // These fields store values we need for our tilemap.
    private readonly Vector gridSize = new Vector();
    private readonly Vector scrollPos = new Vector();
    private int currentDir;
    private int tileSize;

    /// <summary>
    ///     The Init() method is part of the game's lifecycle and called a game starts. We are going to
    ///     use this method to configure background color, ScreenBufferChip and calculate the tile size.
    /// </summary>
    public override void Init()
    {

        // Before we start, we need to set a background color and rebuild the ScreenBufferChip. The screen buffer 
        // allows us to draw our fonts into the background layer to save on draw calls.
        apiBridge.ChangeBackgroundColor(0);
        apiBridge.RebuildScreenBuffer();

        // Here we are going to calculate the tile size for the map and the size of the visible grid.
        tileSize = apiBridge.spriteWidth * 2;
        gridSize.x = apiBridge.displayWidth / tileSize;
        gridSize.y = apiBridge.displayHeight / tileSize;

    }

    /// <summary>
    ///     The Update() method is part of the game's life cycle. The engine
    ///     calls Update() on every frame before the Draw() method. It accepts
    ///     one argument, timeDelta, which is the difference in milliseconds
    ///     since the last frame. We are going to update the scroll position based on
    ///     the currently selected direction.
    /// </summary>
    public override void Update(float timeDelta)
    {

        // Let's detect if the mouse's left button is pressed to change the direction of the scrolling.
        if (apiBridge.GetMouseButtonDown(0))
        {
            currentDir = MathUtil.Repeat(currentDir + 1, dir.Length);
        }

        // Now we need to set the new position for the scrolling based on the currectDir ID.
        var newPos = dir[currentDir];

        // Next, we'll update the scroll position's x and y values.
        scrollPos.x += newPos.x;
        scrollPos.y += newPos.y;

        // Finally we can scroll the screen buffer by calling the APIBridge's ScrollTo() method.
        apiBridge.ScrollTo(scrollPos.x, scrollPos.y);

    }

    /// <summary>
    ///     The Draw() method is part of the game's life cycle. It is called after Update() and
    ///     is where all of our draw calls should go. We'll be using this to render map to the display.
    /// </summary>
    public override void Draw()
    {

        // It's important to clear the display on each frame. There are two ways to do this. Here 
        // we are going to use the DrawScreenBuffer() way to copy over the existing buffer and clear
        // all of the previous pixel data.
        apiBridge.DrawScreenBuffer();

        // For dynamic text, such as the time value we are tracking, it will be too expensive to update the 
        // ScreenBufferChip on each frame. So, in this case, we are going to display the map's scroll position.
        apiBridge.DrawFont("Scroll (" + scrollPos.x + "," + scrollPos.y + ")", 8, 0, "message-font", -4);

    }

}