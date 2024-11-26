using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class News
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public byte[]? Photo { get; set; }

    public DateTime PublishDate { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
