using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.DB;

public class ListItemGroup : EntityBase
{
    [MaxLength(100)]
    public string ListItemGroupCode { get; set; }
    public string ListItemGroupName { get; set; }
    public string SystemCode { get; set; }
    public ICollection<ListItem> ListItems { get; set; }
}
