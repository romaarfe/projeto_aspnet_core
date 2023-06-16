// PROJETO DE PROGRAMAÇÃO - 50 HORAS
// FORMADOR: PAULO JORGE

// TEMA: WEB RPG CREATION
// FORMANDO: RODRIGO FERNANDES - Nº 13

// RAZOR PAGE CRIADA AUTOMÁTICA A PARTIR DO IDENTITY
// SEM GRANDES FUNÇÕES ALÉM DE APRESENTAR A VIEW COM MENSAGEM

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

// ÁREA DOS USINGS/IMPORTS

using Microsoft.AspNetCore.Mvc.RazorPages;

// NAMESPACE DO IDENTITY ACCOUNT

namespace WebRPGCreation.Areas.Identity.Pages.Account
{
    // HERANÇA A PARTIR DE CLASSE ABSTRATA PARA ATUAR COMO MODELO E PÁGINA

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class AccessDeniedModel : PageModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>

        // MÉTODO ASSÍNCRONO PARA APRESENTAR CÓDIGOS DE RECUPERAÇÃO (GET)

        public void OnGet()
        {
        }
    }
}
