﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Models;
using Repository;

namespace Logic
{
    public class CurriculumLogic : ICurriculumLogic
    {
        private IRepository<Curriculum> curriculumRepository;

        public CurriculumLogic(IRepository<Curriculum> curriculumRepository)
        {
            this.curriculumRepository = curriculumRepository;
        }

        public void AddCurriculum(Curriculum curriculum)
        {
            bool isValid = ICurriculumLogic.ValidateCurriculum(curriculum);
            if (!isValid)
            {
                throw new ArgumentException("Invalid argument(s) provided!");
            }
            try
            {
                curriculumRepository.Create(curriculum);
            } catch (Exception) {
                throw new ArgumentException("Failed to update database");
            }
        }

        public Curriculum GetCurriculum(int id)
        {
            var curriculum = curriculumRepository.Read(id);
            if (curriculum == null)
            {
                throw new ObjectNotFoundException(id, typeof(Curriculum));
            }
            return curriculum;
        }

        public IEnumerable<Curriculum> GetCurriculums()
        {
            return curriculumRepository.ReadAll();
        }

        public void RemoveCurriculum(int id)
        {
            try
            {
                curriculumRepository.Delete(id);
            }
            catch (ArgumentNullException)
            {
                throw new ObjectNotFoundException(id, typeof(Curriculum));
            }
        }

        public void UpdateCurriculum(Curriculum curriculum)
        {
            bool isValid = ICurriculumLogic.ValidateCurriculum(curriculum);
            if (!isValid)
            {
                throw new ArgumentException("Invalid argument(s) provided!");
            }
            var old = curriculumRepository.Read(curriculum.CurriculumId);
            if (old == null)
                throw new ObjectNotFoundException(curriculum.CurriculumId, typeof(Curriculum));
            try
            {
                curriculumRepository.Update(curriculum);
            }
            catch (Exception)
            {
                throw new ArgumentException("Failed to update database");
            }
        }
    }
}
