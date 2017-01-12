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
// 

using System;
using PixelVisionSDK.Engine.Chips.Data;
using PixelVisionSDK.Engine.Chips.Game;
using PixelVisionSDK.Engine.Utils;

// Let's take a look at how to draw sprites to the display. The SpriteChip handles rendering each 
// 8 x 8 px sprite on the screen.For this demo, we'll need to create a new game class that contains
// all of our logic. Inside of the game class, we'll work with the APIBridge's draw methods as well
// as the FontChip's own draw methods for visually debugging sprite data. 

/// <summary>
///     The DrawSpriteExample extends the GameChip and will override its Init(), Update() and Draw() methods.
/// </summary>
public class DrawSpriteDemo : GameChip
{

    private int frame;

    /// <summary>
    ///     These values represent the shell's position, speed, animation
    ///     time and frame.
    /// </summary>
    private readonly Vector shellAPos = new Vector(0, 8 * 8);

    private readonly Vector shellBPos = new Vector(8 * 22, 0);

    /// <summary>
    ///     This 2D array stores sprite IDs for the turtle shell animations.
    ///     Each shell is a made up of 4 sprites in a 2x2 grid.
    /// </summary>
    private readonly int[][] shellSprites =
    {
        new[] {0, 1, 6, 7},
        new[] {2, 3, 8, 9}
    };

    private readonly int speed = 100;
    private float time;

    /// <summary>
    ///     The Init() method is part of the game's lifecycle and called a game
    ///     starts. We are going to use this method to configure the DisplayChip,
    ///     ScreenBufferChip and also draw fonts into the background layer.
    /// </summary>
    public override void Init()
    {

        // Here we are starting by changing the background color and telling
        // the DisplayChip to wrap.
        apiBridge.ChangeBackgroundColor(32);
        apiBridge.ToggleDisplayWrap(true);

        // Here we are rebuilding the screen buffer so we can draw tile and 
        // fonts to it. This will cut down on our draw calls.
        apiBridge.RebuildScreenBuffer();

        // With the ScreenBuffer ready, we can now draw fonts into it. Here
        // we are creating two new labels to display under our demo sprites.
        apiBridge.DrawFontToBuffer("Sprite Test", 1, 1, "large-font", 0);
        apiBridge.DrawFontToBuffer("Position Wrap Test", 1, 6, "large-font");

    }

    /// <summary>
    ///     The Update() method is part of the game's life cycle. The engine
    ///     calls Update() on every frame before the Draw() method. It accepts
    ///     one argument, timeDelta, which is the difference in milliseconds
    ///     since the last frame. We are going to keep track of time to sync
    ///     up our sprite animation as well as move the sprites across the screen.
    /// </summary>
    public override void Update(float timeDelta)
    {

        // We are going to move the sprite positions by calculating the speed by 
        // the timeDelata. We can then add this to the x or y position of our sprite
        // vector.
        shellAPos.x += (int) Math.Ceiling(speed * timeDelta);
        shellBPos.y += (int) Math.Ceiling(speed * timeDelta);

        // We are going to keep track of the time by adding timeDelta to our time 
        // field. We can then use this to tell if we should change our animation frame.
        time += timeDelta;

        // We'll use modulus to determine when it's time to change the sprite frame.
        if (time > 0.09f)
        {

            // If time modulus 9 is 0 we'll increase the frame number to advance the animation.
            frame = MathUtil.Repeat(frame + 1, shellSprites.Length);

            // The MathUtil has several methods we can use to simplify common calculations.
            // MathUtil.Repeat() will loop a value based on the maximum value supplied. It's important
            // to use this sparingly since it could potentially slow your game down.

            time = 0f;
        }

    }

    /// <summary>
    ///     The Draw() method is part of the game's life cycle. It is called after Update() and
    ///     is where all of our draw calls should go. We'll be using this to render each of
    ///     the sprites and font characters to the display.
    /// </summary>
    public override void Draw()
    {

        // It's important to clear the display on each frame. There are two ways to do this. Here 
        // we are going to use the DrawScreenBuffer() way to copy over the existing buffer and clear
        // all of the previous pixel data.
        apiBridge.DrawScreenBuffer();

        // Here we are going to draw the first example. The turtle shell is made up of 4 sprites.
        // We'll draw each sprite out with a few pixels between them so you can see how they are
        // put together.
        apiBridge.DrawSprite(0, 8, 24, false, false, true, 0);
        apiBridge.DrawSprite(1, 18, 24, false, false, true, 0);
        apiBridge.DrawSprite(6, 8, 34, false, false, true, 0);
        apiBridge.DrawSprite(7, 18, 34, false, false, true, 0);

        // For the next two examples we'll use the DrawSprites() method which allows us to combine sprites together into 
        // a single draw request. Each sprite still counts as a draw call but this simplifies drawing
        // larger sprites in your game.
        apiBridge.DrawSprites(shellSprites[0], 32, 24, 2, false, false, true, 0);
        apiBridge.DrawSprites(shellSprites[frame], 54, 24, 2, false, false, true, 0);

        // Here we are drawing a turtle shell along the x and y axis. We'll take advantage of the Display's wrap
        // setting so that the turtle will appear on the opposite side of the screen even when the x or y
        // position is out of bounds.
        apiBridge.DrawSprites(shellSprites[frame], shellAPos.x, shellAPos.y, 2, false, false, true, 0);
        apiBridge.DrawFont("(" + shellAPos.x + "," + shellAPos.y + ")", shellAPos.x, shellAPos.y + 20, "large-font", 0);

        // The last thing we are going to do is draw text below each of our moving turtles so we can see the
        // x and y position as they wrap around the display.
        apiBridge.DrawSprites(shellSprites[frame], shellBPos.x, shellBPos.y, 2, false, false, true, 0);
        apiBridge.DrawFont("(" + shellBPos.x + "," + shellBPos.y + ")", shellBPos.x, shellBPos.y + 20, "large-font", 0);

    }

    // At this point our sprite demo is ready to be run. Load it up into an instance of the PixelVision8 engine and see how it works.
}