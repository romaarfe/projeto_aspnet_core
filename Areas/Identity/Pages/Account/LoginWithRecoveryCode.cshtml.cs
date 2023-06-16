// PROJETO DE PROGRAMAÇÃO - 50 HORAS
// FORMADOR: PAULO JORGE

// TEMA: WEB RPG CREATION
// FORMANDO: RODRIGO FERNANDES - Nº 13

// RAZOR PAGE CRIADA AUTOMÁTICA A PARTIR DO IDENTITY
// ATUA COMO MODELO E "CONTROLADOR" (PÁGINA) QUANDO SE TRATA DO LOGIN COM 2FA

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

// ÁREA DOS USINGS/IMPORTS

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

// NAMESPACE DO IDENTITY ACCOUNT

namespace WebRPGCreation.Areas.Identity.Pages.Account
{
    // HERANÇA A PARTIR DE CLASSE ABSTRATA PARA ATUAR COMO MODELO E PÁGINA

    public class LoginWithRecoveryCodeModel : PageModel
    {
        // VARIÁVEIS DA CLASSE (API) PARA ENCAPSULAR E GERIR USERS E LOGINS

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<LoginWithRecoveryCodeModel> _logger;

        // CONSTRUTOR QUE FAZ INSTANCIAÇÕES PARA FUTURO ACESSO ÀS TABELAS NA BASE DE DADOS

        public LoginWithRecoveryCodeModel(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ILogger<LoginWithRecoveryCodeModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        // PROPRIEDADES DA CLASSE COM DATA ANNOTATIONS

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [BindProperty]
            [Required(ErrorMessage = "O código de recuperação é necessário.")]
            [DataType(DataType.Text)]
            [Display(Name = "Código de recuperação")]
            public string RecoveryCode { get; set; }
        }

        // MÉTODO ASSÍNCRONO PARA PROCESSO DE LOGIN 2FA (GET)

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            // ASSEGURA QUE O UTILIZADOR PASSOU PELA TELA DE UTILIZADOR E SENHA PRIMEIRO

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            ReturnUrl = returnUrl;

            return Page();
        }

        // MÉTODO ASSÍNCRONO PARA PROCESSO DE LOGIN 2FA (POST)

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = Input.RecoveryCode.Replace(" ", string.Empty);

            var result = await _signInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            var userId = await _userManager.GetUserIdAsync(user);

            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with a recovery code.", user.Id);
                return LocalRedirect(returnUrl ?? Url.Content("~/"));
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("Conta de utilizador bloqueada.");
                return RedirectToPage("./Lockout");
            }
            else
            {
                _logger.LogWarning("Invalid recovery code entered for user with ID '{UserId}' ", user.Id);
                ModelState.AddModelError(string.Empty, "Código de recuperação inválido.");
                return Page();
            }
        }
    }
}
