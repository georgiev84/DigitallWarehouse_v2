﻿namespace UserManagementService.Application.Models.Dto.Login;

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}