﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MensariumAPI.Podaci.Entiteti;


namespace MensariumAPI.Podaci.DTO
{
    public class PozivanjaDto
    {
        public virtual DateTime DatumPoziva { get; set; }
        public virtual string VremeTrajanja { get; set; } // u bazi je time
        public virtual KorisnikDto Poziva { get; set; } // onaj ko poziva na obrok

        public virtual IList<PozivanjaPozvaniDto> Pozvani { get; set; }

        public PozivanjaDto()
        {
            Pozvani = new List<PozivanjaPozvaniDto>();
        }
    }
}