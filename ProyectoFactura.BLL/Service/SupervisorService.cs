﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFactura.DAL.Repositories;
using ProyectoFactura.Models;

namespace ProyectoFactura.BLL.Service
{
    public class SupervisorService : ISupervisorService
    {

        private readonly IGenericRepository<Supervisor> _supervisorRepository;
        public SupervisorService(IGenericRepository<Supervisor> supervisorRepo)
        {
            _supervisorRepository = supervisorRepo;
        }

        public async Task<bool> Add(Supervisor supervisor)
        {
            string supervisorId = Convert.ToString(supervisor.Idsupervisor);
            if (string.IsNullOrEmpty(supervisorId))
            {
                throw new ArgumentException("La identificación del supervisor no puede ser nula o vacía.");
            }

            return await _supervisorRepository.Add(supervisor);
        }

        public async Task<List<Supervisor>> GetAll()
        {
            try
            {
                var supervisores = await _supervisorRepository.GetAll();
                return supervisores;
            }
            catch (Exception ex)
            {

                throw new Exception("Error al obtener los supervisores", ex);
            }
        }

        public async Task<Supervisor> GetById(object id)
        {
            var supervisor = await _supervisorRepository.GetById(id);
            return supervisor == null ? throw new Exception("Supervisor no encontrado") : supervisor;
        }

    }
}
