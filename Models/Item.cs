using System;
using System.Collections.Generic;

namespace InternationalWidgetsApp.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string Description { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}
