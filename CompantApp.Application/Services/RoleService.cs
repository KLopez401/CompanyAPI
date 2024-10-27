﻿using CompanyApp.Application.Interfaces;
using CompanyApp.Domain.Dto.Role;
using CompanyApp.Domain.Repository.Interfaces;

namespace CompanyApp.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;

        public RoleService(IRoleRepository roleRepository, IConfiguration configuration)
        {
            _roleRepository = roleRepository;
            _configuration = configuration;
        }
        public async Task<RoleDto> GetRoleById(int id)
        {
            var role = await _roleRepository.GetRoleById(id);
            if (role == null) return null;

            var response = new RoleDto()
            {
                Id = id,
                Name = role.Name,
            };

            return response;
        }

        public async Task<RoleDto> GetRoleByName(string roleName)
        {
            var role = await _roleRepository.GetRoleByName(roleName);
            if (role == null) return null;

            var response = new RoleDto()
            {
                Id = role.Id,
                Name = role.Name,
            };

            return response;
        }
    }
}
