using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Comment
{
    public int Id { get; set; }

    public int NewsId { get; set; }

    public string? UserId { get; set; }

    public DateTime Date { get; set; }

    public string Text { get; set; } = null!;

    public virtual News News { get; set; } = null!;
}
