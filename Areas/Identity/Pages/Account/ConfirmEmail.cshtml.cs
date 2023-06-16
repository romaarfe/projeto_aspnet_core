// PROJETO DE PROGRAMAÇÃO - 50 HORAS
// FORMADOR: PAULO JORGE

// TEMA: WEB RPG CREATION
// FORMANDO: RODRIGO FERNANDES - Nº 13

// RAZOR PAGE CRIADA AUTOMÁTICA A PARTIR DO IDENTITY
// ATUA COMO MODELO E "CONTROLADOR" (PÁGINA) QUANDO SE TRATA DA CONFIRMAÇÃO DE EMAIL

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

// ÁREA DOS USINGS/IMPORTS

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

// NAMESPACE DO IDENTITY ACCOUNT

namespace WebRPGCreation.Areas.Identity.Pages.Account
{
    // HERANÇA A PARTIR DE CLASSE ABSTRATA PARA ATUAR COMO MODELO E PÁGINA

    public class ConfirmEmailModel : PageModel
    {
        // VARIÁVEIS DA CLASSE (API) PARA ENCAPSULAR E GERIR USERS E LOGINS

        private readonly UserManager<IdentityUser> _userManager;

        // CONSTRUTOR QUE FAZ INSTANCIAÇÕES PARA FUTURO ACESSO ÀS TABELAS NA BASE DE DADOS

        public ConfirmEmailModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // PROPRIEDADES DA CLASSE COM DATA ANNOTATIONS

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        // MÉTODO ASSÍNCRONO PARA APRESENTAR CÓDIGOS DE RECUPERAÇÃO (GET)

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage = result.Succeeded ? "Obrigado por confirmar seu email." : "Erro ao confirmar seu email.";
            return Page();
        }
    }
}
