using System;
using System.Collections.Generic;

namespace EFthenIncludeDemo.Entities;

public partial class Bolum
{
    public int Id { get; set; }

    public string Adi { get; set; } = null!;

    public virtual ICollection<Ogrenci> Ogrenci { get; set; } = new List<Ogrenci>();
}
