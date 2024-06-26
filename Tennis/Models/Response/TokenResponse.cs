﻿namespace Tennis.Models.Response
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime ExpirationToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ? RefreshTokenExpiration { get; set; }
    }
}
