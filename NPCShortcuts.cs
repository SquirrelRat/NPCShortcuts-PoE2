using System.Collections.Generic;
using ExileCore2;
using ExileCore2.PoEMemory;
using ExileCore2.PoEMemory.Elements;
using ExileCore2.PoEMemory.MemoryObjects;
using Vector2 = System.Numerics.Vector2;
using System.Drawing;
using System.Linq;
using ExileCore2.Shared.Nodes;

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
            if (!Settings.Enable) return; // Check if the plugin is enabled

            var labelHover = GameController.IngameState.IngameUi.ItemsOnGroundLabelElement.LabelOnHover;
            var hoverPath = GameController.IngameState.IngameUi.ItemsOnGroundLabelElement.ItemOnHoverPath;

            if (labelHover == null || hoverPath == null) return;

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
                segments.Add(("Ctrl", GetColor(Settings.ColorSettings.CtrlColor)));
                segments.Add((": " + npc.Ctrl, Settings.ColorSettings.TextColor.Value));
            }

            if (npc.Alt != null)
            {
                segments.Add((separator + "Alt", GetColor(Settings.ColorSettings.AltColor)));
                segments.Add((": " + npc.Alt, Settings.ColorSettings.TextColor.Value));
            }

            if (npc.CtrlAlt != null)
            {
                segments.Add((separator + "CtrlAlt", GetColor(Settings.ColorSettings.CtrlAltColor)));
                segments.Add((": " + npc.CtrlAlt, Settings.ColorSettings.TextColor.Value));
            }

            var fullText = string.Concat(segments.Select(s => s.text));
            var textSize = Graphics.MeasureText(fullText);
            var startX = labelRect.Center.X - textSize.X / 2;
            var y = labelRect.Top - textSize.Y - 5;

            foreach (var segment in segments)
            {
                Graphics.DrawTextWithBackground(segment.text, new Vector2(startX, y), segment.color, Settings.ColorSettings.BackgroundColor.Value);
                startX += Graphics.MeasureText(segment.text).X;
            }
        }

        private Color GetColor(ColorNode colorNode)
        {
            return Settings.ColorSettings.Enabled ? colorNode.Value : Settings.ColorSettings.TextColor.Value;
        }
    }
}
