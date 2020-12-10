using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace Api.Data.Collections
{
    public class Infectado
    {
        public Infectado(DateTime dataNascimento, string sexo, double latitude, double longitude){
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Localizacao = new GeoJson2DGeographicCoordinates(latitude, longitude);
        }

        public Infectado(DateTime dataNascimento, GeoJson2DGeographicCoordinates localizacao) 
        {
            this.DataNascimento = dataNascimento;
                this.Localizacao = localizacao;
               
        }
                public DateTime DataNascimento { get; set; }

        public string  Sexo { get; set; }

        public GeoJson2DGeographicCoordinates Localizacao { get; set; }
    }
}