using System.Collections.Generic;
using ExileCore2;
using ExileCore2.PoEMemory;
using ExileCore2.PoEMemory.Elements;
using ExileCore2.PoEMemory.MemoryObjects;
using Vector2 = System.Numerics.Vector2;
using System.Drawing;
using System.Linq;

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
            if (hoverPath == null) return;

            if (NPCDatabase.NPCDictionary.ContainsKey(hoverPath))
            {
                if (NPCDatabase.NPCDictionary.TryGetValue(hoverPath, out var npc))
                {
                    DrawShortcuts(npc, labelHover);
                }
            }
        }

        private void DrawShortcuts(NPC npc, Element label)
        {
            var labelRect = label.GetClientRectCache;
            var separator = "  ";

            var segments = new List<(string text, Color color)>();

            if (npc.Ctrl != null)
            {
                segments.Add(("Ctrl", Settings.CtrlColor.Value));
                segments.Add((": " + npc.Ctrl, Settings.TextColor.Value));
            }

            if (npc.Alt != null)
            {
                segments.Add((separator + "Alt", Settings.AltColor.Value));
                segments.Add((": " + npc.Alt, Settings.TextColor.Value));
            }

            if (npc.CtrlAlt != null)
            {
                segments.Add((separator + "CtrlAlt", Settings.CtrlAltColor.Value));
                segments.Add((": " + npc.CtrlAlt, Settings.TextColor.Value));
            }

            var fullText = string.Concat(segments.Select(s => s.text));
            var textSize = Graphics.MeasureText(fullText);
            var startX = labelRect.Center.X - textSize.X / 2;
            var y = labelRect.Top - textSize.Y - 5;

            foreach (var segment in segments)
            {
                Graphics.DrawTextWithBackground(segment.text, new Vector2(startX, y), segment.color, Settings.BackgroundColor.Value);
                startX += Graphics.MeasureText(segment.text).X;
            }
        }
    }
}
