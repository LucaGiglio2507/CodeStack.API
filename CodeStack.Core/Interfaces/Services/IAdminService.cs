using CodeStack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeStack.Core.Interfaces.Services
{
    public interface IAdminService
    {
        /// <summary>
        /// Désactive le compte d'un utilisateur (Soft Delete).
        /// Seul un utilisateur avec le rôle Administrator peut déclencher cette logique via le Controller.
        /// </summary>
        /// <param name="userId">L'identifiant unique de l'utilisateur à désactiver.</param>
        /// <returns>True si l'utilisateur a été trouvé et désactivé, sinon False.</returns>


        Task<User?> DisableUserAsync(Guid userId, Guid adminId);
    }
}
