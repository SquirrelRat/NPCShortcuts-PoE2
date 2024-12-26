using ExileCore2.Shared.Interfaces;
using ExileCore2.Shared.Nodes;
using Newtonsoft.Json;
using System.Drawing;

namespace NPCShortcuts
{
    public class NPCShortcutsSettings : ISettings
    {
        public ToggleNode Enable { get; set; } = new ToggleNode(false);
        public ColorNode TextColor { get; set; } = new ColorNode(Color.White);
        public ColorNode BackgroundColor { get; set; } = new ColorNode(Color.Black); // Add background color setting
	public ColorNode CtrlColor { get; set; } = new ColorNode(Color.Green);
	public ColorNode AltColor { get; set; } = new ColorNode(Color.Yellow);
	public ColorNode CtrlAltColor { get; set; } = new ColorNode(Color.Orange);
    }
}
