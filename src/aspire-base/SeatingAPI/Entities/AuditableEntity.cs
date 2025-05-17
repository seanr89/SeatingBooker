
/// <summary>
/// Custom model to handle audit fields, inherited on a model per model basis
/// </summary>
public class AuditableEntity
{
    /// <summary>
    /// Datetime to denote the date and time an item was created
    /// </summary>
    /// <value></value>
    public DateTime Created { get; set; }

    /// <summary>
    /// Planned email identifier of a single user who created
    /// or app name etc...
    /// </summary>
    /// <value></value>
    public string CreatedBy { get; set; } = "System";

    public DateTime? LastModified { get; set; }

    public string LastModifiedBy { get; set; } = "System";
}