﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eBooks.Domain.Entities.Identity;
using eBooks.Utility.Constants;

namespace eBooks.ViewModel.Role
{
    public class EditRoleViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Role Title")]
        [Required(ErrorMessage = AttributesErrorMessages.EnterName)]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF, \s]*$", ErrorMessage = AttributesErrorMessages.NameStructure)]
        public string Name { get; set; }

        [Display(Name = "New Role Title.")]
        [RegularExpression(@"^[\u0600-\u06FF,\u0590-\u05FF, \s]*$", ErrorMessage = AttributesErrorMessages.NameStructure)]
        [Remote("CheckExistRoleName", "Role", "Admin", ErrorMessage = AttributesErrorMessages.RemoteRoleNameMessage)]
        public string? NewName { get; set; }

        public List<Guid>? AllPermissionToRoleList { get; set; }
    }
}