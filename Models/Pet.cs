namespace c18_98_m_csharp.Models;

public class Pet
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string Breed { get; set; } = "Mestizo";
    public string? Color { get; set; }
    public string? Species { get; set; }
    private DateTime _birthdate;
    public DateTime Birthdate
    {
        get { return _birthdate; }
        set { _birthdate = DateTime.SpecifyKind(value, DateTimeKind.Utc); }
    }
    public bool Sex { get; set; }
    public string? MicrochipId { get; set; }
    public string? Notes { get; set; }
}