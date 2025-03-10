using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using PeliculasAPI.Controllers;
using PeliculasAPI.DTOs;
using PeliculasAPI.Entidades;
using PeliculasAPIPruebas.Dobles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPIPruebas.Controllers
{
    [TestClass]
    public sealed class GenerosControllerTests : BasePruebas
    {
        [TestMethod]
        public async Task Get_DevuelveTodosLosGeneros()
        {
            // Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutomapper();
            IOutputCacheStore outputCacheStore = null!;

            contexto.Generos.Add(new Genero() { Nombre = "Genero 1" });
            contexto.Generos.Add(new Genero() { Nombre = "Genero 2" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);

            var controller = new GenerosController(outputCacheStore, contexto2, mapper);


            //Prueba
            var respuesta = await controller.Get();

            //Verificacion
            Assert.AreEqual(expected: 2, actual: respuesta.Count);
        }

        [TestMethod]
        public async Task Get_DebeDevolver404_CuandoGeneroIdNoExiste()
        {
            // Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutomapper();
            IOutputCacheStore outputCacheStore = null!;

            var controller = new GenerosController(outputCacheStore, contexto, mapper);

            var id = 1;

            //Prueba
            var respuesta = await controller.ObtenerPorId(id);

            //Verificacion
            var resultado = respuesta.Result as StatusCodeResult;
            Assert.AreEqual(expected: 404, actual: resultado!.StatusCode);
        }
        
        [TestMethod]
        public async Task Get_DebeDevolverGeneroCorrecto_CuandoGeneroIdExiste()
        {
            // Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutomapper();
            IOutputCacheStore outputCacheStore = null!;

            contexto.Generos.Add(new Genero() { Nombre = "Genero 1" });
            contexto.Generos.Add(new Genero() { Nombre = "Genero 2" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);

            var controller = new GenerosController(outputCacheStore, contexto2, mapper);

            var id = 1;

            //Prueba
            var respuesta = await controller.ObtenerPorId(id);

            //Verificacion
            var resultado = respuesta.Value;
            Assert.AreEqual(expected: id, actual: resultado!.Id);
        }

        [TestMethod]
        public async Task Post_DebeCrearGenero_CuandoEnviamosGenero()
        {
            // Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutomapper();
            var outputCacheStore = new OutputCacheStoreFalse();
            var nuevoGenero = new GeneroCreationDTO() { Nombre = "Nuevo Genero" };
            var controller = new GenerosController(outputCacheStore, contexto, mapper);

            //Prueba
            var respuesta = await controller.Post(nuevoGenero);

            //Verificacion
            var resultado = respuesta as CreatedAtRouteResult;
            Assert.IsNotNull(resultado);

            var contexto2 = ConstruirContext(nombreBD);
            var cantidad = await contexto2.Generos.CountAsync();

            Assert.AreEqual(expected: 1, actual: cantidad);
        }

        private const string cacheTag = "generos";

        [TestMethod]
        public async Task Post_DebeLLamarEvictByTagAsync_CuandoEnviamosGenero()
        {
            // Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutomapper();
            var outputCacheStore = Substitute.For<IOutputCacheStore>(); //Doble de IOutputCacheStore
            
            var nuevoGenero = new GeneroCreationDTO() { Nombre = "Nuevo Genero" };
            var controller = new GenerosController(outputCacheStore, contexto, mapper);

            //Prueba
            var respuesta = await controller.Post(nuevoGenero);

            //Verificacion
            await outputCacheStore.Received(1).EvictByTagAsync(cacheTag, default);
        }
    }
}
