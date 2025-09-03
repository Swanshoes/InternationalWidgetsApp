using System;
using System.Collections.Generic;

namespace InternationalWidgetsApp.Models;

public partial class InvoiceItem
{
    public int InvoiceItemId { get; set; }

    public int InvoiceId { get; set; }

    public int ItemId { get; set; }

    public int Quantity { get; set; }

    public decimal LineAmount { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
