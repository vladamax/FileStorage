using System;
using System.Collections.Generic;

namespace FileStorage.Core.Entities;

public partial class File
{
    public long Id { get; set; }

    public string Filename { get; set; } = null!;

    public string FileBase64 { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
