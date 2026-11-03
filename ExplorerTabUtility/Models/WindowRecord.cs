using System;
using System.Text.Json.Serialization;
using ExplorerTabUtility.Helpers;

namespace ExplorerTabUtility.Models;

public class WindowRecord(string location, nint handle = 0, string[]? selectedItems = null, string name = "", bool restore = false)
{
    [JsonConverter(typeof(IntPtrConverter))]
    public nint Handle { get; set; } = handle;
    public string Name { get; set; } = name;
    public string Location { get; set; } = location;
    public string[]? SelectedItems { get; set; } = selectedItems;
    // Persist wall-clock time. Environment.TickCount is process/boot-relative
    // and becomes invalid after a restart or a long-running session.
    public long CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    public bool Restore { get; set; } = restore;

    [JsonIgnore] public string DisplayLocation => Uri.UnescapeDataString(Location.Replace(@"file:\\\", ""));

    [JsonConstructor]
    private WindowRecord() : this(string.Empty)
    {
    }
}
