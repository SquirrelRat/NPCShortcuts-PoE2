using System;
using System.Collections.Generic;
using System.Linq;
using ExileCore2;
using ExileCore2.PoEMemory;
using ExileCore2.PoEMemory.Elements;
using ExileCore2.PoEMemory.MemoryObjects;
using System.Drawing;
using Vector2 = System.Numerics.Vector2;
using RectangleF = ExileCore2.Shared.RectangleF;

namespace NPCShortcuts
{
    public class NPCShortcuts : BaseSettingsPlugin<NPCShortcutsSettings>
    {
        public override bool Initialise()
        {
            return true;
        }

        public override void Render()
        {
            var labelHover = GameController.IngameState.IngameUi.ItemsOnGroundLabelElement.LabelOnHover;
            if (labelHover == null) return;

            var hoverPath = GameController.IngameState.IngameUi.ItemsOnGroundLabelElement.ItemOnHoverPath;
            if (!NPCDatabase.NPCDictionary.TryGetValue(hoverPath, out var npc)) return;

            DrawShortcuts(npc, labelHover);
        }

        private void DrawShortcuts(NPC npc, Element label)
        {
            var labelRect = label.GetClientRectCache;

            var separator = "  ";
            var stringToDisplay = string.Empty;

            if (npc.Ctrl != null) stringToDisplay += "Ctrl: " + npc.Ctrl;
            if (npc.Alt != null) stringToDisplay += separator + "Alt: " + npc.Alt;
            if (npc.CtrlAlt != null) stringToDisplay += separator + "CtrlAlt: " + npc.CtrlAlt;

            var textSize = Graphics.MeasureText(stringToDisplay);

            var boxPos = new Vector2(labelRect.Center.X - textSize.X / 2, labelRect.Top - textSize.Y - 5);
            var boxSize = new Vector2(textSize.X, textSize.Y);

            var backgroundColor = Settings.BackgroundColor.Value;
            var textColor = Settings.TextColor.Value;

            // Use DrawTextWithBackground
            Graphics.DrawTextWithBackground(stringToDisplay, boxPos, textColor, backgroundColor);
        }
    }
}
