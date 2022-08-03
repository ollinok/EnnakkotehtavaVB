﻿namespace DataAccess.Models;

public class ArticlesModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Ean { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
