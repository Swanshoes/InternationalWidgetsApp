using System;
using System.Collections.Generic;

namespace InternationalWidgetsApp.Models;

public partial class Invoice
{
    public int InvoiceId { get; set; }

    public int CustomerId { get; set; }

    public DateOnly InvoiceDate { get; set; }

    public decimal InvoiceTotal { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
}
