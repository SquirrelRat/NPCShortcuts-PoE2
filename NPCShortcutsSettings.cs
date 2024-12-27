using ExileCore2.Shared.Attributes;
using ExileCore2.Shared.Interfaces;
using ExileCore2.Shared.Nodes;
using Newtonsoft.Json;
using System.Drawing;

namespace NPCShortcuts
{
    public class NPCShortcutsSettings : ISettings
    {
        public ToggleNode Enable { get; set; } = new ToggleNode(false); // Plugin enable toggle
        public ColorSettings ColorSettings { get; set; } = new ColorSettings();
    }

    [Submenu]
    public class ColorSettings
    {
        public ColorNode TextColor { get; set; } = new ColorNode(Color.White);
        public ColorNode BackgroundColor { get; set; } = new ColorNode(Color.Black);
        public ColorNode CtrlColor { get; set; } = new ColorNode(Color.Green);
        public ColorNode AltColor { get; set; } = new ColorNode(Color.Yellow);
        public ColorNode CtrlAltColor { get; set; } = new ColorNode(Color.Orange);

        [Menu("Enable Color Settings")]
        public ToggleNode Enabled { get; set; } = new ToggleNode(true);
    }
}
