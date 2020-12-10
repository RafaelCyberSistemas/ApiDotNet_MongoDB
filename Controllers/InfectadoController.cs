using System.Net;
using System.Reflection.Metadata;
using System.ComponentModel;
using System.Xml;
using System.Data.Common;
using System;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using MongoDB.Driver;
using Api.Data.Collections;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase
    {
        Data.MongoDB _mongoDB;
        IMongoCollection<Infectado> _infectadoCollection;

        public InfectadoController(Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadoCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.DataNascimento, dto.sexo, dto.latitude, dto.longitude);

            _infectadoCollection.InsertOne(infectado);

            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados(){
            var infectados = _infectadoCollection.Find(Builders<Infectado>.Filter.Empty).ToList();

            return Ok(infectados);
        }


        [HttpPut]
        public ActionResult AtualizarInfectado([FromBody] InfectadoDto dto)
        {
            _infectadoCollection.UpdateOne(Builders<Infectado>.Filter.Where(_ => _.DataNascimento== dto.DataNascimento), Builders<Infectado>.Update.Set("sexo", dto.sexo));
            return Ok("Atualizado com sucesso");
        }

        [HttpDelete("{dataNasc}")]
        public ActionResult Delete(DateTime dataNasc)
        {
            _infectadoCollection.DeleteOne(Builders<Infectado>.Filter.Where(_ => _.DataNascimento == dataNasc));
            return Ok("Deletado com sucesso");
        }

    }
}