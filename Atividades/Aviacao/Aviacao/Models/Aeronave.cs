﻿using System;
using System.Collections.Generic;

namespace Aviacao.Models;

public partial class Aeronave
{
    public int IdAeronave { get; set; }

    public int QtdPoltronas { get; set; }

    public int IdTipoAeronave { get; set; }

    public virtual TipoAeronave IdTipoAeronaveNavigation { get; set; } = null!;

    public virtual ICollection<Voo> Voos { get; set; } = new List<Voo>();
}