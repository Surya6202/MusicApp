using System;
using System.Diagnostics.CodeAnalysis;

namespace MusicApp.Models
{
    [ExcludeFromCodeCoverage]
    public class Music
    {
        public int MusicId { get; set; }
        public string MusicName { get; set; }
        public int PublishedYear { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public bool IsPublished { get; set; } = true;
    }
}
