﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Logic;
using Models;

namespace Endpoint
{
    [ApiController]
    [Route("/Curriculums")]
    public class CurriculumController : Controller
    {
        private ICurriculumLogic curriculumLogic;

        public CurriculumController(ICurriculumLogic curriculumLogic)
        {
            this.curriculumLogic = curriculumLogic;
        }

        [HttpGet("{id}")]
        public Curriculum Get(int id)
        {
            return curriculumLogic.GetCurriculum(id);
        }

        [HttpGet]
        public IEnumerable<Curriculum> GetAll()
        {
            return curriculumLogic.GetCurriculums();
        }

        [HttpPost]
        public void Create([FromBody] Curriculum curriculum)
        {
            curriculumLogic.AddCurriculum(curriculum);
        }

        [HttpPut]
        public void Edit([FromBody] Curriculum curriculum)
        {
            curriculumLogic.UpdateCurriculum(curriculum);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            curriculumLogic.RemoveCurriculum(id);
        }
    }
}
